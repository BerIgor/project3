## Choose output file to parse (enable following lines to use file dialog)

# import tkinter as tk
# from tkinter import filedialog

# root = tk.Tk()
# root.withdraw() # Maybe not needed

# output_file_path = filedialog.askopenfilename()

import numpy as np

## Open output file and parse

try:
    output_file_path
except NameError:
    output_file_path = 'C:/Users/abinenfe/Downloads/roni_room.txt' # Default error file for now

with open(output_file_path) as f:
    output_lines = f.readlines()
    # No need for f.close when using "with"

output_lines = [x.strip() for x in output_lines] # Remove '\n' and such

# Filter only results from file
res_bool = False
results = list()
for line in output_lines:
    # Assuming output is split to before/after DATA tag
    if (line != 'DATA:' and not res_bool):
        continue
        
    res_bool = True
    if line == 'DATA:' or line.find('Coords') != -1:
        continue
    
    results.append(line)

# Results text to np arrays
N = len(results)
point_coords = np.zeros((2, N))
click_coords = np.zeros((2, N))
gaze_coords = np.zeros((2, N))
point_to_gaze_error = np.zeros(N)

for i in range(N):
    line = results[i]
    line = line.replace('{','').replace('}','').replace('X=','').replace('Y=','').split('|')
    point_coords[:,i] = np.fromstring(line[0], sep=',')
    click_coords[:,i] = np.fromstring(line[1], sep=',')
    gaze_coords[:,i] = np.fromstring(line[2], sep=',')
    point_to_gaze_error[i] = np.linalg.norm(point_coords[:,i]-gaze_coords[:,i])
    

## Draw points on plot

import ctypes
import cv2
from math import ceil

# Get screen resolution
user32 = ctypes.windll.user32
screensize = list([user32.GetSystemMetrics(1), user32.GetSystemMetrics(0)])

# Set color vars (BGR)
red = (0, 0, 255)
green = (0, 255, 0)
blue = (255, 0, 0)
black = (0, 0, 0)

# Set White image  of resolution size + Opencv plot on full screen
cv2.namedWindow("Results Visualization", cv2.WND_PROP_FULLSCREEN)
cv2.setWindowProperty("Results Visualization",cv2.WND_PROP_FULLSCREEN,cv2.WINDOW_FULLSCREEN)
res_canvas = np.ones(screensize, np.uint8)*255
res_canvas = cv2.cvtColor(res_canvas,cv2.COLOR_GRAY2BGR)

# Draw results on plot 
for i in range(N):
    if i == 0:
        continue
    GT = point_coords[:,i].astype(int) # Ground Truth point
    click = click_coords[:, i].astype(int)
    gaze = gaze_coords[:, i].astype(int)
    # TODO - Maybe we should remove points with error bigger than x? (when we have more data)
    # Draw GT
    cv2.circle(res_canvas, tuple(GT), 3, green, -1)
    # Draw click
    cv2.rectangle(res_canvas,tuple(click),tuple(click+3), blue,-1)
    # Draw gaze + radius to gaze + error circle
    cv2.circle(res_canvas, tuple(gaze), 3, red, -1)
    cv2.line(res_canvas,tuple(GT),tuple(gaze), red)
    cv2.circle(res_canvas,tuple(GT),int(round(point_to_gaze_error[i])), red,1)
    # Set text with details
    # TODO - In order to get error in mm, we need to install gi or receive actual screen size as input.
    #        gi - http://pygobject.readthedocs.io/en/latest/getting_started.html. screen size - https://askubuntu.com/questions/153549/how-to-detect-a-computers-physical-screen-size-in-gtk
    error_txt_y = GT[1]-int(ceil(point_to_gaze_error[i]))-2
    cv2.putText(res_canvas, str(np.round(point_to_gaze_error[i],2)), (GT[0],error_txt_y), cv2.FONT_HERSHEY_COMPLEX_SMALL, 1, black, 1, cv2.LINE_AA)
    # Legend text
    cv2.putText(res_canvas, 'GT', (10, 30), cv2.FONT_HERSHEY_COMPLEX_SMALL, 1, green, 1, cv2.LINE_AA)
    cv2.putText(res_canvas, 'Click', (10, 60), cv2.FONT_HERSHEY_COMPLEX_SMALL, 1, blue, 1, cv2.LINE_AA)
    cv2.putText(res_canvas, 'Gaze', (10, 90), cv2.FONT_HERSHEY_COMPLEX_SMALL, 1, red, 1, cv2.LINE_AA)
    cv2.putText(res_canvas, 'Error (Pixels)', (10, 120), cv2.FONT_HERSHEY_COMPLEX_SMALL, 1,  black, 1, cv2.LINE_AA)


cv2.imshow("Results Visualization", res_canvas)
cv2.waitKey()