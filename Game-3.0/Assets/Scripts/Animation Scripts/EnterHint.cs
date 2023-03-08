using UnityEngine;

public class EnterHint : MonoBehaviour
{
    private static Animator anim;
    private static readonly int OnTrigger = Animator.StringToHash("onTrigger");
    private static readonly int OutTrigger = Animator.StringToHash("outTrigger");

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public static void StartAppearance()
    {
        anim.SetTrigger(OnTrigger);
    }

    public static void StartDisappearance()
    {
        anim.SetTrigger(OutTrigger);
    }
}