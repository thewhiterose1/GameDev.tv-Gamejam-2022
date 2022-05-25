using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 cameraLoc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraLoc = new Vector3(GameManager.instance.activePlayer.transform.position.x, GameManager.instance.activePlayer.transform.position.y - 6.4f, -5.401964f);
        transform.position = Vector3.MoveTowards(transform.position, cameraLoc, 1f);    
    }
}
