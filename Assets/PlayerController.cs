using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 10.0f;
	// Use this for initialization
	void Start () {
        //turns off cursor and keeps it in game window
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    void FixedUpdate()
    {
        float forwardBackward = Input.GetAxis("Vertical") * speed;
        float leftRight = Input.GetAxis("Horizontal") * speed;
        forwardBackward *= Time.deltaTime;
        leftRight *= Time.deltaTime;

        transform.Translate(leftRight, 0, forwardBackward);


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
