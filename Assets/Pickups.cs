﻿using UnityEngine;

public class Pickups : MonoBehaviour
{
    ScoreManager scoreManager;

    [SerializeField]
    private Sprite[] sprites = default;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int scoreToGive = 10000;
    private int randomSprite;

    private void Start()
    {
        scoreManager = GameObject.Find("PRE_ScoreManager").GetComponent<ScoreManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        RandomiseSprite();
    }

    private void OnEnable()
    {
        RandomiseSprite();
        gameObject.SetActive(true);
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
        randomSprite = Random.Range(0, sprites.Length);

        spriteRenderer.sprite = sprites[randomSprite];
    }
}
