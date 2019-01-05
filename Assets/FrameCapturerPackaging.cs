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
                "Assets/ML-Agents/RecordVideos",
            };
            AssetDatabase.ExportPackage(files, "MLAgents_RecordVideos.unitypackage", ExportPackageOptions.Recurse);
        }
    }

}
#endif // UNITY_EDITOR
