using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour, IAgent
{
    private GameObject[] playerList;
    private GameObject NearestPlayer;

    public int health { get; set; }
    public int baseAttack { get; set; }
    public float moveSpeed { get; set; }

    public enum State
    {
        Idle,
        Attack
    }
    public State currState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        NearestPlayer = playerList[0];
        this.currState = State.Attack;
        this.moveSpeed = 0.003f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currState == State.Attack)
        {
            this.AttackNearest();
        }
        else if (currState == State.Idle)
        {

        }
    }

    public void Idle() 
    {
        throw new System.NotImplementedException();
    }

    public void AttackNearest()
    {
        for (int i = 0; i < playerList.Length - 1; i++)
        {
            if (Vector3.Distance(playerList[i].transform.position, transform.position) < (Vector3.Distance(NearestPlayer.transform.position, transform.position)))
            {
                NearestPlayer = playerList[i];
            }

        }
        if (Vector3.Distance(NearestPlayer.transform.position, transform.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, NearestPlayer.transform.position, this.moveSpeed);
        }
    }

    public void DefaultAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
