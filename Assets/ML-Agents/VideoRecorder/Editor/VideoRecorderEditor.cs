using System;
using UnityEditor;
using UnityEngine;
using UTJ.FrameCapturer;

namespace MLAgents
{
    [CustomEditor(typeof(VideoRecorder))]
    public class VideoRecorderEditor : Editor
    {


        public virtual void AudioConfig()
        {
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            var recorder = target as VideoRecorder;
            var so = serializedObject;
            EditorGUILayout.PropertyField(so.FindProperty("outputDirectory"), true);

            EditorGUILayout.PropertyField(so.FindProperty("durationUnit"), true);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(so.FindProperty("interval"), true);
            EditorGUILayout.PropertyField(so.FindProperty("duration"), true);
            EditorGUI.indentLevel--;

            EditorGUILayout.PropertyField(so.FindProperty("recordInRealtime"), true);
            if (recorder.duration > recorder.interval)
            {
                EditorGUILayout.HelpBox("The Duration of the video shouldn't be longer than the Interval !", MessageType.Error);
            }

            EditorGUILayout.PropertyField(so.FindProperty("resolutionWidth"));
            EditorGUILayout.PropertyField(so.FindProperty("resolutionHeight"));
            EditorGUILayout.PropertyField(so.FindProperty("captureAudio"));
            EditorGUILayout.PropertyField(so.FindProperty("verbose"));
            so.ApplyModifiedProperties();
        }

        [MenuItem("GameObject/Create Recording Camera")]
        public static void AddRecordingCamera()
        {
            GameObject recordingCamera = GameObject.Instantiate(Camera.main.gameObject);
            recordingCamera.name = "Recording Camera";
            recordingCamera.tag = "Untagged";
            recordingCamera.AddComponent<VideoRecorder>();
        }
    }
}