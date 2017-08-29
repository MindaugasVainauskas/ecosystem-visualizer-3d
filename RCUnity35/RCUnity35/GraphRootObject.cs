using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCUnity35
{
    public sealed class GraphRootObject
    {
        public Graph graph { get; set; }
        public IList<GraphElement> graphElements { get; set; }
        public IList<GraphRelationship> graphRelationships { get; set; }
    }

    public sealed class Size
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public sealed class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }
   
    public sealed class Outer
    {
        public string fill { get; set; }
        public bool magnet { get; set; }
    }

    public sealed class Inner
    {
        public string fill { get; set; }
    }

    public sealed class Text
    {
        public string text { get; set; }
        public string fill { get; set; }
    }

    public sealed class Connection
    {
        public int strokeWidth { get; set; }
        public string stroke { get; set; }
        public string dataCategory { get; set; }
    }

    public sealed class MarkerVertices
    {
        public string display { get; set; }
    }

    public sealed class MarkerTarget
    {
        public string fill { get; set; }
        public string stroke { get; set; }
        public string d { get; set; }
    }

    public sealed class Attrs
    {
        [JsonProperty(".outer")]
        public Outer outer { get; set; }

        [JsonProperty(".inner")]
        public Inner inner { get; set; }
        public Text text { get; set; }

        [JsonProperty(".connection")]
        public Connection connection { get; set; }

        [JsonProperty(".marker-vertices")]
        public MarkerVertices markerVertices { get; set; }

        [JsonProperty(".marker-target")]
        public MarkerTarget markerTarget { get; set; }
    }

    public sealed class Source
    {
        public string id { get; set; }
        public string selector { get; set; }
        public object port { get; set; }
    }

    public sealed class Target
    {
        public string id { get; set; }
    }

    public sealed class Text2
    {
        public string text { get; set; }
    }

    public sealed class Attrs2
    {
        public Text2 text { get; set; }
    }

    public sealed class Label
    {
        public double position { get; set; }
        public Attrs2 attrs { get; set; }
    }

    public sealed class Cell
    {
        public string type { get; set; }
        public Size size { get; set; }
        public Position position { get; set; }
        public int angle { get; set; }
        public string celltype { get; set; }
        public string asset { get; set; }
        public string id { get; set; }
        public string elemID { get; set; }
        public int z { get; set; }
        public Attrs attrs { get; set; }
        public Source source { get; set; }
        public Target target { get; set; }
        public bool? allowExtend { get; set; }
        public bool? smooth { get; set; }
        public IList<Label> labels { get; set; }
        public string embeds { get; set; }
        public string relID { get; set; }
    }

    public sealed class Graph
    {
        public IList<Cell> cells { get; set; }
    }

    public sealed class GraphElement
    {
        public string name { get; set; }
        public bool actorelem { get; set; }
        public string shape { get; set; }
        public string elemID { get; set; }
        public string asset { get; set; }
        public int actorID { get; set; }
    }

    public sealed class GraphRelationship
    {
        public string category { get; set; }
        public string relID { get; set; }
        public string destid { get; set; }
        public string sourceid { get; set; }
        public string description { get; set; }
        public string clientid { get; set; }
        public string subtype { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }


}
