﻿using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLoader : MonoBehaviour
{
    public Graph ecosystem;
   // public BezierRenderer relationLine;
    public List<GameObject> objectList;
    public List<Cell_Link> relations;

    //lists of link start points and end points
    private List<Vector3> linkStartPos;
    private List<Vector3> linkEndPos;
    private float _graphScale = 5;
    public GameObject ParentObject; //Parent object holding graph objects inside it.
    private Vector3 startPos;
    private Vector3 endPos;

    //gets called before start. Called once per object lifetime
    void Awake()
    {
        LoadGraph("Ecosystem4.json");
       // LoadGraph("MoreComplex.json");
        //LoadGraph("Simple.json");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Declarations of transforms for gameobjects
    public Transform sphere;
    public Transform cube;
    public Transform diamond;
    public Transform hex3d;
    public Transform pyramid;
    public Transform arrowUp;
    public Transform arrowDown;
    public Transform plus3d;

    //method to load the graph from json file
    public void LoadGraph(string path)
    {
        ParentObject = GameObject.Find("ParentObject");
        ParentObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); //Local scale of parent object
        objectList = new List<GameObject>();
        string loadGraph = JSONReader.LoadJSon(path);

        linkStartPos = new List<Vector3>();
        linkEndPos = new List<Vector3>();

        relations = new List<Cell_Link>();

        JSONNode jNode = JSON.Parse(loadGraph);

        //split JSON node into JSON array.
        JSONArray cellArray = (JSONArray)jNode["cells"];
        

        foreach (JSONNode cell in cellArray)
        {
            string cellType = cell["celltype"];

            switch (cellType)
            {
                case "actor":
                    float posX = cell["position"]["x"]-750;
                    float posZ = ((-1)*cell["position"]["y"])+750;
                    string objId = cell["id"];

                    string objectShape = cell["type"];
                    
                    switch (objectShape)
                    {
                        case "basic.InteractiveCircle":
                            instantiateObject(sphere, posX, posZ, objId);
                            break;
                        case "basic.InteractiveRect":
                            instantiateObject(cube, posX, posZ, objId);
                            break;
                        case "basic.InteractiveDiamond":
                            instantiateObject(diamond, posX, posZ, objId);
                            break;
                        case "basic.InteractiveHex":
                            instantiateObject(hex3d, posX, posZ, objId);
                            break;
                        default:
                            break;
                    }                    
                    break;

                //If celltype is relationship then add its fill colour to the list of colours.
                case "relationship":
                    string id = cell["id"];
                    string sourceId = cell["source"]["id"];
                    string targetId = cell["target"]["id"];
                    string relCol = cell["attrs"][".connection"]["stroke"];
                    //Add the variables of a relationship into a Cell_Link object.
                    Cell_Link tempLink = new Cell_Link(id, sourceId, targetId, relCol);

                    //Add the relationship to relations list.
                    relations.Add(tempLink);
                    break;

                default:
                    break;
            }//end of switch(cellType)
        }//end of forEach(JSONNode cell in cellArray)

        foreach (var link in relations)
        {
            //create 2 vector3 objects to hold start and end positions for every relationship
            startPos = new Vector3();
            endPos = new Vector3();

            GameObject StartObj = new GameObject();
            GameObject EndObj = new GameObject();
            for (int i = 0; i < objectList.Count; i++)
            {
                if(link.sourceId.Equals(objectList[i].name))
                {
                    startPos = objectList[i].transform.localPosition;
                    StartObj = objectList[i];
                    StartObj.transform.parent = objectList[i].transform;
                }
                else if (link.targetId.Equals(objectList[i].name))
                {
                    endPos = objectList[i].transform.localPosition;
                    EndObj = objectList[i];
                    EndObj.transform.parent = objectList[i].transform;
                }
                else
                {
                    continue;
                }                
            }
            //render relationship with colour pulled from relationship list.
            //drawRelationship(50, startPos, endPos, link.relCol);
            drawRelationship(StartObj, EndObj, link.relCol);
        }//end of foreach method
    }//end of LoadGraph method

    private LineRenderer lineRenderer;
    public Vector3[] linkPoints;
    private GameObject tempObj;

    //drawing the relationship between nodes
    //public void drawRelationship(int numPoints, Vector3 p0, Vector3 p1, string relColour)
    //{
      public void drawRelationship(GameObject sObj, GameObject eObj, string relColour)
      {
        int numPoints = 50;
        //Following takes a Hex string colour value from JSON file and applies it to the new relation.
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString(relColour, out myColor);

        Vector3 p0 = sObj.transform.localPosition;
        Vector3 p1 = eObj.transform.localPosition;
        Debug.Log("Start pos - "+p0);
        Debug.Log("End pos - "+p1);
        Vector3 midPoint = new Vector3();
        tempObj = new GameObject();
        tempObj.transform.parent = ParentObject.transform; //add as a child of parent node.
        lineRenderer = tempObj.AddComponent<LineRenderer>();        
        lineRenderer.material.color = myColor;
        lineRenderer.widthMultiplier = _graphScale/5; //relationship width is a margin of the scale of the graph objects.
        linkPoints = new Vector3[numPoints];
        lineRenderer.positionCount = 50;
        midPoint = calcMidPoint(p0, p1);
        
        for (int i = 0; i < linkStartPos.Count; i++)
        {
            if (linkStartPos.Contains(p0) && linkEndPos.Contains(p1))
            {
                midPoint.y += _graphScale; //adjusted height of midpoint due to smaller dimensions
                break;
            }
            
        }       

        //add positions to start and end position lists
        linkStartPos.Add(p0);
        linkEndPos.Add(p1);
        for (int i = 1; i <= numPoints; i++)
        {
            float t = i / (float)numPoints;
            linkPoints[i-1] = drawCurvedRelation(t, p0, p1, midPoint);    
        }
        lineRenderer.SetPositions(linkPoints);
    }

    //formulas to draw relations and associated with them functions ------ 
    //function to calculate midpoint between 2 objects
    private Vector3 calcMidPoint(Vector3 p0, Vector3 p1)
    {
        return (p0 + p1) / 2;
    }

    //draw arched relation
    //\mathbf {B} (t)=(1-t)^{2}\mathbf {P} _{0}+2(1-t)t\mathbf {P} _{1}+t^{2}\mathbf {P} _{2}{\mbox{ , }}0\leq t\leq 1. //taken straight from might Wikipedia
    private Vector3 drawCurvedRelation(float t, Vector3 p0, Vector3 p1, Vector3 midPoint) {
        Vector3 temp = new Vector3();
        float r1 = 1 - t;
        float tt = t * t;
        float r2 = r1 * r1;
        temp = r2 * p0 + 2 * r1 * t * midPoint + tt * p1;
        return temp;
    }

    //this is an reusable method to instantiate transform objects from prefabs.
    void instantiateObject(Transform shape, float posX, float posZ, string objId) {
        Transform clone = Instantiate(shape, new Vector3(posX/7, -3.5f, posZ/7), Quaternion.identity);
        clone.name = "" + objId;
        clone.parent = ParentObject.transform;
        objectList.Add(clone.gameObject);
    }  
}
