using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrainScript : MonoBehaviour
{
    public GameObject gameBoard;

    public GameObject knight;

    public Text scoreText;
    public Text highScoreText;

    public Text gameOverMessage;

    public int playerScore;
    public int highScore;

    public bool hasLost;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        LoadScore();

        gameOverMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // updating score (front and backend)
        UpdateScore();
        UpdateScoreLabels();

        // checking if f or p button is pressed to pause the game (FREEZE!)
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
            Debug.Log("The 'f' or 'p' key was pressed.");
        }
    }

    // updating the score on the backend
    public void UpdateScore()
    {
        // checking to see if the player has gotten any higher
        this.playerScore = knight.GetComponent<KnightScript>().highestBoardY;

        // if the player score is bigger than the high score, set it to the high score!
        if (playerScore > highScore)
        {
            highScore = playerScore;
        }
    }

    // updating the score labels (frontend)
    public void UpdateScoreLabels()
    {
        scoreText.text = playerScore.ToString();
        highScoreText.text = highScore.ToString();
    }

    // if we lose :(
    public void Lose()
    {
        hasLost = true;

        // making sure the knight can't move
        this.knight.GetComponent<KnightScript>().canMove = false;
        this.gameBoard.GetComponent<MovingBoard>().canMove = false;

        // show the gameover message
        gameOverMessage.gameObject.SetActive(true);
    }

    // pauses the game
    public void Pause()
    {
        // if we haven't lost, and we aren't paused, PAUSE
        if (hasLost != true && isPaused == false)
        {
            isPaused = true;

            this.knight.GetComponent<KnightScript>().canMove = false;
            this.gameBoard.GetComponent<MovingBoard>().canMove = false;
        }
        // if we're already paused, RESUME
        else if (isPaused == true)
        {
            Resume();
        }
    }

    // resumes the game (from a pause)
    public void Resume()
    {
        // so long as we haven't lost yet...RESUME
        if (hasLost != true)
        {
            isPaused = false;
            this.knight.GetComponent<KnightScript>().canMove = true;
            this.gameBoard.GetComponent<MovingBoard>().canMove = true;
        }
    }

    // resets the scene
    public void Reset()
    {
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // loads high score from the playerPrefs
    public void LoadScore()
    {
        highScore = PlayerPrefs.GetInt("highScore");

    }

    // save the score to the player prefs
    public void SaveScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }

    // changes the scene to the home scene
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

}
