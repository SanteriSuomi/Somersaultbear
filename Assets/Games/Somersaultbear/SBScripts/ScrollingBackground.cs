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
        private float offsetSmooth = 1;
        [SerializeField]
        private float unitsToOffset = 1;
        [SerializeField]
        private float minMoveVelocityRange = 0.15f;
        private float backgroundRendererOffsetX;
        private float currentMaxX;
        private float yVelocity = 0;

        private bool wasMaxUpdated = false;

        private void Awake()
        {
            characterRigidbody = character.GetComponent<Rigidbody2D>();
            backgroundRenderer = GetComponent<MeshRenderer>();
        }

        private void Start() => SetInitialMaxXPos();

        private void SetInitialMaxXPos() => currentMaxX = character.transform.position.x;

        private void Update()
        {
            UpdateBackgroundOffset();
            UpdateCurrentMaxX();
            MoveOffset();
            SetNewBackgroundOffset();
        }

        private void UpdateBackgroundOffset() 
            => backgroundRendererOffsetX = backgroundRenderer.material.mainTextureOffset.x;

        private void UpdateCurrentMaxX()
        {
            if (character.transform.position.x > currentMaxX)
            {
                currentMaxX = character.transform.position.x;
                wasMaxUpdated = true;
            }
            else if (character.transform.position.x < currentMaxX)
            {
                currentMaxX = character.transform.position.x;
                wasMaxUpdated = false;
            }
        }

        private void MoveOffset()
        {
            if (characterRigidbody.velocity.x > -minMoveVelocityRange 
                && characterRigidbody.velocity.x < minMoveVelocityRange)
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