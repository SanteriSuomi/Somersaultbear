using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D), (typeof(AudioSource)))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer = default;
    private Rigidbody2D rigidBody = default;
    private AudioSource audioSource = default;
    private Animator animator = default;

    RaycastHit2D rayHit;

    [SerializeField]
    private float verticalSpeed = 3f;
    [SerializeField]
    private float maxVerticalSpeed = 6.5f;
    [SerializeField]
    private float jumpModifier = 8f;
    [SerializeField]
    private float jumpDetectionHeight = 0.715f;

    private bool pressedSpace = false;
    // Prevent double jumping with small velocity check.
    private const float RB_Y_VELOCITY_MAX = 0.5f;
    private const float REDUCE_AIR_VELOCITY = 2.5f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        #if UNITY_EDITOR
        Assert.IsNotNull(animator);
        #endif
    }

    private void Update()
    {
        pressedSpace = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        // Cast a raycast downwards for ground detection.
        rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

        // Continuously move player to the right.
        if (rigidBody.velocity.x < maxVerticalSpeed)
        {
            rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
        }

        #if UNITY_EDITOR
        Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);
        #endif

        if (rayHit && pressedSpace && rigidBody.velocity.y < RB_Y_VELOCITY_MAX)
        {
            rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
            rigidBody.AddForce(Vector2.left * REDUCE_AIR_VELOCITY, ForceMode2D.Impulse);
            audioSource.Play();

        }

        PlayAnimations();
    }

    private void PlayAnimations()
    {
        if (rigidBody.velocity.y > 1f)
        {
            animator.SetTrigger("Jump");
            FreezeAndResetRotation();
        }
        else
        {
            rigidBody.freezeRotation = false;
        }

        if (rigidBody.velocity.y < -1f)
        {
            animator.SetBool("Fall", true);
            FreezeAndResetRotation();
        }
        else if (rayHit)
        {
            animator.SetBool("Fall", false);
            rigidBody.freezeRotation = false;
        }
    }

    private void FreezeAndResetRotation()
    {
        rigidBody.freezeRotation = true;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}