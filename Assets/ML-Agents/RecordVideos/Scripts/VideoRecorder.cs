using System;
using MLAgents;
using UnityEngine;
using UTJ.FrameCapturer;

namespace MLAgents
{
    public enum DurationUnit
    {
        Seconds,
        AcademySteps
    }

    [RequireComponent(typeof(MovieRecorder))]
    public class VideoRecorder : MonoBehaviour
    {

        [Tooltip("The directory where the videos will be saved. Can also be an absolute path (i.e C:\\Users\\Batman\\Desktop)")]
        public string outputDirectory = "./Capture";
        public int resolutionWidth = 640;
        public int resolutionHeight = 360;

        [Tooltip("Which unit to use for the interval and duration of videos"
                   + " Note that academy steps will not match with training summary steps if you change the decision frequency of the agent.")]
        public DurationUnit durationUnit;

        [Tooltip("One new video will be recorded every X duration unit.")]
        public int interval = 600;
        [Tooltip("Videos will be X duration unit long.")]
        public int duration = 30;

        [Tooltip("When this is activated, the time scale will be set to 1 during recording."
                + " It will be set back to the previous value at the end of recording.")]
        public bool recordInRealtime;

        [Tooltip("Should the audio be recorded?")]
        public bool captureAudio = true;

        [Tooltip("Log when the recorder starts of finish recording a video")]
        public bool verbose;

        private bool isRecording = false;
        private int recordingStartStep = -1;
        private float recordingStartTime = -1;
        private float timeScaleWhenNotRecording;

        private Academy academy;

        private MovieRecorder movieRecorder;

        private int Steps
        {
            get
            {
                return academy.GetTotalStepCount() - 1;
            }
        }

        private void Awake()
        {
            movieRecorder = GetComponent<MovieRecorder>();
            academy = FindObjectOfType<Academy>();
            Camera mCamera = GetComponent<Camera>();
            RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 16, RenderTextureFormat.ARGB32);
            mCamera.targetTexture = rt;
            movieRecorder.targetRT = rt;

            movieRecorder.outputDir = new DataPath(DataPath.Root.Absolute, outputDirectory);
            movieRecorder.captureAudio = captureAudio;
            movieRecorder.resolutionUnit = RecorderBase.ResolutionUnit.Percent;
            movieRecorder.resolutionPercent = 100;
            movieRecorder.captureControl = RecorderBase.CaptureControl.Manual;
            movieRecorder.framerateMode = RecorderBase.FrameRateMode.Constant;
            movieRecorder.fixDeltaTime = false;
            movieRecorder.waitDeltaTime = false;
        }

        private void Start()
        {
            if (duration > interval)
            {
                Debug.LogError("The duration of a video should be smaller than the interval !");
            }
        }

        private bool ShouldStartRecording()
        {
            if (isRecording)
            {
                return false;
            }

            switch (durationUnit)
            {
                case DurationUnit.AcademySteps:
                    return Steps % interval == 0;
                case DurationUnit.Seconds:
                    return recordingStartTime == -1 || Time.realtimeSinceStartup - recordingStartTime > interval;
            }
            Debug.LogError("Unknown duration unit " + durationUnit);
            return false;
        }

        private bool ShouldEndRecording()
        {
            if (!isRecording)
            {
                return false;
            }

            switch (durationUnit)
            {
                case DurationUnit.AcademySteps:
                    return Steps - recordingStartStep > duration;
                case DurationUnit.Seconds:
                    return Time.realtimeSinceStartup - recordingStartTime > duration;
            }
            Debug.LogError("Unknown duration unit " + durationUnit);
            return false;
        }

        private void FixedUpdate()
        {
            if (ShouldEndRecording())
            {
                if (verbose)
                    Debug.Log("End Recording Video " + Steps + "   " + Time.realtimeSinceStartup);
                movieRecorder.EndRecording();
                isRecording = false;

                if (recordInRealtime)
                {
                    Time.timeScale = timeScaleWhenNotRecording;
                }
            }
            // Order is important in case we want to begin a recording on the same frame we ended one
            else if (ShouldStartRecording())
            {
                if (verbose)
                    Debug.Log("Begin Recording Video " + Steps + "   " + Time.realtimeSinceStartup);
                movieRecorder.BeginRecording(DateTime.Now.ToString("dd-MM_HH'h'mm") + "_" + Steps);
                isRecording = true;
                recordingStartStep = Steps;
                recordingStartTime = Time.realtimeSinceStartup;

                if (recordInRealtime)
                {
                    timeScaleWhenNotRecording = Time.timeScale;
                    Time.timeScale = 1;
                }
            }
        }

    }
}