using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(MeshRenderer))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject character = default;
    private Rigidbody2D characterRigidbody = default;
    private Renderer backgroundRenderer = default;

    private Vector2 backgroundNewPosition;

    [SerializeField]
    private float offsetSmooth = 1f;
    [SerializeField]
    private float unitsToOffset = 1f;
    private float backgroundRendererOffsetX;
    private float currentMaxX;
    private float yVelocity = 0;

    private bool wasMaxUpdated = false;

    private const float RB_VELOCITY_RANGE = 0.15f;

    private void Start()
    {
        characterRigidbody = character.GetComponent<Rigidbody2D>();

        backgroundRenderer = GetComponent<MeshRenderer>();

        currentMaxX = character.transform.position.x;

        #if UNITY_EDITOR
        Assert.IsNotNull(character);
        Assert.IsNotNull(backgroundRenderer);
        #endif
    }

    private void Update()
    {
        // Update the current material offset X to the field.
        backgroundRendererOffsetX = backgroundRenderer.material.mainTextureOffset.x;

        // Determine if the current character's position is higher than the highest it has been.
        if (character.transform.position.x > currentMaxX)
        {
            // Update the highest max position with the current one.
            currentMaxX = character.transform.position.x;

            // Signal that it has indeed been updated.
            wasMaxUpdated = true;
        }
        else if (character.transform.position.x < currentMaxX)
        {
            currentMaxX = character.transform.position.x;

            wasMaxUpdated = false;
        }

        if (characterRigidbody.velocity.x > -RB_VELOCITY_RANGE && characterRigidbody.velocity.x < RB_VELOCITY_RANGE)
        {
            // Do nothing if player is close to stationary.
        }
        else if (wasMaxUpdated)
        {
            // Move the repeating texture on the quad on X axis.
            MoveOffsetRight();
        }
        else if (!wasMaxUpdated)
        {
            MoveOffsetLeft();
        }

        // Update the texture offset after the method calls.
        backgroundRenderer.material.mainTextureOffset = backgroundNewPosition;
    }

    private void MoveOffsetRight()
    {
        // Smoothly transition the image with SmoothDamp.
        backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX, backgroundRendererOffsetX + unitsToOffset, ref yVelocity, offsetSmooth), 0);
    }

    private void MoveOffsetLeft()
    {
        backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX, backgroundRendererOffsetX - unitsToOffset, ref yVelocity, offsetSmooth), 0);
    }
}
