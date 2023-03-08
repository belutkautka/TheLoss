using UnityEngine;

public class MoveHint : MonoBehaviour
{
    private static Animator anim;
    private static readonly int Property = Animator.StringToHash("started moving");

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public static void StartAnimation()
    {
        anim.SetTrigger(Property);
    }
}