using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLoader : MonoBehaviour
{
    //Declaration for Relationship manager class, responsible for drawing the relationships
    private RelationshipManager relManager;
    private NodeManager nodeManager;
    private List<GameObject> nodeList;
    public List<Cell_Link> relations;
    private float _graphScale = 5;
    public GameObject ParentObject; //Parent object holding graph objects inside it.
    private Vector3 startPos;
    private Vector3 endPos;
    
    //Declarations of transforms for gameobjects that are mapped to Unity3d prefabs.
    public Transform cloudService;
    public Transform cube;
    public Transform diamond;
    public Transform hex3d;
    public Transform pyramid;
    public Transform arrowUp;
    public Transform arrowDown;
    public Transform plus3d;
    public Transform sphere;


    //gets called before start. Called once per object lifetime
    void Awake()
    {
        //Relationship manager class gets instantiated with the current graph scale value.
        relManager = new RelationshipManager(_graphScale);
        //Instantiate Node drawing class
        nodeManager = new NodeManager();
        //Instantiate local node list, which will pull its value from nodemanager getObjectlist() method when needed
        nodeList = new List<GameObject>();
        //Load the JSON received through GET request in QR code reader class.
        string data = PlayerPrefs.GetString("JSON_graph_data");
        //Load the graph from given data.
        LoadGraph(data);
    }
    
    //method to load the graph from json file
    public void LoadGraph(string path)
    {
        ParentObject = GameObject.Find("ParentObject");
        string loadGraph = path;

        relations = new List<Cell_Link>();

        JSONNode jNode = JSON.Parse(loadGraph);

        //split JSON node into JSON array. Had to add ["graph"] at start to handle QR code input
        JSONArray cellArray = (JSONArray)jNode["graph"]["cells"];        

        foreach (JSONNode cell in cellArray)
        {
            string cellType = cell["celltype"];

            switch (cellType)
            {
                case "actor":
                    float posX = cell["position"]["x"]-800;
                    float posZ = ((-1)*cell["position"]["y"])+1400;
                    string objId = cell["id"];

                    string objectShape = cell["type"];
                    
                    //Instantiate graph nodes depending on actor types present in JSON array.
                    switch (objectShape)
                    {
                        case "basic.InteractiveCircle":
                            nodeManager.instantiateObject(cloudService, posX, posZ, objId);
                            break;
                        case "basic.InteractiveRect":
                            nodeManager.instantiateObject(cube, posX, posZ, objId);
                            break;
                        case "basic.InteractiveDiamond":
                            nodeManager.instantiateObject(diamond, posX, posZ, objId);
                            break;
                        case "basic.InteractiveHex":
                            nodeManager.instantiateObject(hex3d, posX, posZ, objId);
                            break;
                        default:
                            //If the prefab is not in the list, Just instantiate it as a sphere.
                            nodeManager.instantiateObject(sphere, posX, posZ, objId);
                            break;
                    }                    
                    break;

                //If celltype is relationship then add its fill colour to the list of colours.
                case "relationship":
                    string id = cell["id"];
                    string sourceId = cell["source"]["id"];
                    string targetId = cell["target"]["id"];
                    string relCol = cell["attrs"][".connection"]["stroke"];
                    JSONArray relArray = (JSONArray)cell["labels"];
                    string relText;
                    Cell_Link tempLink;
                    foreach (JSONNode relAttr in relArray)
                    {
                        relText = relAttr["attrs"]["text"]["text"];
                        //Add the variables of a relationship into a Cell_Link object.
                        tempLink = new Cell_Link(id, sourceId, targetId, relCol, relText);
                        //Add the relationship to relations list.
                        relations.Add(tempLink);
                    }
                    break;

                default:
                    break;
            }//end of switch(cellType)
        }//end of forEach(JSONNode cell in cellArray)

        //Assign nodeManager object list to the local node list collection.
        nodeList = nodeManager.getObjectList();

        //Each link will need to be drawn with data from relations list and start/end positions from object list in nodeList list
        foreach (var link in relations)
        {
            //create 2 vector3 objects to hold start and end positions for every relationship
            startPos = new Vector3();
            endPos = new Vector3();           

            //Check for start and end positions
            for (int i = 0; i < nodeList.Count; i++)
            {
                if(link.sourceId.Equals(nodeList[i].name))
                {
                    startPos = nodeList[i].transform.localPosition;
                }
                else if (link.targetId.Equals(nodeList[i].name))
                {
                    endPos = nodeList[i].transform.localPosition;
                }
                else
                {
                    continue;
                }                
            }
            //render relationship with colour pulled from relationship list.
            relManager.drawRelationship(startPos, endPos, link.relCol, link.relText);
        }//end of foreach method
    }//end of LoadGraph method
}
