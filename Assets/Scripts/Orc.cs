using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Orc : MonoBehaviour, IAgent
{
    private GameObject[] playerList;
    private GameObject[] orcList;
    private GameObject NearestPlayer;
    private GameObject targetPlayer;
    private Hero playerStats;
    public Image healthUI;


    public float health { get; set; }
    public float baseAttack { get; set; }
    public float moveSpeed { get; set; }
    public float attackSpeed { get; set; } 
    public float maxHealth { get; set; }

    public enum State
    {
        Idle,
        Attack
    }
    public State currState { get; set; }

    private Vector3 idleVector { get; set; }
    private float timer { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        orcList = GameObject.FindGameObjectsWithTag("Enemy");
        GameManager.instance.orcList = orcList;
        playerList = GameObject.FindGameObjectsWithTag("Player");
        NearestPlayer = playerList[0];
        this.currState = State.Idle;
        this.moveSpeed = 0.003f;
        this.attackSpeed = 2f;
        this.baseAttack = 5;
        this.health = 15;
        this.maxHealth = this.health;
        idleVector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        orcList = GameObject.FindGameObjectsWithTag("Enemy");
        CheckNearestPlayer();

        if (currState == State.Attack)
        {
            this.AttackNearest();
        }
        else if (currState == State.Idle)
        {
            this.Idle();
        }
        this.UpdateHealth(this.health, this.maxHealth);

        if (health <= 0)
        {
            this.Die();
        }
        GameManager.instance.orcList = orcList;
    }

    public void UpdateHealth(float health, float maxHealth)
    {   
        healthUI.fillAmount = (health / maxHealth);
    }

    public void CheckNearestPlayer()
    {
        if (NearestPlayer != null)
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                if (Vector3.Distance(playerList[i].transform.position, transform.position) < (Vector3.Distance(NearestPlayer.transform.position, transform.position)))
                {
                    NearestPlayer = playerList[i];
                }
            }
            if (Vector3.Distance(transform.position, NearestPlayer.transform.position) < 15)
            {
                currState = State.Attack;
            }
            else
            {
                currState = State.Idle;
            }
        }
        else
        {
            NearestPlayer = playerList[0];
        }
    }

    public void Idle() 
    {
        if (Vector3.Distance(idleVector, transform.position) < 0.5)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                idleVector = new Vector3(transform.position.x + Random.Range(-4, 4), transform.position.y + Random.Range(-4, 4));
                timer = 0;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, idleVector, moveSpeed);
        }
    }

    public void AttackNearest()
    {
        if (playerList.Length > 0)
        {

            if (Vector3.Distance(NearestPlayer.transform.position, transform.position) > 1.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, NearestPlayer.transform.position, this.moveSpeed);
            }
            else
            {
                DefaultAttack();

            }
        }
        else currState = State.Idle;
    }

    public void DefaultAttack()
    {
        timer += Time.deltaTime;
        if (timer > attackSpeed)
        {
            targetPlayer = NearestPlayer;
            playerStats = (Hero)targetPlayer.GetComponent("Hero");
            playerStats.health -= baseAttack;
            //Play attack animation
            timer = 0;
        }
    }

    public void Die()
    {
        Object.Destroy(gameObject);
    }

    public void OnCollisionStay(Collision collision)
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            idleVector = new Vector3( transform.position.x + Random.Range(-4, 4), transform.position.y + Random.Range(-4, 4));
            timer = 0;
        }
    }
}
