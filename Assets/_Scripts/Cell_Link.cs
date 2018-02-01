using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to hold relationship info
[Serializable]
public class Cell_Link
{
    public string linkId;
    public string sourceId;
    public string targetId;
    public string relCol;

    public Cell_Link(string linkId, string sourceId, string targetId, string relCol)
    {
        this.linkId = linkId;
        this.sourceId = sourceId;
        this.targetId = targetId;
        this.relCol = relCol;
    }
}


