using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrainScript : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject gameBoard;

    public GameObject knight;

    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    public Text scoreText;
    public Text highScoreText;

    public Text gameOverMessage;

    public int playerScore;

    public int highScore;

    public int level;
    public int levelIncrements;

    public float gameSpeed;

    public int timesIncreasedSpeed;

    public bool hasLost;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        timesIncreasedSpeed = 1;
        LoadScore();

        gameOverMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateScoreLabels();

        IncreaseGameSpeed();

        // checking if f or p button is pressed to pause the game (FREEZE!)
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
            Debug.Log("The 'f' or 'p' key was pressed.");
        }
    }

    public void IncreaseGameSpeed()
    {
        if (playerScore >= timesIncreasedSpeed * 5)
        {
            gameBoard.GetComponent<MovingBoard>().IncrementSpeedIncreaseFactor(.25f);
            timesIncreasedSpeed += 1;
        }

    }
    public void UpdateScore()
    {
        this.playerScore = knight.GetComponent<KnightScript>().highestBoardY;

        if (playerScore > highScore)
        {
            highScore = playerScore;
        }
    }

    public void UpdateScoreLabels()
    {
        scoreText.text = playerScore.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void Lose()
    {
        hasLost = true;

        this.knight.GetComponent<KnightScript>().canMove = false;
        this.gameBoard.GetComponent<MovingBoard>().canMove = false;

        gameOverMessage.gameObject.SetActive(false);

        //gameOverMenu.SetActive(true);
        //pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if (hasLost != true && isPaused == false)
        {
            isPaused = true;

            this.knight.GetComponent<KnightScript>().canMove = false;
            this.gameBoard.GetComponent<MovingBoard>().canMove = false;
        }
        else if (isPaused == true)
        {
            Resume();
        }
        //pauseMenu.SetActive(true);
        //gameOverMenu.SetActive(false);
    }

    public void Resume()
    {
        if (hasLost != true)
        {
            isPaused = false;
            this.knight.GetComponent<KnightScript>().canMove = true;
            this.gameBoard.GetComponent<MovingBoard>().canMove = true;
        }
        //pauseMenu.SetActive(true);
        //gameOverMenu.SetActive(false);
    }


    public void Reset()
    {
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScore()
    {
        highScore = PlayerPrefs.GetInt("highScore");

    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

}
