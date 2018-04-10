using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ZXing;
using ZXing.Client.Result;

public class QR_TokenParser : MonoBehaviour {
    private WebCamTexture camTexture;
    private Rect screenRect;
    private string decodedString;
    private GraphAccessToken token;

    // Use this for initialization
    void Start ()
    {
        //set decoded string to null
        decodedString = null;
        //instantiate new GraphAccessToken class
        token = new GraphAccessToken();
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
                //parse QR code result with ZXing result parser class
                var resSon = ResultParser.parseResult(result);
                //set it to string
                decodedString = resSon.ToString();
                //Use Unity built in JsonUtility.FromJson() method to deserialize the string
                token = (GraphAccessToken)JsonUtility.FromJson(decodedString, typeof(GraphAccessToken));

                //check for output.
                Debug.Log(token.url);
                Debug.Log(token.auth_token);

                //send request to get full JSON.
                StartCoroutine(GetTheJson(token));
            }
            
                
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }

    IEnumerator GetTheJson(GraphAccessToken token)
    {
        UnityWebRequest jsonRequest = UnityWebRequest.Get("http://"+token.url);
        jsonRequest.SetRequestHeader("x-hololens-token", token.auth_token);
        yield return jsonRequest.Send();
        Debug.Log("Request sent with auth_token: "+token.auth_token);

        if (jsonRequest.isError)
        {
            Debug.Log(jsonRequest.error);
        }
        else
        {
            // Show results as text
            Debug.Log(jsonRequest.downloadHandler.text);
        }
    }
}
