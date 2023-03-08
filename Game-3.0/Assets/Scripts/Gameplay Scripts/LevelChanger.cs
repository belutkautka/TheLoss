using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private static Animator anim;
    private static int levelToLoad = 4;
    private static readonly int Fade = Animator.StringToHash("fade");

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void ChangeLevelToLoad(int lvl)
    {
        levelToLoad = lvl;
    }

    public static void FadeToLevel()
    {
        anim.SetTrigger(Fade);
    }

    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().buildIndex != 4) 
            levelToLoad = 4;
        if (SceneManager.GetActiveScene().buildIndex == 0) 
            levelToLoad = 5;
        if (SceneManager.GetActiveScene().buildIndex == 3) 
            levelToLoad = 6;
        SceneSaver.Save(SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene(levelToLoad);
    }
}