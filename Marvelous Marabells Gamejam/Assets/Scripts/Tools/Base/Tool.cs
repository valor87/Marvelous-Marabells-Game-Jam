using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] ToolsObject thisTool;
    [SerializeField] Mesh toolMesh;


    public virtual void useToolPrimary()
    {
        Debug.Log($"Tools {gameObject.name} primary has been used");
    }

    public virtual void useToolSecondary()
    {
        Debug.Log($"Tools {gameObject.name} secondary has been used");
    }

    public virtual void equipTool()
    {
        Debug.Log($"Tools {gameObject.name} has been equipped");
    }

    public virtual void breakTool()
    {
        Debug.Log($"Tools {gameObject.name} has been broken");
    }

    public void fallowPlayer(Transform playerTransform)
    {
        transform.position = playerTransform.position;
        transform.eulerAngles = playerTransform.eulerAngles;
    }
}
