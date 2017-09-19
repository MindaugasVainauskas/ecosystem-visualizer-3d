using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cell
{
    public List<Cell_Actor>actors;
    public List<Cell_Link> links;
}
