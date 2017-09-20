using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLoader : MonoBehaviour
{
    public Graph ecosystem;
   // public BezierRenderer relationLine;
    public List<GameObject> objectList;

    //lists of link start points and end points
    private List<Vector3> linkStartPos;
    private List<Vector3> linkEndPos;
    //gets called before start. Called once per object lifetime
    void Awake()
    {
        LoadGraph("MoreComplex.json");
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
        objectList = new List<GameObject>();
        string loadGraph = JSONReader.LoadJSon(path);

        linkStartPos = new List<Vector3>();
        linkEndPos = new List<Vector3>();

        JSONNode jNode = JSON.Parse(loadGraph);

        //split JSON node into JSON array.
        JSONArray cellArray = (JSONArray)jNode["graph"]["cells"];

        foreach (JSONNode cell in cellArray)
        {
            string cellType = cell["celltype"];

            switch (cellType)
            {
                case "actor":
                    float posX = cell["position"]["x"]-650;
                    float posZ = ((-1)*cell["position"]["y"])+700;
                    string objId = cell["elemID"];

                    string objectShape = cell["type"];
                    
                    switch (objectShape)
                    {
                        case "basic.InteractiveCircle":
                            instantiateSphere(posX, posZ, objId);
                            break;
                        case "basic.InteractiveRect":
                            instantiateCube(posX, posZ, objId);
                            break;
                        case "basic.InteractiveDiamond":
                            instantiateDiamond(posX, posZ, objId);
                            break;
                        case "basic.InteractiveHex":
                            instantiateHex(posX, posZ, objId);
                            break;
                        default:
                            break;
                    }                    
                    break;
                    
                default:
                    break;
            }//end of switch(cellType)
        }//end of forEach(JSONNode cell in cellArray)

        ////this works on second and third parts of graph
        ecosystem = JsonUtility.FromJson<Graph>(loadGraph);

        //var elements = ecosystem.graphElements;
        var relations = ecosystem.graphRelationships;

        foreach (var link in relations)
        {
            //create 2 vector3 objects to hold start and end positions for every relationship
            Vector3 startPos = new Vector3();
            Vector3 endPos = new Vector3();
            for (int i = 0; i < objectList.Count; i++)
            {
                if(link.sourceid.Equals(objectList[i].name))
                {
                    startPos = objectList[i].transform.localPosition;
                }
                else if (link.destid.Equals(objectList[i].name))
                {
                    endPos = objectList[i].transform.localPosition;
                }
                else
                {
                    continue;
                }                
            }
            //render relationship
            drawRelationship(50, startPos, endPos);
        }//end of foreach method
    }//end of LoadGraph method

    private LineRenderer lineRenderer;
    public Vector3[] linkPoints;
    private GameObject tempObj;

    //drawing the relationship between nodes
    public void drawRelationship(int numPoints, Vector3 p0, Vector3 p1)
    {
        Vector3 midPoint = new Vector3();
        tempObj = new GameObject();
        lineRenderer = tempObj.AddComponent<LineRenderer>();
        lineRenderer.material.color = Color.green;
        lineRenderer.widthMultiplier = 0.1f;
        linkPoints = new Vector3[numPoints];
        lineRenderer.positionCount = 50;
        midPoint = calcMidPoint(p0, p1);
        
        for (int i = 0; i < linkStartPos.Count; i++)
        {
            if (linkStartPos.Contains(p0) && linkEndPos.Contains(p1))
            {
                Debug.Log("Now this is true!");
                midPoint.y += 1;
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
        //Debug.Log("Mid-Point -> " + midPoint);
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

    //instantiation of objects
    void instantiateSphere(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(sphere, new Vector3(posX/80, 0, posZ/80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiateCube(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(cube, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiateDiamond(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(diamond, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiateHex(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(hex3d, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiatePyramid(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(pyramid, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiateArrowUp(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(arrowUp, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiateArrowDown(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(arrowDown, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
    void instantiatePlus(float posX, float posZ, string objId)
    {
        Transform clone = Instantiate(plus3d, new Vector3(posX / 80, 0, posZ / 80), Quaternion.identity);
        clone.name = "" + objId;
        objectList.Add(clone.gameObject);
    }
}
