using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using NWH;

public class MazeDraw : MonoBehaviour {

    private Vec4i[] currentMaze;
    private GameObject[][] currentObjects;

    private WallInstantiator WallBuilder;

	// Use this for initialization
	void Start () {
        WallBuilder = gameObject.GetComponent<WallInstantiator>();
    }

    /*
    private void simplifyLines()
    {
        List<Vec4i> uniques = new List<Vec4i>();
        for (int i = 0; i < limit; ++i)
        {
            if (newMaze[i].Item0 == newMaze[i].Item2)
            {
                // horizontal line
                int distance = Math.Abs(newMaze[i].Item3 - newMaze[i].Item1);
                if (distance > 1)
                {
                    // line longer than 1 unit, split into smaller lines.
                }
                else
                {
                    // line is only 1 unit long, check for uniqueness then add
                }
            }
            else if (newMaze[i].Item1 == newMaze[i].Item3)
            {
                // vertical line
            }
        }
    }
    */

    public void mazeUpdate(Vec4i[] newMaze)
    {
        int limit = newMaze.Length;
        if(currentMaze == null)
        {
            // prepare to redraw
            currentMaze = newMaze;
            currentObjects = new GameObject[limit][];
        }
        else
        {
            int objClear = currentObjects.Length;
            for(int i = 0; i < objClear; ++i)
            {
                int tempLim = currentObjects[i].Length;
                for(int j = 0; j < tempLim; ++j)
                {
                    Destroy(currentObjects[i][j]);
                }
            }
            // compare old to new.
            currentMaze = newMaze;
            currentObjects = new GameObject[limit][];
        }
        for(int i = 0; i < limit; ++i)
        {
            currentObjects[i] = WallBuilder.CreateWallSegment(currentMaze[i]);
        }
    }
}
