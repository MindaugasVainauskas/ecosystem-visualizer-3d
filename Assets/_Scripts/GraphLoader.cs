using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLoader : MonoBehaviour {
    public Graph ecosystem;    
    //gets called before start. Called once per object lifetime
    void Awake() {
        //LoadGraph("Simple.json");
        LoadGraph("MoreComplex.json");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform sphere;
    public Transform cube;
    public Transform diamond;
    public Transform hex3d;
    public Transform pyramid;
    public Transform arrowUp;
    public Transform arrowDown;
    public Transform plus3d;
    //method to load the graph from json file
    public void LoadGraph(string path) {
        string loadGraph = JSONReader.LoadJSon(path);
        JSONNode node = JSON.Parse(loadGraph);

        //JSONNode cell = node["graph"];
        JToken array = JObject.Parse(loadGraph);
        //dynamic jsonarray = JsonConvert.DeserializeObject<dynamic>(loadGraph);
        //var cells = jsonarray.cells;
        //foreach (var cell in cells)
        //{
        //    var celltype = cell.celltype;
        //    Debug.Log(celltype);
        //}
        ecosystem = JsonUtility.FromJson<Graph>(loadGraph);
        Debug.Log(array);
       
        var elements = ecosystem.graphElements;

        for (int i = 0; i < elements.Length; i++)
        {
            string elShape = elements[i].shape;
            switch (elShape)
            {
                case "Circle":
                    instantiateSphere(i);
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
            
        }
    }

    void instantiateSphere(int x) {
        Instantiate(sphere, new Vector3(0, 1, x), Quaternion.identity);
    }
    void instantiateCube(int x) {
        Instantiate(cube, new Vector3(0, 1, x+1), Quaternion.identity);
    }
    void instantiateDiamond(int x)
    {
        Instantiate(diamond, new Vector3(0, 1, x+2), Quaternion.identity);
    }
    void instantiateHex(int x)
    {
        Instantiate(hex3d, new Vector3(0, 1, x+3), Quaternion.identity);
    }
    void instantiatePyramid(int x)
    {
        Instantiate(pyramid, new Vector3(0, 1, x + 4), Quaternion.identity);
    }
    void instantiateArrowUp(int x)
    {
        Instantiate(arrowUp, new Vector3(0, 1, x + 5), Quaternion.identity);
    }
    void instantiateArrowDown(int x)
    {
        Instantiate(arrowDown, new Vector3(0, 1, x + 6), Quaternion.identity);
    }
    void instantiatePlus(int x)
    {
        Instantiate(plus3d, new Vector3(0, 1, x + 7), Quaternion.identity);
    }
}
