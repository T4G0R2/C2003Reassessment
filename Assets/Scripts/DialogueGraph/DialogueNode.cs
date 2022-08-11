using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueNode : Node
{
    public string GUID;
    public string DialogueText;
    public bool EntryPoint = false;
}
