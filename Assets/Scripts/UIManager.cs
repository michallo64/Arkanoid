using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text Target;
    public Text ScoreText;
    public Text LivesTexts;
    public int Score { get; set; }

    private void Awake()
    {
        Brick.OnBrickDestruction += OnBrickDestruction;
        BricksManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLiveLost += OnLiveLost;
    }
    private void Start()
    {
    
        OnLiveLost(GameManager.Instance.AvailableLives); 
    }

    private void OnLiveLost(int remainingLives)
    {
        LivesTexts.text = $"LIVES: {remainingLives}";        
    }

    private void OnLevelLoaded()
    {
        UpdateRemainingBricksText();
        UpdateScoreText(0);
    }

    private void UpdateScoreText(int increments)
    {
        this.Score += increments;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = $@"SCORE:
{scoreString}";
    }

    private void OnBrickDestruction(Brick obj)
    {
        UpdateRemainingBricksText();
        UpdateScoreText(10);
    }

    private void UpdateRemainingBricksText()
    {
        Target.text = $@"Target:
{BricksManager.Instance.RemainingBricks.Count} / {BricksManager.Instance.InitialBricksCount}";
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
        BricksManager.OnLevelLoaded -= OnLevelLoaded;
    }
}
