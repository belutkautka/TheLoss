using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TimeLineMenedgerSc : MonoBehaviour
{
    [FormerlySerializedAs("Director")] public PlayableDirector director;
    
    private void Update()
    {
        if (director.state != PlayState.Playing) SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
}