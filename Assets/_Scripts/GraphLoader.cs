//using Newtonsoft.Json;
//using System;
//using System.IO;
//using System.Linq;
//using UnityEngine;

//public class GraphLoader : MonoBehaviour
//{
//GraphRootObject myGraphRoot;

//void Awake() {
//    LoadGraph("Simple.json");
//    readTheGraph();
//}

//private void LoadGraph(string path)
//{
//    string loadGraph = JSONReader.LoadJSon(path);

//    if (myGraphRoot == null) myGraphRoot = new GraphRootObject();

//    dynamic json = JsonConvert.DeserializeObject<GraphRootObject>(loadGraph);

//    myGraphRoot = json;
//}

//public void readTheGraph() {
//    string actorString;
//    string linkString;
//    for (int i = 0; i < myGraphRoot.graph.cells.Count(); i++)
//    {
//        switch (myGraphRoot.graph.cells[i].celltype)
//        {
//            case "actor":
//                {
//                    actorString = Environment.NewLine +
//                                    "celltype: " + myGraphRoot.graph.cells[i].celltype + Environment.NewLine +
//                                    "size: " + myGraphRoot.graph.cells[i].size.width + ", " + myGraphRoot.graph.cells[i].size.height + Environment.NewLine +
//                                    "outer fill: " + myGraphRoot.graph.cells[i].attrs.outer.fill + Environment.NewLine +
//                                    "attrs text text:" + myGraphRoot.graph.cells[i].attrs.text.text + Environment.NewLine +
//                                    "attrs text fill: " + myGraphRoot.graph.cells[i].attrs.text.fill + Environment.NewLine;

//                    Debug.Log(actorString);
//                    break;
//                }
//            case "relationship":
//                {
//                    linkString = Environment.NewLine +
//                                    "celltype: " + myGraphRoot.graph.cells[i].celltype + Environment.NewLine +
//                                    "source id: " + myGraphRoot.graph.cells[i].source.id + Environment.NewLine +
//                                    "target id: " + myGraphRoot.graph.cells[i].target.id + Environment.NewLine +
//                                    "attrs text text: " + myGraphRoot.graph.cells[i].labels[0].attrs.text.text + Environment.NewLine;
//                    Debug.Log(linkString);
//                    break;
//                }
//            default:
//                break;
//        }
//    }
//}


//private void updateScreenData()
//{
//    tblData.Text = tblData.Text + Environment.NewLine + "Data Retrieved...";

//    for (int i = 0; i < myGraphRoot.graph.cells.Count(); i++)
//    {
//        switch (myGraphRoot.graph.cells[i].celltype)
//        {
//            case "actor":
//                {
//                    tblData.Text = tblData.Text + Environment.NewLine +
//                                    "celltype: " + myGraphRoot.graph.cells[i].celltype + Environment.NewLine +
//                                    "size: " + myGraphRoot.graph.cells[i].size.width + ", " + myGraphRoot.graph.cells[i].size.height + Environment.NewLine +
//                                    "outer fill: " + myGraphRoot.graph.cells[i].attrs.outer.fill + Environment.NewLine +
//                                    "attrs text text:" + myGraphRoot.graph.cells[i].attrs.text.text + Environment.NewLine +
//                                    "attrs text fill: " + myGraphRoot.graph.cells[i].attrs.text.fill + Environment.NewLine;
//                    break;
//                }
//            case "relationship":
//                {
//                    tblData.Text = tblData.Text + Environment.NewLine +
//                                    "celltype: " + myGraphRoot.graph.cells[i].celltype + Environment.NewLine +
//                                    "source id: " + myGraphRoot.graph.cells[i].source.id + Environment.NewLine +
//                                    "target id: " + myGraphRoot.graph.cells[i].target.id + Environment.NewLine +
//                                    "attrs text text: " + myGraphRoot.graph.cells[i].labels[0].attrs.text.text + Environment.NewLine;
//                    break;
//                }
//            default:
//                break;
//        }
//    }


//}// end of GraphLoader

//    private async void loadData(string filePath)
//    {
//        if (myGraphRoot == null) myGraphRoot = new GraphRootObject();
//        StorageFolder storageFolder;
//        StorageFile storageFile;

//        string text;
//        try
//        {
//            storageFolder = ApplicationData.Current.LocalFolder;
//            storageFile = await storageFolder.GetFileAsync("Simple.Json");
//            text = await File.ReadTextAsync(storageFile);

//            //var result = JsonConvert.DeserializeObject<RootObject>(json);
//        dynamic json = JsonConvert.DeserializeObject<GraphRootObject>(text);

//        myGraphRoot = json;
//    }
//    catch (Exception ex)
//    {
//        tblData.Text = "Error: " + ex.Message;
//    }

//}

//    //private void cmdData_Click(object sender, RoutedEventArgs e)
//    //{
//    //    string filePath = "/Resources/Simple.Json";
//    //    tblData.Text = filePath;

