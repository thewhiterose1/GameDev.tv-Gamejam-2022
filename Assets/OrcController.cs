using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcController : MonoBehaviour
{
    private GameObject[] playerList;
    private GameObject NearestPlayer;
    public float speed = 0.003f;

    // Start is called before the first frame update
    void Start()
    {
      playerList = GameObject.FindGameObjectsWithTag("Player");
      NearestPlayer = playerList[0];
    }

    // Update is called once per frame
    void Update()
    {
       for (int i = 0; i < playerList.Length-1; i++)
        {
            if (Vector3.Distance(playerList[i].transform.position, transform.position) < (Vector3.Distance(NearestPlayer.transform.position, transform.position)))
            {
                NearestPlayer = playerList[i];
            }
                
        }
        if (Vector3.Distance(NearestPlayer.transform.position, transform.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, NearestPlayer.transform.position, speed);
        }
    }
}
