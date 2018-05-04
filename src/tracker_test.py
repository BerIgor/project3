# This test is based on the code available at http://developer.tobiipro.com/python/python-step-by-step-guide.html

import tobii_research as tr
import time

found_eyetrackers = tr.find_all_eyetrackers()
print("hello world")
print(found_eyetrackers)
print("goodbye world")
my_eyetracker = found_eyetrackers[0]
print("Address: " + my_eyetracker.address)
print("Model: " + my_eyetracker.model)
print("Name (It's OK if this is empty): " + my_eyetracker.device_name)
print("Serial number: " + my_eyetracker.serial_number)
