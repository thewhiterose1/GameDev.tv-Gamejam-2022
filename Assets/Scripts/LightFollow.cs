using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    private GameObject[] playerList;
    private Vector3 moveTo;
    public float speed = 0.003f;
    private enum BobState
    {
        stateUp, stateDown
    }
    private BobState state;
    private int frames = 0;


    // Start is called before the first frame update
    void Start()
    {
        state = BobState.stateUp;
        playerList = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        frames = frames + 1;
        moveTo = new Vector3(0,0,0);
        foreach ( GameObject player in playerList)
        {
            moveTo = moveTo + player.transform.position * (1f/playerList.Length);
        };
        if (frames == 300)
        {


            switch (state)
            {
                case BobState.stateUp:
                    state = BobState.stateDown;
                    frames = 0;
                    break;
                case BobState.stateDown:
                    state = BobState.stateUp;
                    moveTo = new Vector3(moveTo.x, moveTo.y, moveTo.z - 0.05f);
                    frames = 0;
                    break;
            }
        }    
        if (state == BobState.stateDown)
        {
          moveTo = new Vector3(moveTo.x, moveTo.y, moveTo.z);
        }
        else
        {
          moveTo = new Vector3(moveTo.x, moveTo.y, moveTo.z + 0.025f);
        }

        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed);
    }
}