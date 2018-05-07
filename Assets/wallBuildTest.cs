using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using OpenCvSharp;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using NWH;

public class wallBuildTest : MonoBehaviour {
    private WallInstantiator wallBuilder;
	// Use this for initialization
	void Start () {
       wallBuilder = gameObject.GetComponent<WallInstantiator>();
	}
	
	// Update is called once per frame
	void Update () {
       /*
        //test vertical line with item1 < item 3
        Vec4i testVec0 = new Vec4i(2, 1, 2, 8);
        wallBuilder.CreateWallSegment(testVec0);    //distances for verticals should be 7
        //test vertical line with item1 > item 3
        Vec4i testVec1 = new Vec4i(4, 8, 4, 1);
        wallBuilder.CreateWallSegment(testVec1);
    
        //test horizontal line with item0 > item 2
        Vec4i testVec2 = new Vec4i(5, 10, 13, 10);  //distance for horizontals should be 8
        wallBuilder.CreateWallSegment(testVec2);
        //test horizontal line with item0 < item 2
        Vec4i testVec3 = new Vec4i(13, 12, 5, 12);
        wallBuilder.CreateWallSegment(testVec3);
        */
        
    }
}
