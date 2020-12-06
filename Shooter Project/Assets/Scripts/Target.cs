﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private static int spawnedTargets = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnedTargets++;
        Debug.Log("Spawned Targets: " + spawnedTargets.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public static int GetSpawnedTargets()
    {
        return spawnedTargets;
    }

    private void OnDestroy()
    {
        gameManager.IncrementScore();
        spawnedTargets--;
        GameObject.Find("ScoreText").GetComponent<Text>().text = gameManager.GetScore().ToString();
    }
}
