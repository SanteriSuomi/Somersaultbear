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

    private const float MIN_X_VELOCITY = 0.05f;

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
        if (character.transform.position.x > currentMaxX)
        {
            currentMaxX = character.transform.position.x;
        }

        // Update the current material offset X to the field.
        backgroundRendererOffsetX = backgroundRenderer.material.mainTextureOffset.x;

        // If the instance isn't the menu background.
        if (characterRigidbody.velocity.x > MIN_X_VELOCITY)
        {
            // Move the repeating texture on the quad on X axis to create a scrolling background effect.
            MoveOffsetRight();
        }
        else if (characterRigidbody.velocity.x < -MIN_X_VELOCITY)
        {
            MoveOffsetLeft();
        }

        // Update the texture offset after the method calls.
        backgroundRenderer.material.mainTextureOffset = backgroundNewPosition;
    }

    private void MoveOffsetRight()
    {
        backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX, backgroundRendererOffsetX + unitsToOffset, ref yVelocity, offsetSmooth), 0);
    }

    private void MoveOffsetLeft()
    {
        backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX, backgroundRendererOffsetX - unitsToOffset, ref yVelocity, offsetSmooth), 0);
    }
}
