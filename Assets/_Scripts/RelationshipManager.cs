using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager{
    private LineRenderer lineRenderer;
    public Vector3[] linkPoints;
    private GameObject tempObj;
    private GameObject midPointObj;
    private float _graphScale;
    private List<Vector3> linkStartPos;
    private List<Vector3> linkEndPos;

    private int numPoints = 50; //Declare and instantiate how many points every relationship has. 50 seems to be plenty.

    public GameObject parentObject; //Parent object holding graph objects inside it.

    public RelationshipManager(float _graphScale)
    {
        //get the graph scale size used
        this._graphScale = _graphScale;
        //Instantiate relationship start and end position lists
        linkStartPos = new List<Vector3>();
        linkEndPos = new List<Vector3>();
        //Locate Parent object that will be used to store the entire graph
        parentObject = GameObject.Find("ParentObject");
    }

    //drawing the relationship between nodes
    public void drawRelationship(Vector3 p0, Vector3 p1, string relColour, string relText)
    {
        //int numPoints = 50;
        //Following takes a Hex string colour value from JSON file and applies it to the new relation.
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString(relColour, out myColor);
        Vector3 midPoint = new Vector3();
        tempObj = new GameObject();
        midPointObj = new GameObject();
        lineRenderer = tempObj.AddComponent<LineRenderer>();
        lineRenderer.material.color = myColor;
        lineRenderer.widthMultiplier = _graphScale / 15; //relationship width is a margin of the scale of the graph objects.
        linkPoints = new Vector3[numPoints];
        lineRenderer.positionCount = numPoints;
        midPoint = calcMidPoint(p0, p1);
        midPointObj.transform.localPosition = midPoint;
        TextMesh txt = midPointObj.AddComponent<TextMesh>();
        txt.text = relText;
        txt.fontSize = 8;

        //other way of calculating midpoints for recurring relations
        //Don't like nested for loops but I don't seem to have much other choice...
        foreach (Vector3 sPos in linkStartPos)
        {
            foreach (Vector3 ePos in linkEndPos)
            {
                if (sPos == p0 && ePos == p1)
                {
                    midPoint.y += (_graphScale / 4); //adjusted height of midpoint due to smaller dimensions
                    txt.transform.localPosition += new Vector3(0, (_graphScale / 6), 0);
                    break;
                }
            }

        }

        //set positions for text for relationships and names for midpoints and relations themselves.
        txt.transform.localPosition += new Vector3(-1f, 1f, 0);
        midPointObj.name = "Midpoint-" + relText;
        midPointObj.transform.SetParent(parentObject.transform);
        tempObj.name = "Relationship-" + relText;
        tempObj.transform.SetParent(parentObject.transform);

        //add positions to start and end position lists
        linkStartPos.Add(p0);
        linkEndPos.Add(p1);
        for (int i = 1; i <= numPoints; i++)
        {
            float t = i / (float)numPoints;
            linkPoints[i - 1] = drawCurvedRelation(t, p0, p1, midPoint);
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
    private Vector3 drawCurvedRelation(float t, Vector3 p0, Vector3 p1, Vector3 midPoint)
    {
        Vector3 temp = new Vector3();
        float r1 = 1 - t;
        float tt = t * t;
        float r2 = r1 * r1;
        temp = r2 * p0 + 2 * r1 * t * midPoint + tt * p1;
        return temp;
    }

}
