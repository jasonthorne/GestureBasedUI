## Voice Activated Media Player

The purpose of this application is to provide a media player to Windows users, which can be controlled through the use of voice commands. This is done by extending the use of Cortana with voice commands that launch and execute actions within the application. It is also designed to be used through a traditional interface of provided buttons, allowing the user to chose their preferable method of operation. 

This uses the _MediaPlayerElement_ to play content. To best accommodate the viewing of videos, this takes up the most space in the app. Its controls can be found at the bottom of the page, providing the user with the buttons necessary to play content.  

The gestures used in this application are incorporated through Cortana, though the use of Voice Command Definitions. These give the user the ability to operate the media player through a set of defined vocal instructions. Enabling the same functionality as can be done through traditional buttons. These are installed upon launch of the app. This allows for the updating of the phrase lists within the Voice Command Definitions file. As the application is initially unaware of the file names of the user’s videos and music, these are dynamically added to the phrase list through first reading them from the user’s system, then altering the phrase lists accordingly. This then allows for the user to speak the name of their song/video through a launch command, which can then be correctly interpreted and launched. 

For testing the gestures of this app, please ensure that you have music files in your music system folder and videos in the video folder. The following is the list of voice commands that can be used within the application:  

* **Play a video:** “play video {video name} in project” 
* **Play some music:** “play music {song name} in project” 
* **Pause player:** “pause [the] [video] [music] in project” 
* **Resume content:** “resume [the] [video] [music] in project” 
* **Fast forward content:** “fast forward the [video] [music] in project” 
* **Rewind content:** “rewind the [video] [music] in project” 
* **Increase volume:** “increase [the] volume” OR “turn up [the] volume in project” 
* **Reduce volume:** “reduce [the] volume” OR “turn down [the] volume in project” 
* **Make full screen:** “make full screen in project” 
* **Exit full screen:** “ exit full screen OR minimize full screen in project” 
* **Stop content:** “stop the [video] [music] in project” 
