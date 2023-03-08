using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Platform") ||
            col.transform.CompareTag("Button")) //передаем персонажу скорость движущихся платформ
            transform.parent = col.transform;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Platform") ||
            col.transform.CompareTag("Button")) //убираем у персонажа скорость платформы
            transform.parent = null;
    }
}