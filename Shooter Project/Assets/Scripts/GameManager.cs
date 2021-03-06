﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject positiveTarget;
    [SerializeField] private GameObject negativeTarget;
    [SerializeField] private Text scoreText;
    [SerializeField] private Image winningImage;
    private int score = 0;
    private int gameRunTimeSeconds = 0;
    private float timeBetweenSpawn = 3f;

    public const int TARGETS_ON_FAIL = 10;
    public const int WINNING_SCORE = 50;

    private bool gameRunning; 
    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        InvokeRepeating("CountTime", 0f, 1f);
        Invoke("SpawnTarget", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void SpawnTarget()
    {
        if (Target.GetSpawnedTargets() >= TARGETS_ON_FAIL) gameRunning = false;

        if (gameRunning)
        {
            float x = Random.Range(-8.0f, 8.0f);
            float y = Random.Range(-4.0f, 4.0f);

            int i = Random.Range(0, 5);
            GameObject target;
            if(i == 0)
            {
                target = negativeTarget;
            }
            else
            {
                target = positiveTarget;
            }

            Instantiate(target, new Vector2(x, y), Quaternion.identity);

            if (gameRunTimeSeconds % 10 == 0 && timeBetweenSpawn > 0.5f)
            {
                timeBetweenSpawn -= 0.5f;
            }
            Invoke("SpawnTarget", timeBetweenSpawn);
        }
        else
        {
            Invoke("RestartScene", 0.5f);
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CountTime()
    {
        gameRunTimeSeconds++;

        if (score >= WINNING_SCORE)
        {
            gameRunning = false;
            scoreText.enabled = false;
            winningImage.enabled = true;
        }
    }

    public void IncrementScore(string targetTag)
    {
        if (targetTag == "PositiveTarget")
        {
            score++;
        }else
        {
            score--;
        }
    }

    public int GetScore()
    {
        return this.score;
    }
}
