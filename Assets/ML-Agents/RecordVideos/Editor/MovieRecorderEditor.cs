using System;
using UnityEditor;
using UnityEngine;

namespace UTJ.FrameCapturer
{
    [CustomEditor(typeof(MovieRecorder))]
    public class MovieRecorderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var so = serializedObject;
            so.ApplyModifiedProperties();
        }
    }
}
