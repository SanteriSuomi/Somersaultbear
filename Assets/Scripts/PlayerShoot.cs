using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab = default;
    private List<GameObject> projectiles = default;

    private Vector3 target;

    [SerializeField]
    private int amountToPool = 10;
    [SerializeField]
    private float projectileSpeed = 10f;

    private bool pressedLeftClick = false;

    private void Start()
    {
        projectiles = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            projectiles.Add(obj);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            target.z = 0f;

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
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x * projectileSpeed, target.y * projectileSpeed);
        }
    }
}
