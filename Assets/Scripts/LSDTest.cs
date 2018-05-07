using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using NWH;

public class LSDTest : MonoBehaviour
{
    private RawImage rawImage;
    private WebCamTexture webCamTexture;
    private Texture2D tex;
    private Mat mat, gray, mask, rgbIm;
    private LineSegmentDetector lsd;
    private float timeHolder;
    private Vec4f[] previous, final;
    private Scalar lowerRed = new Scalar(0, 0, 0);
    private Scalar upperRed = new Scalar(50, 50, 50);
    private bool camRun = false;

    // Modify these based on the grid we can see (hard coded scaling)
    private float squareHoriz = 16;
    private float squareVert = 11.6f;

    private float xScale, yScale;

    private MazeDraw mazeBuilder;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
           Debug.Log(devices[i].name);


        lsd = LineSegmentDetector.Create(LineSegmentDetectorModes.RefineNone);

        rawImage = GameObject.Find("Canvas/RawImage").GetComponent<RawImage>();

        webCamTexture = new WebCamTexture("HD Pro Webcam C920");
       //if(webCamTexture == null)
           // Debug.Log("null webcam");
        webCamTexture.Play();

        tex = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGBA32, false);
        mat = new Mat(webCamTexture.height, webCamTexture.width, MatType.CV_8UC4);
        rgbIm = new Mat(webCamTexture.height, webCamTexture.width, MatType.CV_8UC3);
        gray = new Mat(webCamTexture.height, webCamTexture.width, MatType.CV_8UC1);
        mask = new Mat(webCamTexture.height, webCamTexture.width, MatType.CV_8UC1);

        xScale = webCamTexture.width / squareHoriz;
        yScale = webCamTexture.height / squareVert;

        mazeBuilder = gameObject.GetComponent<MazeDraw>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            camRun = true;
        }
        if (camRun)
        {
            timeHolder += Time.deltaTime;
            if (webCamTexture.didUpdateThisFrame && webCamTexture.isPlaying)
            {
                CamUpdate();
            }
        }
    }

    void CamUpdate()
    {

        CvUtil.GetWebCamMat(webCamTexture, ref mat);
        Cv2.CvtColor(mat, rgbIm, ColorConversionCodes.RGBA2RGB);

        Cv2.InRange(rgbIm, lowerRed, upperRed, mask);

        //gray = mat & mask;

        //Cv2.ImShow("mask", mask);

        //Cv2.CvtColor(mat, gray, ColorConversionCodes.RGB2GRAY);
        Cv2.Canny(mask, gray, 40, 210, 3, false);

        // Run Standard Hough Transform 
        //LineSegmentPolar[] segStd = Cv2.HoughLines(gray, 1, Mathf.PI / 180, 50, 0, 0);
        //int limit = Mathf.Min(segStd.Length, 50);
        Vec4f[] lines;
        double[] width, prec, nfa;


        lsd.Detect(mask, out lines, out width, out prec, out nfa);
        previous = lines;

        if (timeHolder >= 1.0)
        {
            if (previous == null)
            {
                previous = lines;
            }
            else
            {
                List<Vec4f> temp = new List<Vec4f>();
                for (int i = 0; i < previous.Length; ++i)
                {
                    for (int j = 0; j < lines.Length; ++j)
                    {
                        bool x1 = (int)previous[i].Item0 == (int)lines[j].Item0;
                        bool y1 = (int)previous[i].Item1 == (int)lines[j].Item1;
                        bool x2 = (int)previous[i].Item2 == (int)lines[j].Item2;
                        bool y2 = (int)previous[i].Item3 == (int)lines[j].Item3;
                        if (x1 && x2 && y1 && y2)
                        {
                            temp.Add(previous[i]);
                        }
                    }
                }
                final = temp.ToArray();
                previous = lines;
                camRun = false;
            }
            timeHolder -= 1.0f;
        }


        int limit;
        if (final == null)
        {
            limit = 0;
        }
        else
        {
            limit = final.Length;
        }
        for (int i = 0; i < limit; i++)
        {
            Point pt1 = new Point { X = (int)final[i].Item0, Y = (int)final[i].Item1 };
            Point pt2 = new Point { X = (int)final[i].Item2, Y = (int)final[i].Item3 };
            mat.Line(pt1, pt2, new Scalar(0, 255, 0, 127), 3, LineTypes.AntiAlias, 0);

        }


        CvConvert.MatToTexture2D(mat, ref tex);
        rawImage.texture = tex;
        //Cv2.ImShow("gray", gray);
        Vec4i[] gridLines = ConvertLines(final);
        mazeBuilder.mazeUpdate(gridLines);

    }

    private Vec4i[] ConvertLines(Vec4f[] lines)
    {
        int len = lines.Length;
        Vec4i[] converted = new Vec4i[len];
        for (int i = 0; i < len; ++i)
        {
            // do stuff
            converted[i].Item0 = Mathf.RoundToInt(lines[i].Item0 / xScale);
            converted[i].Item1 = Mathf.RoundToInt(lines[i].Item1 / yScale);
            converted[i].Item2 = Mathf.RoundToInt(lines[i].Item2 / xScale);
            converted[i].Item3 = Mathf.RoundToInt(lines[i].Item3 / yScale);
            //wallBuilder.CreateWallSegment(converted[i]);
        }
        return converted;
    }

    private void OnDestroy()
    {
        webCamTexture.Stop();
    }
}