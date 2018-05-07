# MazeDraw

Maze Draw is a virtually reality, two player, proof of concept game designed to blend the virtual and physical world. Through the use of OpenCV, Unity, and Oculus, a drawn maze on a whiteboard is translated into a 3D maze within unity that a player in VR can walk through.

[See the video documentation here!](https://www.youtube.com/watch?v=S9dH5GWhVRM&feature=youtu.be)

### Implementation of Computer Vision

The script that does the job of using the webcam and recognizing the maze is found at Assets/Scripts/LSDTest.cs. It works by utilizing a color filter on a webcam image, and is then sent to OpenCV's line segment detector. The line segment coordinates are converted from pixel coordinates to game grid coordinates and then sent to the MazeDraw.cs script which stores an array of the lines that should be built in the game.


