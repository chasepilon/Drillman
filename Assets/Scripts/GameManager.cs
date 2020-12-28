using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Making this a singleton
    public static GameManager Instance { get; private set; }

    [SerializeField]
    Text score;
    [SerializeField]
    Text fuelRemainingText;
    [SerializeField]
    Text gameOverText;

    [SerializeField]
    PlayerMovement controller;

    

    int gemsCollected;
    bool isGameOver;
    public int fuelRemaining;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }
    void Start()
    {
        isGameOver = false;
        gemsCollected = 0;
        score.text = "Gems: " + gemsCollected;
        fuelRemainingText.text = "Fuel Left: " + fuelRemaining;
    }

    private void FixedUpdate()
    {
        if(isGameOver)
        {
            controller.enabled = false;
            gameOverText.enabled = true;
        }
    }

    public void CollectGem(int value)
    {
        gemsCollected += value;
        score.text = "Gems: " + gemsCollected;
    }

    public void DecreaseFuelRemaining()
    {
        fuelRemaining--;
        if (fuelRemaining < 0)
            fuelRemaining = 0;

        fuelRemainingText.text = "Fuel Left: " + fuelRemaining;
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
