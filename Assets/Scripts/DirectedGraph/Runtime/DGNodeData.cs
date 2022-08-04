using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DGNodeData
{
    public string NodeGUID;
    public string NodeLabel;
    public Vector2 Position;
    public List<DGDialog> NodeDialog;
}
[System.Serializable]
public class DGDialog
{
    public string dialog;
    public Character character;
    public DGDialog(string dialog)
    {
        this.dialog = dialog;
    }
}

[System.Serializable]
[CreateAssetMenu(menuName = "Directed Graph/Character")]
public class Character : ScriptableObject
{
    public Sprite spriteAvatar;
    public string Name;
}