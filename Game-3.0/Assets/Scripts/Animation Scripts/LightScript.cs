using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightScript : MonoBehaviour
{
    private Light2D lightBulb;
    private GameObject player;

    private void Start()
    {
        player = gameObject.gameObject;
        for (var i = 0; i < player.transform.childCount; i++)
        {
            var child = player.transform.GetChild(i);
            if (!child.CompareTag("LightBulb")) continue;
            lightBulb = child.transform.GetChild(0).GetComponent<Light2D>();
            break;
        }
    }


    private void Update()
    {
        if (lightBulb == null) return;
        if (!Control.CheckForConnection(player))
            lightBulb.intensity = 0f;
        else
            lightBulb.intensity = 1f;
    }
}