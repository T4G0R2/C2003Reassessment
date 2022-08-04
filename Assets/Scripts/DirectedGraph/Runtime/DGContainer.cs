using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DGContainer : ScriptableObject
{
    public List<DGLinkData> DGLinkData = new List<DGLinkData>();
    public List<DGNodeData> DGNodeData = new List<DGNodeData>();
}
