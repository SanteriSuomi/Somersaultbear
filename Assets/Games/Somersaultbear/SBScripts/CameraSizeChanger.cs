using Cinemachine;
using UnityEngine;

namespace Somersaultbear
{
    public class CameraSizeChanger : MonoBehaviour
    {
        [SerializeField]
        private GameObject player = default;
        [SerializeField]
        private CinemachineVirtualCamera cinemachine = default;
        [SerializeField]
        private LayerMask groundLayer = default;

        [SerializeField]
        private float lensSmoothTime = 0.5f;
        [SerializeField]
        private float detectionHeight = 1.25125f;
        [SerializeField]
        private float maxLensSize = 7;
        [SerializeField]
        private float minLensSize = 5;
        private float yVelocity = 0f;

        private void Update()
        {
            RaycastHit2D rayHit = Physics2D.Raycast(player.transform.position, Vector2.down, detectionHeight, groundLayer);

            #if UNITY_EDITOR
            Debug.DrawRay(player.transform.position, Vector2.down * detectionHeight, Color.black);
            #endif

            if (rayHit)
            {
                ChangeOrtographicSize(minLensSize);
            }
            else
            {
                ChangeOrtographicSize(maxLensSize);
            }
        }

        private void ChangeOrtographicSize(float target) 
            => cinemachine.m_Lens.OrthographicSize = Mathf.SmoothDamp(cinemachine.m_Lens.OrthographicSize, target, ref yVelocity, lensSmoothTime);
    }
}