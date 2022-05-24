using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    public static GameManager instance;

    public GameObject[] playerList;
    public GameObject activePlayer;

    private void Start()
    {
        instance = new GameManager();
    }
}
