using UnityEngine;

public class SpiritPicker : MonoBehaviour
{
    private static int spiritCount;
    private static int maxSpiritCount;
    private static Animator anim;
    public AudioClip audioClip;
    private float volume;

    private AudioSource audioSource;
    private static readonly int OpenDoors = Animator.StringToHash("openDoors");


    private void Start()
    {
        maxSpiritCount = GameObject.FindGameObjectsWithTag("Spirit").Length;
        spiritCount = 0;
        audioSource = gameObject.GetComponent<AudioSource>();

        anim = GameObject.FindWithTag("Gate's property").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
            Destroy(other.gameObject);
            spiritCount++;
            if (spiritCount == maxSpiritCount) anim.SetTrigger(OpenDoors);
        }

        if (other.gameObject.CompareTag("Door") && spiritCount == maxSpiritCount) LevelChanger.FadeToLevel();
    }
}