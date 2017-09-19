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

        JSONNode jNode = JSON.Parse(loadGraph);

        //split JSON node into JSON array.
        JSONArray cellArray = (JSONArray)jNode["graph"]["cells"];

        foreach (JSONNode cell in cellArray)
        {
            string cellType = cell["celltype"];

            switch (cellType)
            {
                case "actor":
                    //Debug.Log(cellType);
                    float posX = cell["position"]["x"]-600;
                    float posZ = cell["position"]["y"];
                    string objId = cell["elemID"];
                   // Debug.Log("pos X = "+posX+", and pos Z = "+posZ+". Object ID -> "+objId);

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
            Vector3 startPos = new Vector3();
            Vector3 endPos = new Vector3();
            //Debug.Log("Source ID ->"+link.sourceid);
           // Debug.Log("Target ID ->" + link.destid);
            for (int i = 0; i < objectList.Count; i++)
            {
               // Debug.Log("Object No."+i+" ID from object list: "+objectList[i].name);
               // Debug.Log("Object Li." + i + " ID from source.....:" + link.sourceid);
                if(link.sourceid.Equals(objectList[i].name))
                {
                    Debug.Log("Here's ur source ID : "+objectList[i].name);
                    startPos = objectList[i].transform.localPosition;
                    Debug.Log("Start position for this link : " + startPos);
                }
                else if (link.destid.Equals(objectList[i].name))
                {
                    Debug.Log("And here's ur dest ID : "+objectList[i].name);
                    endPos = objectList[i].transform.localPosition;
                    Debug.Log("Destination position for this link : " + endPos);
                }
                else
                {
                    continue;
                }                
            }
            drawRelationship(50, startPos, endPos);
        }
        ////render relationship
        //drawRelationship(50, objectList[0].transform.localPosition, objectList[1].transform.localPosition);
    }//end of LoadGraph method

    private LineRenderer lineRenderer;
    public Vector3[] linkPoints;
    //public int numPoints = 50;
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
        midPoint.y += 1;
        for (int i = 1; i <= numPoints; i++)
        {
            float t = i / (float)numPoints;
           // linkPoints[i - 1] = drawStraightLine(t, p0, p1);
            linkPoints[i-1] = drawCurvedRelation(t, p0, p1, midPoint);
           // Debug.Log("link points: "+linkPoints[i-1]);            
        }
        lineRenderer.SetPositions(linkPoints);        
        Debug.Log("Mid-Point -> " + midPoint);
    }

    //formulas to draw relations and associated with them functions ------ 
    //function to calculate midpoint between 2 objects
    private Vector3 calcMidPoint(Vector3 p0, Vector3 p1)
    {
        return (p0 + p1) / 2;
    }
    //formula to draw the straight line
    private Vector3 drawStraightLine(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    //draw arched relation
    //\mathbf {B} (t)=(1-t)^{2}\mathbf {P} _{0}+2(1-t)t\mathbf {P} _{1}+t^{2}\mathbf {P} _{2}{\mbox{ , }}0\leq t\leq 1.
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
