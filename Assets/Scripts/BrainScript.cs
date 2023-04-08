using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainScript : MonoBehaviour
{
    public GameObject knight;
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

    public void tileGotClicked(GameObject sender)
    {
        var knightScript = knight.GetComponent<KnightScript>();
        var senderScript = sender.GetComponent<TileScript>();


        if (knightScript.knightCanMove(senderScript.boardX, senderScript.boardY) == true)
        {

            knightScript.boardX = senderScript.boardX;
            knightScript.boardY = senderScript.boardY;

            Vector3 newPosition = new Vector3(sender.GetComponent<Transform>().position.x,
                                        sender.GetComponent<Transform>().position.y,
                                        knight.GetComponent<Transform>().position.z);

            knight.GetComponent<Transform>().position = newPosition;
        }
    }

    

    


}
