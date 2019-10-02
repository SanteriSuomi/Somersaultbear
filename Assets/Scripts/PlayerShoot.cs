using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
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

    private float timer;

    private bool pressedLeftClick = false;

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
    }

    private void Start()
    {
        // Instantiate the object pool with the projectile prefabs.
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
        // Update timer time.
        timer += Time.deltaTime;
        // Check if mouse button is pressed and timer above a certain value.
        if (Input.GetMouseButtonDown(0) && timer >= 0.125)
        {
            // Reset the timer.
            timer = 0;
            // Update the target of the projectile by transforming the mouseclick position from screen to world space.
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            // Set the Z vector 0 zero just in case.
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
            Debug.Log($"Launched {projectile.name}.");
            #endif

            // Activate the prefab.
            projectile.SetActive(true);
            // Set the activated prefab's position and transform to the player that shoots it.
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;
            // Give the spawned prefab some velocity according to the target's X and Y values.
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x * projectileSpeed, target.y * projectileSpeed), ForceMode2D.Impulse);
            // Ignore the collisions between the player and the spawned projectile.
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            // Ignore collisions between the projectile and boulders using layers.
            Physics2D.IgnoreLayerCollision(10, 11);
        }
    }
}