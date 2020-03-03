using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Somersaultbear
{
    /// <summary>
    /// World Manager makes sure that floating point inaccuracy won't be a problem (moves the world back to origin after player is too far).
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        [SerializeField]
        private Transform player = default;
        [SerializeField]
        private Transform gameScenes = default;
        [SerializeField]
        private CinemachineVirtualCamera cinemachineCam = default;
        private CinemachineTransposer cinemachineTransposer;
        private WaitUntil waitUntilAdjustmentFalse;
        private Vector3 originalDamping;
        [SerializeField]
        private float maxPlayerXPosBeforeAdjustment = 10000;
        private bool isAdjusting;

        private void Awake()
        {
            cinemachineTransposer = cinemachineCam.GetCinemachineComponent<CinemachineTransposer>();
            originalDamping = new Vector3(cinemachineTransposer.m_XDamping,
                                          cinemachineTransposer.m_YDamping,
                                          cinemachineTransposer.m_ZDamping);
            waitUntilAdjustmentFalse = new WaitUntil(() => !isAdjusting);
        }

        private void Update()
        {
            if (player.position.x > maxPlayerXPosBeforeAdjustment && !isAdjusting)
            {
                StartCoroutine(AdjustPositions());
            }
        }

        private IEnumerator AdjustPositions()
        {
            isAdjusting = true;
            StartCoroutine(ModifyCameraDampingCoroutine());
            StartCoroutine(ResetPlayerPos());
            StartCoroutine(ResetLevelPos());
            yield return null;
            isAdjusting = false;
        }

        private IEnumerator ResetPlayerPos()
        {
            player.position = new Vector3(player.position.x - maxPlayerXPosBeforeAdjustment, 
                player.position.y, 
                player.position.z);
            yield return null;
        }

        private IEnumerator ResetLevelPos()
        {
            gameScenes.position = new Vector3(gameScenes.position.x - maxPlayerXPosBeforeAdjustment, 
                gameScenes.position.y, 
                gameScenes.position.z);
            yield return null;
        }

        private IEnumerator ModifyCameraDampingCoroutine()
        {
            ModifyCameraDamping(Vector3.zero);
            yield return waitUntilAdjustmentFalse;
            ModifyCameraDamping(originalDamping);
        }

        private void ModifyCameraDamping(Vector3 damping)
        {
            cinemachineTransposer.m_XDamping = damping.x;
            cinemachineTransposer.m_YDamping = damping.y;
            cinemachineTransposer.m_ZDamping = damping.z;
        }
    }
}