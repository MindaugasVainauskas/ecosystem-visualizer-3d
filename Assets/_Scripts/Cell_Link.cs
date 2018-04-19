using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Meant to hold the relationship data when pulling it out of JSON array.
[Serializable]
public class Cell_Link
{
    public string linkId;
    public string sourceId;
    public string targetId;
    public string relCol;
    public string relText;
        
    public Cell_Link(string linkId, string sourceId, string targetId, string relCol, string relTxt)
    {
        this.linkId = linkId;
        this.sourceId = sourceId;
        this.targetId = targetId;
        this.relCol = relCol;
        this.relText = relTxt;
    }
}


