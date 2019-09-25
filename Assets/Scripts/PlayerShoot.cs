using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab = default;
    [SerializeField]
    private GameObject[] projectiles = default;

    private bool pressedLeftClick = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressedLeftClick = true;
        }
        else
        {
            pressedLeftClick = false;
        }
    }

    private void FixedUpdate()
    {
        if (pressedLeftClick)
        {

        }
    }
}
