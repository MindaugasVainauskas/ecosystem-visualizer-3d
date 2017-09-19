using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to hold relationship info
[Serializable]
public class Cell_Link
{
    public string type;
    public Source source;
    public Target target;
    public bool allowExtend;
    public bool smooth;
    public Label[] labels;
    public string cellType;
    public string id;
    public string embeds;
    public int z;
    public string relID;
    public Vertice[] vertices;
    public LinkAttributes attribs;
}

[Serializable]
public class LinkAttributes
{
    public Connection connection;
    public MarkerVertices marker_vertices;
    public MarkerTarget marker_target;
}

[Serializable]
public class MarkerTarget
{
    public string fill;
    public string stroke;
    public string d;
}

[Serializable]
public class MarkerVertices
{
    public string display;
}

//class to hold data for connection of relationship
[Serializable]
public class Connection
{
    public int strokeWidth;
    public string stroke;
    public string dataCategory;
}

//class to hold vertice info of a relationship
[Serializable]
public class Vertice
{
    public float x;
    public float y;
}

//label info for relationship
[Serializable]
public class Label
{
    public float position;
    public LabelAttribute attributes;
}

//class to hold label attributes of relationship
[Serializable]
public class LabelAttribute
{
    public LinkText text;
}

//class to hold link text
[Serializable]
public class LinkText
{
    public string text;
}

//class to hold target id data for relationship
[Serializable]
public class Target
{
    public string id;
}

//class to hold source info for a relationship
[Serializable]
public class Source
{
    public string id;
    public string selector;
    public string port; // Need to look at this one. not sure what type it actually is!!!
}