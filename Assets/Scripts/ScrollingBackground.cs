using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject player = default;

    private Rigidbody2D playerRigidbody = default;

    private Renderer backgroundRenderer = default;

    [SerializeField]
    private float backgroundSpeed = 0.05f;

    private const float minXVelocity = 0.85f;

    private void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();

        backgroundRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (playerRigidbody.velocity.x > minXVelocity)
        {
            // Move the repeating texture on the quad on X axis to create a scrolling background effect.
            backgroundRenderer.material.mainTextureOffset += Vector2.right * backgroundSpeed * Time.deltaTime;
        }
    }
}
