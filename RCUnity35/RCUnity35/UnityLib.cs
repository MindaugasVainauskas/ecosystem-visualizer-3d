using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCUnity35
{
    public sealed class UnityLib
    {
        /*
         * class file for dll to parse json file for unity
         * need to use the .net framework and newtonsoft
         * so repackaging
         */

        public GraphRootObject readJsonFile(string jsonText)
        {
            GraphRootObject myGraph;

            // parse the text string to an array and work from there.
            // removes the problem of where the file is.
            // might be clunky for now

            myGraph = new GraphRootObject();

            var json = JsonConvert.DeserializeObject<GraphRootObject>(jsonText);
            myGraph = json;
            return myGraph;
        }


    }
}
