using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraSizeChanger : MonoBehaviour
{
    [SerializeField]
    private float lensSmoothTime = 0.5f;

    [SerializeField]
    private float detectionHeight = 1.43f;

    private float yVelocity = 0f;

    private const float maxLensSize = 7f;

    private const float minLensSize = 5f;

    private const float timeToWait = 2f;

    [SerializeField]
    private LayerMask groundLayer = default;

    [SerializeField]
    private GameObject player = default;

    private CinemachineVirtualCamera cinemachine = default;

    private void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    private void FixedUpdate()
    {
        // Cast a 2d raycast down, and use detection height as the height. Only detect layers specified in groundLayer.
        RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, detectionHeight, groundLayer);

        #if UNITY_EDITOR
        Debug.DrawRay(player.transform.position, Vector2.down * detectionHeight, Color.black);
        #endif

        // Evaluate if player is touching the ground and change camera size according to that.
        if (rayHit && rayHit.collider)
        {
            StartCoroutine(CameraSmall());
        }
        else
        {
            StartCoroutine(CameraBig());
        }
    }

    private IEnumerator CameraSmall()
    {
        // Change size smoothly with smoothdamp.
        cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, minLensSize, ref yVelocity, lensSmoothTime);

        // Wait for a X amount of seconds to prevent any unexpected "spam" behaviour or such.
        yield return new WaitForSeconds(timeToWait);
    }

    private IEnumerator CameraBig()
    {
        cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, maxLensSize, ref yVelocity, lensSmoothTime);

        yield return new WaitForSeconds(timeToWait);
    }
}