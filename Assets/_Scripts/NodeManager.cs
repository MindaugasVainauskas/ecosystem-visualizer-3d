using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager{
    
    public GameObject parentObject; //Parent object holding graph objects inside it.
    private List<GameObject> objectList;

    //Constructor
    public NodeManager()
    {
        parentObject = GameObject.Find("ParentObject");
        objectList = new List<GameObject>();
    }

    //Reusable graph node creation method
    public void instantiateObject(Transform shape, float posX, float posZ, string objId)
    {
        float scaleFactor = 0.09f;
        float distanceReducer = 30f;
        Transform clone = Object.Instantiate(shape, new Vector3(posX / distanceReducer, -4.6f, ((posZ / distanceReducer) + 10)), Quaternion.identity);
        clone.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        clone.name = "" + objId;
        clone.parent = parentObject.transform;
        objectList.Add(clone.gameObject);
    }

    public List<GameObject> getObjectList()
    {
        return objectList;
    }

}
