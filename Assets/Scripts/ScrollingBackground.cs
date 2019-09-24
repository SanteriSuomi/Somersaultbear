using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody2D))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject character = default;

    private Rigidbody2D characterRigidbody = default;

    private Renderer backgroundRenderer = default;

    [SerializeField]
    private bool isMenuBackground = false;

    [SerializeField]
    private float backgroundSpeed = 0.05f;

    private const float MIN_X_VELOCITY = 0.85f;

    private void Start()
    {
        characterRigidbody = character.GetComponent<Rigidbody2D>();

        backgroundRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // If the instance isn't the menu background.
        if (!isMenuBackground && characterRigidbody.velocity.x > MIN_X_VELOCITY)
        {
            // Move the repeating texture on the quad on X axis to create a scrolling background effect.
            MoveOffsetRight();
        }
        // If the instance is the menu background.
        else if (isMenuBackground)
        {
            if (characterRigidbody.velocity.x > float.Epsilon)
            {
                MoveOffsetRight();
            }
            else
            {
                MoveOffsetLeft();
            }
        }
    }

    private void MoveOffsetRight()
    {
        backgroundRenderer.material.mainTextureOffset += Vector2.right * backgroundSpeed * Time.deltaTime;
    }

    private void MoveOffsetLeft()
    {
        backgroundRenderer.material.mainTextureOffset += Vector2.left * backgroundSpeed * Time.deltaTime;
    }
}
