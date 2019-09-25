﻿using UnityEngine;
using UnityEngine.Assertions;

public class EnemyWaspAI : MonoBehaviour
{
    private UIManager uiManager;
    private SpriteRenderer spriteRenderer;

    private Vector3 target;

    [SerializeField]
    private float moveSpeed = 1f;

    private void Start()
    {
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(uiManager);
        Assert.IsNotNull(spriteRenderer);
        #endif
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            #if UNITY_EDITOR
            Debug.Log($"{gameObject.name} hit {other.name}");
            #endif

            target = other.transform.position;
            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            uiManager.ShowMenuItemsDeath();
        }
    }

    private void Update()
    {
        if (target.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
