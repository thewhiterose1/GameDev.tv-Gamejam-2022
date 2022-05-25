using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{

    public static GameManager instance;

    public GameObject[] playerList;
    public GameObject activePlayer;
    public GameObject[] orcList;
    private void Awake()
    {
        instance = this;
    }
}
