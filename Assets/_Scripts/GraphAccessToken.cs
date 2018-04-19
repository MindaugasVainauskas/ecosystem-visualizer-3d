using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In this serializable class, QR code data that gets parsed by ZXing library is defined.
[Serializable]
public class GraphAccessToken{
    public string url;
    public string auth_token;
}
