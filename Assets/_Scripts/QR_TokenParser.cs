using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using ZXing;
using ZXing.Client.Result;

public class QR_TokenParser : MonoBehaviour {
    private WebCamTexture camTexture;
    private Rect screenRect;
    private string decodedString;
    private GraphAccessToken token;
    private string jsonRequestData;

    // Use this for initialization
    void Awake ()
    {
        Debug.Log("Awoke in QR Reader Scene==!");
        //set decoded string to null
        decodedString = null;
        jsonRequestData = null;
        //instantiate new GraphAccessToken class
        token = new GraphAccessToken();
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;

        //Start up camera feed if texture is not null.
        if (camTexture != null)
        {
            
            camTexture.Play();
            Debug.Log("Started video feed==!");
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
            Debug.Log("Setting up barcode reader===!");
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if (result != null && decodedString == null)
            {
                Debug.Log("Getting the parsed result===!");
                //parse QR code result with ZXing result parser class
                var resSon = ResultParser.parseResult(result);
                //set it to string
                decodedString = resSon.ToString();
                //Use Unity built in JsonUtility.FromJson() method to deserialize the string
                token = (GraphAccessToken)JsonUtility.FromJson(decodedString, typeof(GraphAccessToken));

                Debug.Log("Parsed QR Code url: "+token.url);
                //send request to get full JSON from the network.
                StartCoroutine(GetTheJson(token));                
            }                
        }
        catch (Exception ex) { Debug.LogWarning(ex.Message); }
    }

    IEnumerator GetTheJson(GraphAccessToken token)
    {
        //Set up a GET request with url from the QR code data
        UnityWebRequest jsonRequest = UnityWebRequest.Get(token.url);
        //set the get request header to authentication header
        jsonRequest.SetRequestHeader("x-hololens-token", token.auth_token);
        //send the request
        yield return jsonRequest.Send();
        Debug.Log("Request sent with auth_token: "+token.auth_token);

        //Catch an error if something wrong happens and display in console.
        if (jsonRequest.isError)
        {
            Debug.Log(jsonRequest.error);
        }
        else
        {
            jsonRequestData = jsonRequest.downloadHandler.text;
            // Show results as text in console
            Debug.Log(jsonRequestData);

            //If the request brings back a response save the JSON array and load next scene.
            if (jsonRequestData != null)
            {
                changeScene();
            }
        }
    }

    public void changeScene()
    {
        //Save the JSON in PlayerPrefs preset.
        PlayerPrefs.SetString("JSON_graph_data", jsonRequestData);
        //Stop the camera.
        camTexture.Stop();
        
        //Load the visualiser scene
        SceneManager.LoadScene("VisualiserScene");
    }
}
