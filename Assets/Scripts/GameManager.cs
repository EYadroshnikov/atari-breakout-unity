using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
        InitGame();
    }

    private void InitGame()
    {
        this.score = 0;
        this.lives = 1;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;
        // foreach (TextMeshProUGUI item in Brick.FindObjectsOfType<TextMeshProUGUI>())
        // {
        //     item.text = this.score.ToString("000");
        // }
        FindAnyObjectByType<TextMeshProUGUI>().text = this.score.ToString();


        if (Finished())
        {
            LoadLevel(this.level + 1);
        }
    }

    private void ResetLevel()
    {
        this.ball.ResetPosition();
        this.paddle.ResetPosition();

        // for (int i = 0; i < this.bricks.Length; i++)
        // {
        //     this.bricks[i].Reset();
        // }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("FinishScene");
    }

    public void Miss()
    {
        this.lives--;

        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private bool Finished()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }
}
