﻿using UnityEngine;
using UnityEngine.Assertions;

public class Pickups : MonoBehaviour
{
    ScoreManager scoreManager;

    [SerializeField]
    private Sprite[] sprites = default;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int scoreToGive = 10000;
    private int random = 0;

    private void Start()
    {
        scoreManager = GameObject.Find("PRE_ScoreManager").GetComponent<ScoreManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(scoreManager);
        Assert.IsNotNull(spriteRenderer);
        #endif

        RandomiseSprite();
    }

    private void OnEnable()
    {
        if (random != 0)
        {
            RandomiseSprite();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreManager.CurrentScore += scoreToGive;
            gameObject.SetActive(false);
        }
    }

    private void RandomiseSprite()
    {
        // Randomise the sprite to a random sprite in the sprites array.
        random = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[random];
    }
}
