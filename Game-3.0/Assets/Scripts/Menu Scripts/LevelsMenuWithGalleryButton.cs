using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelsMenuWithGalleryButton : MonoBehaviour
{
    private static bool gameIsPaused;

    private static int buttonIndex;

    [FormerlySerializedAs("PauseMenuUI")] public GameObject pauseMenuUI;

    [FormerlySerializedAs("SettingsMenuUI")]
    public GameObject settingsMenuUI;

    [FormerlySerializedAs("menuIncluded")] [FormerlySerializedAs("MenuIncluded")]
    public bool menuTurnedOn;

    [FormerlySerializedAs("Pointer")] public Animator pointer;
    [FormerlySerializedAs("Slider")] public Animator slider;

    public AudioClip levelStartFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    private AudioSource myFx;
    private float volume = 0.35f;


    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        myFx = GetComponent<AudioSource>();
    }

    private void Update()
    {
        myFx.volume = volume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                if (menuTurnedOn)
                {
                    HoverSound();
                    Resume();
                }
                else
                {
                    SettingsExit();
                }
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && menuTurnedOn && buttonIndex + 1 <= 4)
        {
            HoverSound();
            buttonIndex += 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && menuTurnedOn && buttonIndex - 1 >= 0)
        {
            HoverSound();
            buttonIndex -= 1;
        }

        if (!menuTurnedOn && gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && volume < 1f)
            {
                volume += 0.2f;
                PlayerPrefs.SetFloat("Volume", volume);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && volume > 0f)
            {
                volume -= 0.2f;
                PlayerPrefs.SetFloat("Volume", volume);
            }

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


        if (Input.GetKeyDown(KeyCode.Return) && menuTurnedOn)
            switch (buttonIndex)
            {
                case 0:
                    ClickSound();
                    Resume();
                    break;
                case 1:
                    ClickSound();
                    ReStart();
                    break;
                case 2:
                    SettingsPlay();
                    break;
                case 3:
                    ClickSound();
                    ReturnToGallery();
                    break;
                case 4:
                    QuitGame();
                    break;
            }

        if (menuTurnedOn)
            switch (buttonIndex)
            {
                case 0:
                    pointer.Play("ContinueChoiceGame", -1, 0.5f);
                    break;
                case 1:
                    pointer.Play("RestartChoiceGame", -1, 0.5f);
                    break;
                case 2:
                    pointer.Play("SettingsChoiceGame", -1, 0.5f);
                    break;
                case 3:
                    pointer.Play("ExitChoiceGame", -1, 0.5f);
                    break;
                case 4:
                    pointer.Play("GalleryChoice", -1, 0.5f);
                    break;
            }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        menuTurnedOn = false;
    }

    private void Pause()
    {
        HoverSound();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        menuTurnedOn = true;
    }

    private void QuitGame()
    {
        ClickSound();
        Application.Quit();
    }

    private void ReStart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void SettingsPlay()
    {
        ClickSound();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        menuTurnedOn = false;
    }

    private void SettingsExit()
    {
        HoverSound();
        menuTurnedOn = true;
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    private void ReturnToGallery()
    {
        Resume();
        Time.timeScale = 1f;
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }


    private void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    private void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }

    public void LevelStartSound()
    {
        myFx.PlayOneShot(levelStartFx);
    }
}