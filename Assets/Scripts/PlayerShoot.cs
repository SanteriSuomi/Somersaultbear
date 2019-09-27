using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab = default;
    private List<GameObject> projectiles = default;
    private AudioSource[] audioSources = default;

    private Vector3 target;

    [SerializeField]
    private int amountToPool = 10;
    [SerializeField]
    private float projectileSpeed = 10f;

    private bool pressedLeftClick = false;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        // Instantiate the object pool with the projectile prefabs.
        projectiles = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            projectiles.Add(obj);
        }

        #if UNITY_EDITOR
        Assert.IsNotNull(projectilePrefab);
        Assert.IsNotNull(projectiles);
        Assert.IsNotNull(audioSources);
        #endif
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Update the target of the projectile by transforming the mouseclick from screen to world space.
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            // Set the Z vector 0 zero since it's a 2D game.
            target.z = 0f;
            // Indicate that left click has been pressed.
            pressedLeftClick = true;
        }
    }

    private void FixedUpdate()
    {
        if (pressedLeftClick)
        {
            // Prevent accidental double clicking.
            pressedLeftClick = false;
            // Play the throwing sound (second audiosource on the gameobject).
            audioSources[1].Play();
            // Select the first deactivated prefab from the pool.
            GameObject projectile = projectiles.Where(p => !p.activeSelf).First();

            #if UNITY_EDITOR
            Assert.IsNotNull(projectile);
            Debug.Log($"Launched {projectile.name}.");
            #endif

            projectile.SetActive(true);
            // Set the activated prefab's position and transform to the player that shoots it.
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;
            // Give the spawned prefab some velocity according to the target's X and Y values.
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x * projectileSpeed, target.y * projectileSpeed);
            // Ignore the collisions between the player and the spawned projectile.
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }
}
