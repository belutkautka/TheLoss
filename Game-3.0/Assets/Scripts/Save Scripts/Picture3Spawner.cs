using UnityEngine;

public class Picture3Spawner : MonoBehaviour
{
    private bool levelAlreadyOpened;

    private void Update()
    {
        if (PlayerPrefs.HasKey("2nd level") && !levelAlreadyOpened)
        {
            transform.position = new Vector3(4.02f, 2.94f - 0.88f, 8.8f);
            levelAlreadyOpened = true;
        }
    }
}