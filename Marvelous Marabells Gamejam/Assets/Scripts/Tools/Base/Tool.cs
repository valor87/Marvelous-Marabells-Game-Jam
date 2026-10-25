using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolsObject thisTool;
    public Sprite toolImage;
    [SerializeField] Mesh _toolMesh;


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

    public void fallowPlayer(Transform _playerTransform)
    {
        transform.position = _playerTransform.position;
        transform.eulerAngles = _playerTransform.eulerAngles;
    }
}
