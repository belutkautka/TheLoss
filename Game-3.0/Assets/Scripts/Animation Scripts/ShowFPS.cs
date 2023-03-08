using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public static float fps;

    private void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUILayout.Label("FPS: " + (int) fps);
    }
}