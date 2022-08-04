using System;
using System.Collections.Generic;
using System.Linq;

//Functions for use in runtime are at the bottom

namespace Director
{
    #region Initialisation
    public class Option
    {
        internal int targetIndex;
        public DGButton button;

        internal Option(Node parent, Node child, int optionID, DGContainer sourceMap)
        {
            targetIndex = child.index;
            button = GetSourceLinks(sourceMap, parent, child).Find(x => x.PortID == optionID).Button;
        }
        List<DGLinkData> GetSourceLinks(DGContainer sourceMap, Node parent, Node child) //Returns all nodes from DGLinkData that correlates to the node running this function
        {
            return sourceMap.DGLinkData.FindAll(x => x.ParentGUID == parent.guid && x.TargetGUID == child.guid);
        }
    }
    public class Node
    {
        internal List<DGDialog> dialogList;  //Stores all dialogs for node
        internal List<Option> optionList;    //Stores all available options for node
        internal int index;           //Stores index
        internal string guid;

        public Node(string guid, int index, DGContainer sourceMap)
        {
            this.index = index;
            this.guid = guid;
            this.dialogList = GetSourceNode(sourceMap).NodeDialog;
        }

        public Node(string guid, List<Node> nodes, List<string> children, DGContainer sourceMap)
        {
            this.index = nodes.Count();
            this.guid = guid;
            this.dialogList = GetSourceNode(sourceMap).NodeDialog;

            foreach (var (child, i) in GetChildren(nodes, children).Select((child, i) => (child, i))) //Foreach loop with an iteration count i
            {
                optionList.Add(new Option(this, child, i, sourceMap));
            }
        }
        List<Node> GetChildren(List<Node> nodes, List<string> childGUIDs)
        {
            return nodes.Where(x => childGUIDs.Any(y => y == x.guid)).ToList(); //returns a list of Nodes containing all the children as specified by the list of guids
        }
        DGNodeData GetSourceNode(DGContainer sourceMap) //Returns the original node from DGNodeData that correlates to the node running this function
        {
            return sourceMap.DGNodeData.Find(x => x.NodeGUID == this.guid);
        }

    }

    internal class BurntGraph
    {
        internal readonly List<Node> nodes;
        static List<string> ignore; //List of GUIDs to ignore during initialisation, to avoid duplication. Is used only during initialisation of a graph and is emtpy otherwise
        private static DGContainer sourceMap;

        public BurntGraph(DGContainer source)
        {

            sourceMap = source;
            ignore = new List<string>();
            nodes = new List<Node>();
            nodes = LoadNodes("START"); //Loads DGContainer as a more processing effecient object
            sourceMap = null; //unloads source map to save memory

        }

        private List<Node> LoadNodes(string guid)
        {
            List<Node> output = new List<Node>();
            List<string> avenues = sourceMap.DGLinkData.Where(x => x.ParentGUID == guid).Select(x => x.TargetGUID).ToList(); //Creates a list of all node GUIDs that originate from the node of input GUID
            if (avenues.Count == 0) //Recursion is at the end of a tree
            {
                output.Add(new Node(guid,output.Count(),sourceMap)); // Create a node with no Children
                ignore.Add(guid); //Mark avenue as observed
                return output;
            }
            
            foreach(string check in avenues)
            {
                if (!ignore.Contains(check)) //if the observered avenue hasnt already been checked
                {
                    output.AddRange(LoadNodes(check));
                }
            }
            output.Add(new Node(guid,output,avenues.Distinct().ToList(), sourceMap)); // Create Node with Children. It can be assumed that any children of node have been added to the list of nodes
            ignore.Add(guid); //Mark avenue as observed
            return output;
        }
    }
    #endregion

    #region Runtime
    public class Director //References the entire graph
    {
        readonly BurntGraph loaded;
        internal int pointer;

        public Director(DGContainer source)
        {
            this.loaded = new BurntGraph(source);
            this.pointer = loaded.nodes.Count(); //It can be assumed that the last node checked is the origin of the tree as all connections are one to many
        }
        public List<Option> GetOptions()
        {
            return loaded.nodes[pointer].optionList; //returns list of all options with button information
        }
        public List<DGDialog> GetDialog()
        {
            return loaded.nodes[pointer].dialogList; //returns list of all dialog for that node
        }
        public void Select(Option selection) //Feed in the Option class to navigate to the correct node
        {
            pointer = selection.targetIndex;
        }
        
    }
    #endregion
}