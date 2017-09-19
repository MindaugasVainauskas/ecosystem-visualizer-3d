//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Newtonsoft.Json;
//using System;
//using System.Linq;
//using System.Text;

//public class GraphRootObject {
//    public Graph graph { get; set; }
//    public List<GraphElement> graphElements { get; set; }
//    public List<GraphRelationship> graphRelationships { get; set; }
//}

//    public class Size
//    {
//        public int width { get; set; }
//        public int height { get; set; }
//    }

//    public class Position
//    {
//        public int x { get; set; }
//        public int y { get; set; }
//    }

//    public class Outer
//    {
//        public string fill { get; set; }
//        public bool magnet { get; set; }
//    }

//    public class Inner
//    {
//        public string fill { get; set; }
//    }

//    public class Text
//    {
//        public string text { get; set; }
//        public string fill { get; set; }
//    }

//    public class Connection
//    {
//        public int strokeWidth { get; set; }
//        public string stroke { get; set; }
//        public string dataCategory { get; set; }
//    }

//    public class MarkerVertices
//    {
//        public string display { get; set; }
//    }

//    public class MarkerTarget
//    {
//        public string fill { get; set; }
//        public string stroke { get; set; }
//        public string d { get; set; }
//    }

//    public class Attrs
//    {
//        [JsonProperty(".outer")]
//        public Outer outer { get; set; }

//        [JsonProperty(".inner")]
//        public Inner inner { get; set; }
//        public Text text { get; set; }

//        [JsonProperty(".connection")]
//        public Connection connection { get; set; }

//        [JsonProperty(".marker-vertices")]
//        public MarkerVertices markerVertices { get; set; }

//        [JsonProperty(".marker-target")]
//        public MarkerTarget markerTarget { get; set; }
//    }

//    public class Source
//    {
//        public string id { get; set; }
//        public string selector { get; set; }
//        public object port { get; set; }
//    }

//    public class Target
//    {
//        public string id { get; set; }
//    }

//    public class Text2
//    {
//        public string text { get; set; }
//    }

//    public class Attrs2
//    {
//        public Text2 text { get; set; }
//    }

//    public class Label
//    {
//        public double position { get; set; }
//        public Attrs2 attrs { get; set; }
//    }

//    public class Cell
//    {
//        public string type { get; set; }
//        public Size size { get; set; }
//        public Position position { get; set; }
//        public int angle { get; set; }
//        public string celltype { get; set; }
//        public string asset { get; set; }
//        public string id { get; set; }
//        public string elemID { get; set; }
//        public int z { get; set; }
//        public Attrs attrs { get; set; }
//        public Source source { get; set; }
//        public Target target { get; set; }
//        public bool? allowExtend { get; set; }
//        public bool? smooth { get; set; }
//        public List<Label> labels { get; set; }
//        public string embeds { get; set; }
//        public string relID { get; set; }
//    }

//    public class Graph
//    {
//        public List<Cell> cells { get; set; }
//    }

//    public class GraphElement
//    {
//        public string name { get; set; }
//        public bool actorelem { get; set; }
//        public string shape { get; set; }
//        public string elemID { get; set; }
//        public string asset { get; set; }
//        public int actorID { get; set; }
//    }

//    public class GraphRelationship
//    {
//        public string category { get; set; }
//        public string relID { get; set; }
//        public string destid { get; set; }
//        public string sourceid { get; set; }
//        public string description { get; set; }
//        public string clientid { get; set; }
//        public string subtype { get; set; }
//        public string type { get; set; }
//        public string name { get; set; }
//    }





