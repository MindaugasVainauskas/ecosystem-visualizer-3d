using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to hold Graph node info
[Serializable]
public class Cell_Actor
{
    public string type;
    public Size size;
    public Position position;
    public int angle;
    public string celltype;
    public string asset;
    public string id;
    public string elemID;
    public int z;
    public Attributes attrs;
}

//class to hold cell attributes(colour/text)
[Serializable]
public class Attributes
{
    public Outer outer;
    public Inner inner;
    public Text text;
}

[Serializable]
public class Text
{
    public string text; //text inside cell
    public string fill; // colour of text
}

//inner attribute info
[Serializable]
public class Inner
{
    public string fill;
}

//outer attribute info
[Serializable]
public class Outer
{
    public string fill;
    public bool magnet;
}

//class for holding size info of a cell
[Serializable]
public class Size
{
    public float width;
    public float height;
}

//class to hold position info of a cell
[Serializable]
public class Position
{
    public float x;
    public float y;
}