//    //    loadData(filePath);
//    //    updateScreenData();

//    //}
//}







using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJSON;
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
       
        LoadGraph("Simple.json");
    }

    // Use this for initialization
    void Start()
    {
        // LoadGraph("MoreComplex.json");

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
        JSONArray innerJson = (JSONArray)jNode["graph"]["cells"];

        foreach (JSONNode cell in innerJson)
        {
            string cellType = cell["celltype"];

            switch (cellType)
            {
                case "actor":
                    Debug.Log(cellType);
                    float posX = cell["position"]["x"];
                    float posZ = cell["position"]["y"];
                    Debug.Log("pos X = "+posX+", and pos Z = "+posZ);

                    string objectShape = cell["type"];
                    switch (objectShape)
                    {
                        case "basic.InteractiveCircle":
                            instantiateSphere(posX, posZ);
                            break;
                        case "basic.InteractiveRect":
                            instantiateSphere(posX, posZ);
                            break;
                        case "basic.InteractiveDiamond":
                            instantiateSphere(posX, posZ);
                            break;
                        case "basic.InteractiveHex":
                            instantiateSphere(posX, posZ);
                            break;
                        default:
                            break;
                    }
                    
                    break;

                case "relationship":
                default:
                    break;
            }


           // Debug.Log(cellType);
        }
        
        //this works on second and third parts of graph
        ecosystem = JsonUtility.FromJson<Graph>(loadGraph);
       
        var elements = ecosystem.graphElements;
        var relations = ecosystem.graphRelationships;

        for (int i = 0; i < elements.Length; i++)
        {
            string elShape = elements[i].shape;
            string elId = elements[i].elemID;

            //instantiate game objects depending on their shape
            switch (elShape)
            {
                case "Circle":
                    //instantiateSphere(i);
                    Debug.Log(objectList[i].name + " pos: " + objectList[i].transform.position+";  Id of this obj --> "+elId);
                    break;
                case "Square":
                    instantiateCube(i);
                    break;
                case "Diamond":
                    instantiateDiamond(i);
                    break;
                case "Hex":
                    instantiateHex(i);
                    break;
                case "Pyramid":
                    instantiatePyramid(i);
                    break;
                case "ArrowUp":
                    instantiateArrowUp(i);
                    break;
                case "ArrowDown":
                    instantiateArrowDown(i);
                    break;
                case "Plus":
                    instantiatePlus(i);
                    break;
                default:
                    break;
            }

        }//end of for loop
        ////render relationship
        drawRelationship(50, objectList[0].transform.localPosition, objectList[1].transform.localPosition);
    }

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
    //void instantiateSphere(int x)
    //{
    //   Transform clone =  Instantiate(sphere, new Vector3(x * 2.0f, 1, 3), Quaternion.identity);
    //    clone.name = "sphere-" + x;
    //    objectList.Add(clone.gameObject);
    //}

    void instantiateSphere(float posX, float posZ)
    {
        Transform clone = Instantiate(sphere, new Vector3(posX/80, 1, posZ/80), Quaternion.identity);
       // clone.name = "sphere-" + x /;
        objectList.Add(clone.gameObject);
    }
    void instantiateCube(int x)
    {
        Transform clone = Instantiate(cube, new Vector3(0, 1, x + 1), Quaternion.identity);
        clone.name = "Cube-" + x;
        objectList.Add(clone.gameObject);
    }
    void instantiateDiamond(int x)
    {
        Transform clone = Instantiate(diamond, new Vector3(0, 1, x + 2), Quaternion.identity);
        clone.name = "Diamond-" + x;
        objectList.Add(clone.gameObject);
    }
    void instantiateHex(int x)
    {
        Transform clone = Instantiate(hex3d, new Vector3(0, 1, x + 3), Quaternion.identity);
        clone.name = "Hex-" + x;
        objectList.Add(clone.gameObject);
    }
    void instantiatePyramid(int x)
    {
        Transform clone = Instantiate(pyramid, new Vector3(0, 1, x + 4), Quaternion.identity);
        clone.name = "Pyramid-" + x;
        objectList.Add(clone.gameObject);
    }
    void instantiateArrowUp(int x)
    {
        Transform clone = Instantiate(arrowUp, new Vector3(0, 1, x + 5), Quaternion.identity);
        clone.name = "ArrowUp-" + x;
        objectList.Add(clone.gameObject);
    }
    void instantiateArrowDown(int x)
    {
        Transform clone = Instantiate(arrowDown, new Vector3(0, 1, x + 6), Quaternion.identity);
        clone.name = "ArrowDown-" + x;
        objectList.Add(clone.gameObject);
    }
    void instantiatePlus(int x)
    {
        Transform clone = Instantiate(plus3d, new Vector3(0, 1, x + 7), Quaternion.identity);
        clone.name = "Plus-" + x;
        objectList.Add(clone.gameObject);
    }
}
