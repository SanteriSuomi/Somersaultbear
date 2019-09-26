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
            var projectile = projectiles.Where(p => !p.activeSelf).First();

            Assert.IsNotNull(projectile);
            Debug.Log($"Launched {projectile.name}");

            projectile.SetActive(true);

            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;

            var projectileRigidbody = projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x * projectileSpeed, target.y * projectileSpeed);
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }
}
