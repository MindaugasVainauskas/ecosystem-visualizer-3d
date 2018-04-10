using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.Client.Result;

public class QR_TokenParser : MonoBehaviour {
    private WebCamTexture camTexture;
    private Rect screenRect;
    private string decodedString;

    // Use this for initialization
    void Start ()
    {
        decodedString = null;
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(),
              camTexture.width, camTexture.height);
            if (result != null && decodedString == null)
            {
                var resSon = ResultParser.parseResult(result);
                string jsonstring = resSon.ToString();
                //Debug.Log(jsonstring);
                GraphAccessToken token = new GraphAccessToken();
                token = (GraphAccessToken)JsonUtility.FromJson(jsonstring, typeof(GraphAccessToken));
                Debug.Log(token.url);
                Debug.Log(token.auth_token);
                // decodedString = result.Text;
                //Debug.Log("DECODED TEXT FROM QR: " +result.Text);
            }
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }
}
