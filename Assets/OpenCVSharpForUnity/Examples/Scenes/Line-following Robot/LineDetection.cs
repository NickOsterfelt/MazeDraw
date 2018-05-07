using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using NWH;

public class LineDetection : MonoBehaviour {

    public RobotController rc;
    public RenderTexture rendTex;
    public RawImage rawImg;
    private Texture2D tex2d;
    public Mat m;

    private void Start()
    {
		tex2d = new Texture2D(1, 1);
        m = new Mat();
    }

    // Update is called once per frame
    void Update () {
        CvConvert.RenderTextureToTexture2D(rendTex, ref tex2d);
        CvConvert.Texture2DToMat(tex2d, ref m);

        float midPoint = getMidPoint(2, ref m);

        CvConvert.MatToTexture2D(m, ref tex2d);
        rawImg.texture = tex2d;

        if (midPoint < -0.06f)
        {
            rc.RotateRight();
            rc.MoveForward();
        }
        else if (midPoint > 0.06f)
        {
            rc.RotateLeft();
            rc.MoveForward();
        }
        else
        {
            rc.MoveForward();
        }
    }


	private float getMidPoint(int bias, ref Mat mat)
	{
        Cv2.CvtColor(mat, mat, ColorConversionCodes.RGBA2GRAY);
        Cv2.Threshold(mat, mat, 100, 255, ThresholdTypes.BinaryInv);

        Point[][] cntPoints;
        HierarchyIndex[] hIndex;
        Cv2.FindContours(mat, out cntPoints, out hIndex, RetrievalModes.List, ContourApproximationModes.ApproxSimple);
		Cv2.CvtColor(m, m, ColorConversionCodes.GRAY2RGBA);

        float minMaxCx = (bias > 0 ? -Mathf.Infinity : Mathf.Infinity);

        foreach (Point[] point in cntPoints)
		{
            Moments mu = Cv2.Moments(point, false);

            if (mu.M00 > 100.0f)
			{
                OpenCvSharp.Rect r = Cv2.BoundingRect(point);
                Cv2.Rectangle(mat, r, new Scalar(0, 255, 128, 255), 3, LineTypes.Link8, 0);
                float cx;

				if (bias > 0)
				{
                    cx = r.X + r.Width - 12;
					if (cx > minMaxCx)
					{
                        minMaxCx = cx;
					}
				}
				else
				{
					cx = r.X + 12;
					if (minMaxCx > cx)
					{
						minMaxCx = cx;
					}
				}
			}
                            
		}

        if (float.IsInfinity(minMaxCx))
            minMaxCx = mat.Cols / 2;
		return 1.0f - 2.0f * minMaxCx / mat.Cols;
	}
	
}
