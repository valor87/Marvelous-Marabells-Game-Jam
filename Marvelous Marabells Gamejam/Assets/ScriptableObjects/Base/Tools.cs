using UnityEngine;

[CreateAssetMenu(fileName = "Tools", menuName = "Scriptable Objects/Tools")]
public class ToolsObject : ScriptableObject
{
    public string toolName = "Hoe";
    public toolTypes toolType;
    public bool twoHanded = false;
    public bool consumable = false;

    public int durability = 10;
}
