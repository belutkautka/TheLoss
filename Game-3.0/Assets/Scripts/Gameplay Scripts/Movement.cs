using System;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEditor.SearchService;

public class Movement : MonoBehaviour
{
    private static float tempAxis;

    public float speed = 4;
    public float jumpForce = 7;

    public Transform groundCheck;
    public float groundRadius;
    public LayerMask layerGrounds;
    private Animator anim;
    private PlayerInputSystem input;

    private bool isGrounded;
    private bool jump;
    private float movementX;
    private GameObject player;

    private new Rigidbody2D rigidbody;
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Jump1 = Animator.StringToHash("jump");

    private void Awake()
    {
        input = new PlayerInputSystem();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        player = gameObject.gameObject;

        input.Player.Move.performed += context => Move(context.ReadValue<float>());
        input.Player.Move.canceled += context => Move(0);
        input.Player.Jump.performed += context => Jump();
    }

    private void Update()
    {
        if (!Control.CheckForConnection(player))
            movementX = 0;
        else if (input.Player.Move.IsPressed()) Move(tempAxis);
        rigidbody.velocity = new Vector2(movementX, rigidbody.velocity.y);
        anim.SetBool(Run, movementX != 0);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, layerGrounds);
        anim.SetBool(Jump1, !isGrounded);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        movementX = 0;
    }

    private void Move(float axis)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 4)
            MoveHint.StartAnimation();
        tempAxis = axis;
        if (!Control.CheckForConnection(player)) return;
        movementX = axis * speed;
        if (axis != 0)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * axis, transform.localScale.y,
                transform.localScale.z);
    }

    private void Jump()
    {
        if (!isGrounded || !Control.CheckForConnection(player)) return;
        if (SceneManager.GetActiveScene().buildIndex == 1) SpaceHint.StartAnimation();
        rigidbody.velocity = new Vector2(movementX, jumpForce);
        isGrounded = false;
    }
}