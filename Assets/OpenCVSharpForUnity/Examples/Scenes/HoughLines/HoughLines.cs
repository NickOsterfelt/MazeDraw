using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using NWH;

public class HoughLines : MonoBehaviour
{
	private RawImage rawImage;
	private WebCamTexture webCamTexture;
	private Texture2D tex;
	private Mat mat, gray;

	void Start()
	{
        rawImage = GameObject.Find("Canvas/RawImage").GetComponent<RawImage>();

		webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name);
		webCamTexture.Play();

		tex = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGBA32, false);
		mat = new Mat(webCamTexture.height, webCamTexture.width, MatType.CV_8UC4);
        gray = new Mat(webCamTexture.height, webCamTexture.width, MatType.CV_8UC1);
	}

	void Update()
	{
		if (webCamTexture.didUpdateThisFrame && webCamTexture.isPlaying)
		{
			CamUpdate();
		}
	}

	void CamUpdate()
	{
		CvUtil.GetWebCamMat(webCamTexture, ref mat);

        Cv2.CvtColor(mat, gray, ColorConversionCodes.RGBA2GRAY);
		Cv2.Canny(gray, gray, 40, 210, 3, false);

		// Run Standard Hough Transform 
		LineSegmentPolar[] segStd = Cv2.HoughLines(gray, 1, Mathf.PI / 180, 50, 0, 0);
		int limit = Mathf.Min(segStd.Length, 10);
		for (int i = 0; i < limit; i++)
		{
			// Draws result lines
			float rho = segStd[i].Rho;
			float theta = segStd[i].Theta;
			double a = Mathf.Cos(theta);
			double b = Mathf.Sin(theta);
			double x0 = a * rho;
			double y0 = b * rho;
            Point pt1 = new Point { X = (int)Mathf.Round((float)(x0 + 1000 * (-b))), 
                Y = (int)Mathf.Round((float)(y0 + 1000 * (a))) };
			Point pt2 = new Point { X = (int)Mathf.Round((float)(x0 - 1000 * (-b))), 
                Y = (int)Mathf.Round((float)(y0 - 1000 * (a))) };
			mat.Line(pt1, pt2, new Scalar(0, 255, 0, 127), 3, LineTypes.AntiAlias, 0);
		}

		CvConvert.MatToTexture2D(mat, ref tex);
		rawImage.texture = tex;
	}

	private void OnDestroy()
	{
		webCamTexture.Stop();
	}
}