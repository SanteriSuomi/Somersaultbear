using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField]
    private GameObject player = default;

    private GameObject _camera = default;

    [SerializeField]
    private Vector3 cameraOffset = default;

    private void Start()
    {
        _camera = GameObject.Find("PRE_Main_Camera");
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        _camera.gameObject.transform.position = player.gameObject.transform.position + cameraOffset;
    }
}
