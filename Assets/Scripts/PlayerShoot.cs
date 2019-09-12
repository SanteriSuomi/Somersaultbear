using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	[SerializeField]
	private GameObject projectile;

	[SerializeField]
	[Range(0.0f, 1f)]
	private float projectileSpeed = 0.05f; 

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
		{
			var shotProjectile = Instantiate(projectile, gameObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

			var shotProjectileRb = shotProjectile.GetComponent<Rigidbody2D>();

			shotProjectileRb.velocity = transform.TransformDirection(Input.mousePosition.x * projectileSpeed, Input.mousePosition.y * projectileSpeed, 0);
		}
    }
}