﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMousePos : MonoBehaviour {
    public GameObject mousePointer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mousePointer.transform.position = snapPosition(getWorldPoint());
	}
    public Vector3 getWorldPoint()
    {
        Camera cam = GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    public Vector3 snapPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);
        return snapped;

    }
    public GameObject getHitObject()
    {
        Camera cam = GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.name == "Plane")
            {
                return null;
            }
            return hit.transform.gameObject;
        }
        return null;
    }
}
