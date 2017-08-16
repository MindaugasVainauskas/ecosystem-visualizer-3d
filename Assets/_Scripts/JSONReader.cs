using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader {
    //load the text from resources folder
    public static string LoadJSon(string jsonPath)
    {
        //replace .json ending with nothing
        string jsonFile = jsonPath.Replace(".json", "");
        //create text asset
        TextAsset loadedJson = Resources.Load<TextAsset>(jsonFile);

        //return the text of text asset
        return loadedJson.text;
    }
}
