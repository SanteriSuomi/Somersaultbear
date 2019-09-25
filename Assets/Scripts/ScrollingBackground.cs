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
    private float backgroundSpeed = 0.05f;

    [SerializeField]
    private float offsetSmooth = 1f;

    private float backgroundRendererOffsetX;

    private float zeroVelocity = Mathf.Epsilon;

    private float yVelocity = 0;

    private const float UNITS_TO_OFFSET = 1f;

    private void Start()
    {
        characterRigidbody = character.GetComponent<Rigidbody2D>();

        backgroundRenderer = GetComponent<MeshRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(character);
        Assert.IsNotNull(backgroundRenderer);
        #endif
    }

    private void Update()
    {
        // Update the current material offset X to the field.
        backgroundRendererOffsetX = backgroundRenderer.material.mainTextureOffset.x;

        // If the instance isn't the menu background.
        if (characterRigidbody.velocity.x > zeroVelocity)
        {
            // Move the repeating texture on the quad on X axis to create a scrolling background effect.
            MoveOffsetRight();
        }
        else if (characterRigidbody.velocity.x < zeroVelocity)
        {
            MoveOffsetLeft();
        }

        backgroundRenderer.material.mainTextureOffset = backgroundNewPosition;
    }

    private void MoveOffsetRight()
    {
        backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX, backgroundRendererOffsetX + UNITS_TO_OFFSET, ref yVelocity, offsetSmooth), 0);
    }

    private void MoveOffsetLeft()
    {
        backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX, backgroundRendererOffsetX - UNITS_TO_OFFSET, ref yVelocity, offsetSmooth), 0);
    }
}
