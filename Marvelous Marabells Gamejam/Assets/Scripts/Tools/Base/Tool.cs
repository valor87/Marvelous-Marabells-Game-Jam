using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] ToolsObject thisTool;
    [SerializeField] Mesh toolMesh;


    public virtual void useTool()
    {
        Debug.Log($"Tools {gameObject.name} has been used");
    }

    public virtual void equipTool()
    {
        Debug.Log($"Tools {gameObject.name} has been equipped");
    }

    public virtual void breakTool()
    {
        Debug.Log($"Tools {gameObject.name} has been broken");
    }

}
