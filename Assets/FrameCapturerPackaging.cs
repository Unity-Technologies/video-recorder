#if UNITY_EDITOR
using UnityEditor;


public class FrameCapturerPackaging
{
    [MenuItem("Assets/Make .unitypackage")]
    public static void MakePackage()
    {
        {
            string[] files = new string[]
            {
                "Assets/ML-Agents/VideoRecorder",
            };
            AssetDatabase.ExportPackage(files, "VideoRecorder.unitypackage", ExportPackageOptions.Recurse);
        }
    }

}
#endif // UNITY_EDITOR
