using UnityEngine;

public class PictureLight : MonoBehaviour
{
    private Animator light;
    private static readonly int LightOn = Animator.StringToHash("lightOn");
    private static readonly int LightOff = Animator.StringToHash("lightOff");

    private void Start()
    {
        light = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) light.SetTrigger(LightOn);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) light.SetTrigger(LightOff);
    }
}