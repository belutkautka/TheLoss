using UnityEngine;

public class Button : MonoBehaviour
{
    public float upperEdge = 5f;
    public float bottomEdge = -1f;
    public float speed = 3f;
    public GameObject platform;

    private Animator animator;
    private bool onCollision;
    private float startPositionY;
    private static readonly int GoDown = Animator.StringToHash("goDown");
    private static readonly int GoUp = Animator.StringToHash("goUp");

    private void Start()
    {
        startPositionY = transform.position.y;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        // опускание кнопки и поднятие кнопки
        if (!onCollision)
            MovePlatformDown();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger(GoDown);
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        onCollision = false;
        MovePlatformDown();
        animator.SetTrigger(GoUp);
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        onCollision = true;
        MovePlatformUp();
    }

    private void MovePlatformUp()
    {
        if (platform.transform.position.y > upperEdge)
            return;
        platform.transform.position =
            new Vector2(platform.transform.position.x, platform.transform.position.y + speed * Time.deltaTime);
    }

    private void MovePlatformDown()
    {
        if (platform.transform.position.y < bottomEdge)
            return;
        platform.transform.position =
            new Vector2(platform.transform.position.x, platform.transform.position.y - speed * Time.deltaTime);
    }
}