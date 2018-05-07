# MazeDraw

### Project Description 
Maze Draw is a virtually reality, two player, proof of concept game designed to blend the virtual and physical world. Through the use of OpenCV, Unity, and Oculus, a drawn maze on a whiteboard is translated into a 3D maze within unity that a player in VR can walk through. Although MazeDraw is not a complete "Game" so to speak, we had many ideas for the game that MazeDraw could one day be developed into, such as a dungeons and dragons style game, or adding compettivitiy between the drawer and the maze runner. Overall the project achieved its goal of using intuitive drawing as the input to designing a 3D virtual world.

[See the video documentation here!](https://www.youtube.com/watch?v=S9dH5GWhVRM&feature=youtu.be)

### Implementation of Computer Vision

The script that does the job of using the webcam and recognizing the maze is found at Assets/Scripts/LSDTest.cs. It works by utilizing a color filter on a webcam image, and is then sent to OpenCV's line segment detector. The line segment coordinates are converted from pixel coordinates to game grid coordinates and then sent to the MazeDraw.cs script which stores an array of the lines that should be built in the game.

### Resources

* PC running windows 10 with Unity 5 or later
* Oculus Rift heaset and Touch controllers
* Gridded Whiteboard with dry erase markers
* HD webcam that can be mounted vertically over whiteboard


### Authors

1.  Brandon Boylan-Peck
2.  Nick Osterfelt
3.  Wylie Wells

[See github.io site for MazeDraw here!](https://nickosterfelt.github.io/MazeDraw/)
