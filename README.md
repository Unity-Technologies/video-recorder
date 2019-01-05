# ML-Agents VideoRecorder plugin
This is a plugin for [Unity's MLAgents framework](https://github.com/Unity-Technologies/ml-agents) that allows to record videos of a scene at a given frequency.

It can be used to :
* Understand how the behavior of your agents evolves over time.
* Debug problems that can happen when the environment has been running for a long time.
* Send a video to a friend to brag that your agents are better than him at a video game.

This repository is based of the [FrameCapturer plugin](https://github.com/unity3d-jp/FrameCapturer), it should work on Windows and Mac. To use it on Linux you will need to compile your own version of the plugin.

# How to use

1. Import this package to your project: [VideoRecorder.unitypackage](https://github.com/Unity-Technologies/video-recorder/raw/master/VideoRecorder.unitypackage)
2. Add a recording camera to your scene, you can either:
    * Create a new camera and add the `VideoRecorder` script to it
    * Use the menu (GameObject -> Create Recording Camera), this will clone the main camera in your scene and add the script
3. Configure the VideoRecorder:
![Video Recorder Settings](https://github.com/Unity-Technologies/video-recorder/raw/master/Images/VideoRecorder.png)

* Output Directory: Where the videos will be saved, it can be either an absolute path or a relative path.
* Duration Unit: The measurement unit for the interval and the duration, either seconds or academy steps.
* Interval: The time we want to wait between each video recording. If the interval is 600 seconds, a new video will start being recorded every 10 minutes.
* Duration: The length of each individual video. Can't be larger than the interval.
* Record in realtime: If you check this and you use a time scale different than 1, the videoRecorder will change the timescale back to 1 while it is recording and then back to the original value the rest of the time. This way your video will show your game at the normal speed, not the accelerated speed during training. Note that this will slow down training speed while the video is recording.
* Resolution: Resolution of the video, can be higher than the resolution of the game window.
* Capture Audio: Should the video contain audio as well?
* Verbose: Log when a recording starts and ends.









