using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainScript : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject gameBoard;

    public GameObject knight;

    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    public Text scoreText;
    public Text highScoreText;

    public int playerScore;
    public int highScore;

    public int level;
    public int levelIncrements;

    public float gameSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateScoreLabels();
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
        this.knight.GetComponent<KnightScript>().canMove = false;
        this.gameBoard.GetComponent<MovingBoard>().canMove = false;

        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        this.knight.GetComponent<KnightScript>().canMove = false;
        this.gameBoard.GetComponent<MovingBoard>().canMove = false;

        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void Reset()
    {
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScore()
    {

    }

    public void SaveScore()
    {

    }

}
