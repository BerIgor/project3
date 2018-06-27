# In this repository
tobii_bench for tobii tracker evaluation

## tobii_bench
### what you'll need
__Tobii core software: https://tobiigaming.com/getstarted/ __

This program uses Tobii Core SDK

### How to run
1. Calibrate the device (this is commented out in the program due to inconsistent behaviour)
2. Run tobii_bench.exe from ~/tobii_bench/tobii_bench/bin/Debug/
   or
   Use VS to open the project and run Program

   In either case, the following files should be in the .exe directory (maybe one is enough, but hey, it works)
   Tobii.EyeX.Client.dll
   Tobii.Interaction.Model.dll
   Tobii.Interaction.Net.dll
3. Fill in session data; Press OK.
4. The white canvas is the data collection process. To begin click (LMB) at the top left corner of the screen (0, 0)
5. The run ends when you complete all 15 points, or press Esc
6. Data is saved at the .exe dir. under data

### Program information
* The circle radius, number of rows and number of columns, are defined in the DataCollecter class in Program.cs
* Tobii interaction is done in the the TobiiTracker class
* The canvas listens to mouse clicks using OnCanvasMouseClick in DataCollector. On a click, the gaze point is registered using RegisterGaze, then returned by request via GetGaze.

# Not in this repository
#### Screen overlay (i.e. the thing that shows user's gaze)
https://tobiigaming.com/tobii-ghost/

#### The Core SDK
https://developer.tobii.com/consumer-eye-trackers/core-sdk/





### PyGaze Installation (you don't need this)
Following excerpts from http://www.pygaze.org/2015/10/pygaze-installation-on-debian-8-jessie/#sec-3-2
sudo apt-get install python-numpy
sudo apt-get install python-imaging
sudo apt-get install python-pyglet
sudo apt-get install psychopy
sudo apt install python-pip
pip install python-pygaze

