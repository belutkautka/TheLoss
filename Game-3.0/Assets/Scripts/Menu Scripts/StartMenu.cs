using UnityEngine;
using UnityEngine.Serialization;

public class StartMenu : MonoBehaviour
{
    private static bool gameIsPaused;
    [FormerlySerializedAs("Slider")] public Animator slider;

    [FormerlySerializedAs("SettingsMenuUI")]
    public GameObject settingsMenuUI;

    public AudioClip clickFx;
    private AudioSource myFx;

    private float volume = 0.4f;

    private void Start()
    {
        myFx = GetComponent<AudioSource>();
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LevelChanger.FadeToLevel();

        myFx.volume = volume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
                SettingsExit();
            else
                SettingsPlay();
        }

        if (gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && volume < 1f) volume += 0.2f;

            if (Input.GetKeyDown(KeyCode.LeftArrow) && volume > 0f) volume -= 0.2f;

            PlayerPrefs.SetFloat("Volume", volume);
            if (volume - 0 <= 10e-7)
                slider.Play("Volume0", -1, 0.5f);
            else if (volume - 0.2f <= 10e-7)
                slider.Play("Volume1", -1, 0.5f);
            else if (volume - 0.4f <= 10e-7)
                slider.Play("Volume2", -1, 0.5f);
            else if (volume - 0.6f <= 10e-7)
                slider.Play("Volume3", -1, 0.5f);
            else if (volume - 0.8f <= 10e-7)
                slider.Play("Volume4", -1, 0.5f);
            else if (volume - 1f <= 10e-7)
                slider.Play("Volume5", -1, 0.5f);
        }
    }

    private void SettingsPlay()
    {
        ClickSound();
        gameIsPaused = true;
        settingsMenuUI.SetActive(true);
    }

    private void SettingsExit()
    {
        ClickSound();
        gameIsPaused = false;
        settingsMenuUI.SetActive(false);
    }

    private void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}