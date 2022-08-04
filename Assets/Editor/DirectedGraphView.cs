using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;
using UnityEditor.UIElements;
//using UnityEditor.UIElements;

public class DirectedGraphView : GraphView
{
    public readonly Vector2 DefaultNodeSize = new Vector2(150, 200);

    public DirectedGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DirectedGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach((port) =>
       {
           if (startPort != port && startPort.node != port.node)
           {
               compatiblePorts.Add(port);
           }

       });
        return compatiblePorts;
    }

    private Port GeneratePort(DGNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float)); //arbitrairy type
    }
    private DGNode GenerateEntryPointNode()  //Generates the first Node of any new Directed Graph
    {
        var node = new DGNode
        {
            title = "START",
            GUID = "START", 
            Entry = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable; //Removes ability to Move and remove Start Node
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState(); //Refreshes node
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDirectedGraphNode(nodeName));
    }

    internal DGNode CreateDirectedGraphNode(string nodeName)
    {
        var node = new DGNode
        {
            title = nodeName,
            GUID = Guid.NewGuid().ToString(), //Generates an ID using Unity
            NodeDialog = new List<DGDialog>(), //NodeData
            Entry = false
        };

        node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        //Input Ports
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        node.inputContainer.Add(inputPort);

        //Add Output Port
        var buttonOutPort = new Button(() => { AddOutputPort(node); });
        buttonOutPort.text = "New Link";
        node.titleContainer.Add(buttonOutPort);

        //Add Dialog
        var buttonDialog = new Button(() => {
            node.NodeDialog.Add(new DGDialog(""));
            AddDialog(node); });
        buttonDialog.text = "New Dialog";
        node.titleContainer.Add(buttonDialog);

        //dialog text
        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt =>
        {
            node.NodeDialog[0].dialog = evt.newValue;
        });
        textField.SetValueWithoutNotify(node.title);

        //Func<VisualElement> makeItem = () => new Label();

        //Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = node.NodeDialog[i].dialog;

        var dialogContainer = new Box();

        //var listField = new ListView(node.NodeDialog, node.NodeDialog.Count(), makeItem, bindItem);
        //listField.Add(textField);
        //node.mainContainer.Add(listField); 

        node.RefreshPorts();
        node.RefreshExpandedState();
        node.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));

        return node;
    }

    private void AddDialog(DGNode node, Character overridenCharacter = null, string overriddenDialog = "")
    {
        var dialogContainer = new Box();
        node.mainContainer.Add(dialogContainer);

        var characterField = new ObjectField("Character") { 
            objectType = typeof(Character),
            value = overridenCharacter
        }; //Adds a Character object field


        var textField = new TextField
        {
            value = overriddenDialog
        };
        textField.RegisterValueChangedCallback(evt => node.NodeDialog[node.mainContainer.IndexOf(dialogContainer) - 2].dialog = evt.newValue);


        var deleteButton = new Button(() => RemoveDialog(node, dialogContainer))
        {
            text = "Delete"
        };

        dialogContainer.contentContainer.Add(new Label((node.mainContainer.IndexOf(dialogContainer) - 2).ToString()));
        dialogContainer.contentContainer.Add(characterField);
        dialogContainer.contentContainer.Add(textField);
        dialogContainer.Add(deleteButton);

        node.RefreshExpandedState(); //Refreshes node
    }

    internal void AddDialog(DGNode node, DGDialog nodeDialog)
    {
        AddDialog(node, nodeDialog.character, nodeDialog.dialog);
    }

    private void RemoveDialog(DGNode node, Box dialogContainer)
    {
        node.NodeDialog.RemoveAt(node.mainContainer.IndexOf(dialogContainer) - 2);
        node.mainContainer.Remove(dialogContainer);
        node.RefreshExpandedState(); //Refreshes node
    }

    private void UpdateDialog()
    {

    }

    #region OutputPorts
    public void AddOutputPort(DGNode node, string overiddenPortName = "")
    {

        var generatedPort = GeneratePort(node, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
        var outputPortName = $"Option {outputPortCount + 1}";

        var portName = string.IsNullOrEmpty(overiddenPortName)
            ? $"Option {outputPortCount + 1}"
            : overiddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = outputPortName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);

        generatedPort.contentContainer.Add(new Label("   "));

        generatedPort.contentContainer.Add(textField);

        var deleteButton = new Button(() => RemovePort(node, generatedPort))
        {
            text = "X"
        };
        generatedPort.Add(deleteButton);

        generatedPort.portName = portName;

        node.outputContainer.Add(generatedPort);
        node.RefreshExpandedState(); //Refreshes node
        node.RefreshPorts();
    }

    private void RemovePort(Node node, Port socket)
    {
        var targetEdge = edges.ToList()
            .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        node.outputContainer.Remove(socket);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }
} 
#endregion
