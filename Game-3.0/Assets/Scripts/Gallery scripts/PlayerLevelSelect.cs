using UnityEngine;

public class PlayerLevelSelect : MonoBehaviour
{
    private bool availableToSwitchLevel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && availableToSwitchLevel && !LevelMenu.GameIsPaused)
            LevelChanger.FadeToLevel();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Picture1"))
        {
            EnterHint.StartAppearance();
            availableToSwitchLevel = true;
            LevelChanger.ChangeLevelToLoad(1);
        }

        if (other.gameObject.CompareTag("Picture2"))
        {
            EnterHint.StartAppearance();
            availableToSwitchLevel = true;
            LevelChanger.ChangeLevelToLoad(2);
        }

        if (other.gameObject.CompareTag("Picture3"))
        {
            EnterHint.StartAppearance();
            availableToSwitchLevel = true;
            LevelChanger.ChangeLevelToLoad(3);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (availableToSwitchLevel)
            EnterHint.StartDisappearance();
        availableToSwitchLevel = false;
    }
}