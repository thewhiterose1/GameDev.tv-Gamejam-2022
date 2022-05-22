using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private Light2D lightScript;

    private bool hoveringUp = true;

    void Start() { 
        lightScript = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("LightFlicker", 0f, 3f);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, Camera.main.nearClipPlane + 5f));
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, 0.01f);
        if (this.hoveringUp) {
            this.hoveringUp = false;
        }
        else {
            this.hoveringUp = true;
        }
    }

    void LightFlicker() {
        lightScript.intensity = Random.Range(0.7f, 1f);
    }

    void UpdateColor() {
        lightScript.color = Color.white;
    }
}
