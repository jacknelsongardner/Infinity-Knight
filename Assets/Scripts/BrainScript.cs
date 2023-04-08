using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainScript : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject gameBoard;

    public int playerScore;

    public int level;
    public int levelIncrements;

    public float gameSpeed;

    private int WIN;
    private int LOSE;
    private int UNDETERMINED;

    // Start is called before the first frame update
    void Start()
    {

        WIN = 1;
        LOSE = 0;
        UNDETERMINED = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lose()
    {

    }

    public void Pause()
    {

    }

}
