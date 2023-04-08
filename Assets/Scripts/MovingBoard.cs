using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoard : MonoBehaviour
{

    public GameObject spawnTile;
    public bool canMove;

    public float baseMovementSpeed;
    public float speedIncreaseFactor;


    // Start is called before the first frame update
    void Start()
    {
        speedIncreaseFactor = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            // move the entire game board (including peices) down
            Vector3 position = targetGameObject.transform.position;
            position.y -= baseMovementSpeed * speedIncreaseFactor * Time.deltaTime;
            targetGameObject.transform.position = position;
        }
        else 
        { 
            // do nothing   
        }
    }

    public void IncrementSpeedIncreaseFactor()
    {
        speedIncreaseFactor = speedIncreaseFactor * speedIncreaseFactor + .3;
    }

    public void IncrementSpeedIncreaseFactor(float increaseToIncrease)
    {
        speedIncreaseFactor = speedIncreaseFactor * speedIncreaseFactor + increaseToIncrease;
    }
}
