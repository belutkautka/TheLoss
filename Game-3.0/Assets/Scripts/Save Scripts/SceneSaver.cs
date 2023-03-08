using UnityEngine;

public class SceneSaver : MonoBehaviour
{
    public static void Save(string sceneName)
    {
        PlayerPrefs.SetString(sceneName, sceneName);
        PlayerPrefs.Save();
    }

    public static bool CheckForSave(string sceneName)
    {
        return PlayerPrefs.HasKey(sceneName);
    }
}