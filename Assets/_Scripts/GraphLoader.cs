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
        LoadGraph("Simple.json");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform sphere;
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
            if (elShape.Equals("Circle"))
            {
                instantiateSphere(i);
            }
        }
    }

    void instantiateSphere(int x) {
        Instantiate(sphere, new Vector3(x+1, 1, 1), Quaternion.identity);
    }
}
