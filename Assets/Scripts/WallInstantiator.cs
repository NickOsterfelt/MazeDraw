using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using OpenCvSharp;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using NWH;

public class WallInstantiator : MonoBehaviour {
    bool creating, deleting;
    ShowMousePos pointer;
    public GameObject polePrefab;
    public GameObject wallPrefab;

    GameObject lastPole;
	
    /*// Use this for initialization
	void Start () {
        pointer = GetComponent<ShowMousePos>();
	}
	
	// Update is called once per frame
	void Update () {
		 getInput();
	}
    void getInput()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
        //    startDelete();

        //}
        //else if(Input.GetMouseButtonUp(1))
        //{
        //    endDelete();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            startWall();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetWall();
        }
        else
        {
            if (creating)
            {
                UpdateWall();
            }
        }
    }
    void startWall()
    {
        creating = true;
        Vector3 startPos = pointer.getWorldPoint(); //get pointer starting position;
        startPos = pointer.snapPosition(startPos);
        GameObject startPole = Instantiate(polePrefab, startPos, Quaternion.identity); //create pole
        startPole.transform.position = new Vector3(startPos.x, startPos.y + 0.3f, startPos.z);
        lastPole = startPole;


    }
    //void startDelete()
    //{
    //    deleting = true;
    //    GameObject start = pointer.getHitObject();
    //    Destroy(start);
        

    //}
    //void endDelete()
    //{

    //}
    void SetWall()
    {
        creating = false;
    }
    void UpdateWall()
    {
        Vector3 current = pointer.getWorldPoint();
        current = pointer.snapPosition(current);
        
        if(!(current.x == lastPole.transform.position.x && current.z == lastPole.transform.position.z ))
        {
            createWallSegmentDraw(current);
        }
    }
    */
    public GameObject[] CreateWallSegment(Vec4i line)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((line.Item0 - line.Item2), 2) + Mathf.Pow((line.Item1 - line.Item3), 2));
        Vector3 start = new Vector3(0, 0, 0);
        // move to center of map
        line.Item1 = 12 - line.Item1;
        line.Item3 = 12 - line.Item3;
        line.Item0 += 10;
        line.Item1 += 10;
        line.Item2 += 10;
        line.Item3 += 10;
        // make return array
        int limit = (2 * Mathf.CeilToInt(distance)) + 1;
        GameObject[] returnArray = new GameObject [limit];
        // make vertical line
        if (line.Item0 == line.Item2)
        {
            if(line.Item1 < line.Item3)
            {
                for (int i = 0; i < distance; i++)
                {
                    GameObject newPole = Instantiate(polePrefab, start, Quaternion.identity);
                    newPole.transform.position = new Vector3(line.Item0, 0.3f, line.Item1 + i);
                    GameObject newWall = Instantiate(wallPrefab, start, Quaternion.identity);
                    newWall.transform.position = new Vector3(newPole.transform.position.x, newPole.transform.position.y, newPole.transform.position.z+0.5f);
                    newWall.transform.LookAt(newPole.transform);
                    returnArray[2 * i] = newPole;
                    returnArray[(2 * i) + 1] = newWall;
                    if (i + 1== distance)
                    {
                        GameObject lastPole = Instantiate(polePrefab, start, Quaternion.identity);
                        lastPole.transform.position = new Vector3(newPole.transform.position.x, newPole.transform.position.y, newPole.transform.position.z + 1.0f);
                        returnArray[(2 * i) + 2] = lastPole;
                    }
                }
            }
            else
            {
                for (int i = 0; i < distance; i++)
                {
                    GameObject newPole = Instantiate(polePrefab, start, Quaternion.identity);
                    newPole.transform.position = new Vector3(line.Item0, 0.3f, line.Item3 + i);
                    GameObject newWall = Instantiate(wallPrefab, start, Quaternion.identity);
                    newWall.transform.position = new Vector3(newPole.transform.position.x, newPole.transform.position.y, newPole.transform.position.z + 0.5f);
                    newWall.transform.LookAt(newPole.transform);
                    returnArray[2 * i] = newPole;
                    returnArray[(2 * i) + 1] = newWall;
                    if (i +1 == distance)
                    {
                        GameObject lastPole = Instantiate(polePrefab, start, Quaternion.identity);
                        lastPole.transform.position = new Vector3(newPole.transform.position.x, newPole.transform.position.y, newPole.transform.position.z + 1.0f);
                        returnArray[(2 * i) + 2] = lastPole;
                    }
                }
            }
        }
        //horizontal lines
        if (line.Item1 == line.Item3)
        {
            if (line.Item0 < line.Item2)
            {
                for (int i = 0; i < distance; i++)
                {
                    GameObject newPole = Instantiate(polePrefab, start, Quaternion.identity);
                    newPole.transform.position = new Vector3(line.Item0 + i, 0.3f, line.Item1);
                    GameObject newWall = Instantiate(wallPrefab, start, Quaternion.identity);
                    newWall.transform.position = new Vector3(newPole.transform.position.x + 0.5f, newPole.transform.position.y, newPole.transform.position.z);
                    newWall.transform.LookAt(newPole.transform);
                    returnArray[2 * i] = newPole;
                    returnArray[(2 * i) + 1] = newWall;
                    if (i +1 == distance)
                    {
                        GameObject lastPole = Instantiate(polePrefab, start, Quaternion.identity);
                        lastPole.transform.position = new Vector3(newPole.transform.position.x + 1.0f, newPole.transform.position.y, newPole.transform.position.z);
                        returnArray[(2 * i) + 2] = lastPole;
                    }
                }
            }
            else
            {
                for (int i = 0; i < distance; i++)
                {
                    GameObject newPole = Instantiate(polePrefab, start, Quaternion.identity);
                    newPole.transform.position = new Vector3(line.Item2 + i, 0.3f, line.Item1);
                    GameObject newWall = Instantiate(wallPrefab, start, Quaternion.identity);
                    newWall.transform.position = new Vector3(newPole.transform.position.x + 0.5f, newPole.transform.position.y, newPole.transform.position.z);
                    newWall.transform.LookAt(newPole.transform);
                    returnArray[2 * i] = newPole;
                    returnArray[(2 * i) + 1] = newWall;
                    if (i + 1 == distance)
                    {
                        GameObject lastPole = Instantiate(polePrefab, start, Quaternion.identity);
                        lastPole.transform.position = new Vector3(newPole.transform.position.x + 1.0f, newPole.transform.position.y, newPole.transform.position.z);
                        returnArray[(2 * i) + 2] = lastPole;
                    }
                }
            }
        }
        return returnArray;
        //diagonal lines?
    }
    void createWallSegmentDraw(Vector3 current)
    {
        GameObject newPole = Instantiate(polePrefab, current, Quaternion.identity);
        newPole.transform.position = new Vector3(current.x, current.y + 0.3f, current.z);
        Vector3 middle = Vector3.Lerp(newPole.transform.position, lastPole.transform.position, 0.5f);
        //lerp calculates point inbetween to positions, 0.5 means middle point
        GameObject newWall = Instantiate(wallPrefab, middle, Quaternion.identity);
        newWall.transform.LookAt(lastPole.transform);

        lastPole = newPole;
    }
}
