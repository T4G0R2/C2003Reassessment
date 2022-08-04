using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DirectedGraphView _graphView;
    private DGContainer _containerCache;

    private List<Edge> Edges => _graphView.edges.ToList();
    private List<DGNode> Nodes => _graphView.nodes.ToList().Cast<DGNode>().ToList();


    public static GraphSaveUtility GetInstance(DirectedGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _graphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        var dgContainer = ScriptableObject.CreateInstance<DGContainer>();
        //Save Nodes
        if (!Edges.Any()) return; //if there aren't any connections then return


        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

        for (int i = 0; i < connectedPorts.Count(); i++)
        {
            var outputNode = connectedPorts[i].output.node as DGNode;
            var inputNode = connectedPorts[i].input.node as DGNode;



            dgContainer.DGLinkData.Add(new DGLinkData
            {
                ParentGUID = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                PortID = connectedPorts[i].output.node.outputContainer.Children().Select(x => x.Q<Port>()).ToList().IndexOf(connectedPorts[i].output),
                TargetGUID = inputNode.GUID
            });
        }


        foreach (var node in Nodes.Where(node => node.GUID != "START"))
        {
            dgContainer.DGNodeData.Add(new DGNodeData
            {
                NodeGUID = node.GUID,
                NodeLabel = node.NodeLabel,
                Position = node.GetPosition().position,
                NodeDialog = node.NodeDialog,
            });
        }

        //endSave Nodes
        //Auto creates DGSaves Folder in Resources if it does not exist already
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets","Resources");
        
        if (!AssetDatabase.IsValidFolder("Assets/Resources/DGSaves"))
            AssetDatabase.CreateFolder("Assets/Resources", "DGSaves");
        
        AssetDatabase.CreateAsset(dgContainer, $"Assets/Resources/DGSaves/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DGContainer>("DGSaves/"+fileName);
        if(_containerCache==null)
        {
            EditorUtility.DisplayDialog("File Does Not Exist", "Target Directed Graph file not found", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes()
    {
        //Iterate through all nodes
        //Get GUID of node, check list of Links for any 

        for (int i = 0; i < Nodes.Count; i++)
        {
            var connections = _containerCache.DGLinkData.Where(x => x.ParentGUID == Nodes[i].GUID).ToList();
            for (var j = 0; j < connections.Count; j++)
            {
                var targetNodeGUID = connections[j].TargetGUID;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(_containerCache.DGNodeData.First(x => x.NodeGUID == targetNodeGUID).Position,
                                                _graphView.DefaultNodeSize));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge()
        {
            output = output,
            input = input
        };
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);

        _graphView.Add(tempEdge);
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DGNodeData.Where(node => node.NodeGUID != "START")) //START is a unique GUID thanks to Unity's GUID generation
        {
            var tempNode = _graphView.CreateDirectedGraphNode(nodeData.NodeLabel);
            tempNode.GUID = nodeData.NodeGUID;
            tempNode.NodeDialog = nodeData.NodeDialog;
            _graphView.AddElement(tempNode);

            var nodePort = _containerCache.DGLinkData.Where(x => x.ParentGUID == nodeData.NodeGUID).ToList();
            nodePort.ForEach(x => _graphView.AddOutputPort(tempNode, x.PortName));

            nodeData.NodeDialog.ForEach(x => _graphView.AddDialog(tempNode,x));
        }
    }

    private void ClearGraph()
    {
        foreach (var node in Nodes)
        {
            //remove Edges connected to Node
            if (node.Entry) continue;
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _graphView.RemoveElement(edge));

            //Then Remove Node
            _graphView.RemoveElement(node);
        }
    }
}
