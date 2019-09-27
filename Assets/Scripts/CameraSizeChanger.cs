using UnityEngine;
using Cinemachine;
using UnityEngine.Assertions;

public class CameraSizeChanger : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer = default;

    [SerializeField]
    private GameObject player = default;
    [SerializeField]
    private CinemachineVirtualCamera cinemachine = default;

    [SerializeField]
    private float lensSmoothTime = 0.5f;
    [SerializeField]
    private float detectionHeight = 1.25125f;
    private float yVelocity = 0f;

    private const float maxLensSize = 7f;
    private const float minLensSize = 5f;

    private void Start()
    {
        #if UNITY_EDITOR
        Assert.IsNotNull(player);
        Assert.IsNotNull(cinemachine);
        #endif
    }

    private void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, detectionHeight, groundLayer);

        #if UNITY_EDITOR
        Debug.DrawRay(player.transform.position, Vector2.down * detectionHeight, Color.black);
        #endif

        if (rayHit)
        {
            CameraSmall();
        }
        else
        {
            CameraBig();
        }
    }

    private void CameraSmall()
    {
        // Change the camera size smoothly.
        cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, minLensSize, ref yVelocity, lensSmoothTime);
    }

    private void CameraBig()
    {
        cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, maxLensSize, ref yVelocity, lensSmoothTime);
    }
}