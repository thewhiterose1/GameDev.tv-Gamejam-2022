using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour, IAgent
{
    private GameObject[] playerList;
    public GameObject activePlayer;
    private GameObject nearestOrc;
    private Orc orcStats;
    private float timer;

    public Image healthUI;

    public float maxHealth { get; set; }
    public float health { get; set; }
    public float baseAttack { get; set; }
    public float moveSpeed { get; set; }
    public float attackSpeed { get; set; }

    public enum State
    {
        Follow,
        Attack
    }


    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        activePlayer = playerList[0];
        GameManager.instance.activePlayer = activePlayer;
        this.moveSpeed = 1f;
        this.health = 100;
        this.maxHealth = this.health;
        this.baseAttack = 5f;
        this.attackSpeed = 2f;
        this.currState = State.Follow;
        Debug.Log(maxHealth + gameObject.name);
    }

    public State currState { get; set; }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(currState);
        this.CheckNearestOrc();
        this.PlayerController();
        this.UpdateHealth(this.health, this.maxHealth);
        if (health <= 0) {
            this.Die();
        }
    }

    public void CheckNearestOrc()
    {
        if (nearestOrc != null) {
            for (int i = 0; i < GameManager.instance.orcList.Length; i++)
            {
                if (Vector3.Distance(GameManager.instance.orcList[i].transform.position, transform.position) < (Vector3.Distance(nearestOrc.transform.position, transform.position)))
                {
                    nearestOrc = GameManager.instance.orcList[i];
                }
            }
        }
        else
        {
            nearestOrc = GameManager.instance.orcList[0];
        }
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        healthUI.fillAmount = (health / maxHealth);
    }

    public void PlayerController() {
        if (Input.GetKeyDown(KeyCode.I))
        {
            switch (currState)
            {
                case State.Attack:
                    currState = State.Follow;
                    break;
                case State.Follow:
                    currState = State.Attack;
                    break;
            }
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P");

            if (activePlayer == playerList[playerList.Length - 1])
            {
                activePlayer = playerList[0];
            }

            else
            {
                for (int i = 0; i < playerList.Length - 1; i++)
                {
                    if (activePlayer == playerList[i])
                    {
                        activePlayer = playerList[i + 1];
                        break;
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("O");
            if (activePlayer == playerList[0])
            {
                activePlayer = playerList[playerList.Length - 1];
            }

            else
            {
                for (int i = 0; i < playerList.Length; i++)
                {
                    if (activePlayer == playerList[i])
                    {
                        activePlayer = playerList[i - 1];
                        break;
                    }
                }
            }
        }
        if (nearestOrc != null)
        {
            if (Vector3.Distance(nearestOrc.transform.position, transform.position) < 1.6f)
            {
                DefaultAttack();
            }
        }

        if (activePlayer == gameObject)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(this.moveSpeed * inputX, 0, this.moveSpeed * inputY);

            movement *= Time.deltaTime;

            transform.Translate(movement);
        }
        else
        {
            if (nearestOrc != null)
            {
                if (currState == State.Follow | (currState == State.Attack & Vector3.Distance(nearestOrc.transform.position, transform.position) > 5f))
                {
                    if (Vector3.Distance(transform.position, activePlayer.transform.position) > 3.5f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, activePlayer.transform.position, 0.004f);
                    }
                }
                else
                {
                    if (Vector3.Distance(nearestOrc.transform.position, transform.position) > 1.5)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, nearestOrc.transform.position, 0.004f);
                    }
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, activePlayer.transform.position) > 3.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, activePlayer.transform.position, 0.004f);
                }
            }
        }
        GameManager.instance.activePlayer = activePlayer;
        playerList = GameObject.FindGameObjectsWithTag("Player");
    }

    public void AttackNearest()
    {
        throw new System.NotImplementedException();
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void DefaultAttack()
    {
       timer += Time.deltaTime;
        if (timer > attackSpeed)
        {
            orcStats = (Orc)nearestOrc.GetComponent("Orc");
            orcStats.health -= baseAttack;
            //Play attack animation
            timer = 0;
        }
    }

    public void Die()
    {
        Object.Destroy(gameObject);
    }
}
