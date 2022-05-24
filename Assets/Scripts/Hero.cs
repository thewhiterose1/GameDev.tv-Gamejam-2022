using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour, IAgent
{
    private GameObject[] playerList;
    public GameObject activePlayer;

    public Image healthUI;

    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        activePlayer = playerList[0];
        this.moveSpeed = 5f;
        this.maxHealth = this.health;
        this.health = 100;
    }

    public int maxHealth { get; set; }
    public int health { get; set; }
    public int baseAttack { get; set; }
    public float moveSpeed { get; set; }

    // Update is called once per frame
    void Update()
    {
        this.PlayerController();
        if (health <= 0) {
            this.Die();
        }
    }

    public void UpdateHealth(int health, int maxHealth)
    {
        healthUI.fillAmount = (health / maxHealth) * 100;
    }

    public void PlayerController() {
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

        Debug.Log(activePlayer);
        Debug.Log(gameObject);
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
            if (Vector3.Distance(transform.position, activePlayer.transform.position) > 3.5f)
            {
                //Debug.Log(activePlayer.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, activePlayer.transform.position, 0.05f);
            }
        }
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
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
