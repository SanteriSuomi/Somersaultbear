using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(Renderer))]
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

        private void Awake()
        {
            characterRigidbody = character.GetComponent<Rigidbody2D>();
            backgroundRenderer = GetComponent<MeshRenderer>();
        }

        private void Start() => SetInitialMaxXPos();

        // Set the initial max X vector value to the character's X value.
        private void SetInitialMaxXPos() => currentMaxX = character.transform.position.x;

        private void Update()
        {
            UpdateBackgroundOffset();
            UpdateCurrentMaxX();
            MoveOffset();
            SetNewBackgroundOffset();
        }

        // Update the current material offset X to the field.
        private void UpdateBackgroundOffset() => backgroundRendererOffsetX = backgroundRenderer.material.mainTextureOffset.x;

        private void UpdateCurrentMaxX()
        {
            // Determine if the current character's position is higher than the highest it has been.
            if (character.transform.position.x > currentMaxX)
            {
                // Update the highest max position with the current one.
                currentMaxX = character.transform.position.x;
                wasMaxUpdated = true;
            }
            // Else if the current position doesn't exceed the max position, keep the current value.
            else if (character.transform.position.x < currentMaxX)
            {
                currentMaxX = character.transform.position.x;
                wasMaxUpdated = false;
            }
        }

        private void MoveOffset()
        {
            if (characterRigidbody.velocity.x > -RB_VELOCITY_RANGE && characterRigidbody.velocity.x < RB_VELOCITY_RANGE)
            {
                // Do nothing if player velocity is within range (close to stationary).
            }
            else if (wasMaxUpdated)
            {
                MoveOffsetRight();
            }
            else
            {
                MoveOffsetLeft();
            }
        }

        // Update the texture offset after the method calls.
        private void SetNewBackgroundOffset() => backgroundRenderer.material.mainTextureOffset = backgroundNewPosition;

        private void MoveOffsetRight()
            => backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX,
                backgroundRendererOffsetX + unitsToOffset,
                ref yVelocity,
                offsetSmooth),
                0);

        private void MoveOffsetLeft()
            => backgroundNewPosition = new Vector2(Mathf.SmoothDamp(backgroundRendererOffsetX,
                backgroundRendererOffsetX - unitsToOffset,
                ref yVelocity,
                offsetSmooth),
                0);
    }
}