using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovementController : MonoBehaviour {
    private Transform playerPos;

    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
        playerPos = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    private void FixedUpdate()
    {
        
       // Transform newPos = playerPos;
       /* 
        float forwardBackward = Input.GetAxis("Oculus_GearVR_RThumbstickX") * speed;
        float leftRight = Input.GetAxis("Oculus_GearVR_RThumbstickY") * speed;

        forwardBackward *= Time.deltaTime;
        leftRight *= Time.deltaTime;

        transform.Translate(leftRight, 0, forwardBackward);
        */
        
        float forward = Input.GetAxis("Oculus_GearVR_RIndexTrigger");
        forward *= Time.deltaTime;

        transform.Translate(0, 0, forward);
         
    }
}
