using UnityEngine;

public class Picture2Spawner : MonoBehaviour
{
    private bool levelAlreadyOpened;

    private void Update()
    {
        if (PlayerPrefs.HasKey("1st level") && !levelAlreadyOpened)
        {
            transform.position = new Vector3(-0.14f, 2.94f - 0.88f, 8.8f);
            levelAlreadyOpened = true;
        }
    }
}