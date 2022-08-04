using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class DirectedGraph : EditorWindow
{
    private DirectedGraphView _graphView;
    private string _fileName = "New Graph";

    [MenuItem("Graph/DirectedGraph")]
    public static void OpenDirectedGraphWindow()
    {
        var window = GetWindow<DirectedGraph>();

        window.titleContent = new GUIContent("Directed Graph");
    }
    private void OnEnable()
    {
        ConstructGraph();
        GenerateToolbar();
        GenerateMinimap();
    }

    private void GenerateMinimap() //Adds a minimap to the window
    {
        var miniMap = new MiniMap { anchored = true};
        miniMap.SetPosition(new Rect(10, 30, 200, 100));
        _graphView.Add(miniMap);

    }

    private void ConstructGraph()
    {
        _graphView = new DirectedGraphView
        {
            name = "Directed Graph"
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        //Change Graph File Name
        var fileNameTextField = new TextField( "File Name:" );
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        //Saving and Loading
        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });

        //Create Node Button
        var nodeCreateButton = new Button(() => { _graphView.CreateNode("New Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);


        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid File Name", "Please enter a valid file name", "OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);

        if (save) //Save Function
        {
            saveUtility.SaveGraph(_fileName);
        }
        else //Load Function
        {
            saveUtility.LoadGraph(_fileName);
        }
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
