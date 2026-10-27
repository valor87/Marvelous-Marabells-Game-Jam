using UnityEngine;

public class ToolWallManager : MonoBehaviour
{
    [SerializeField] private ToolBoardTool[] CurrentToolsOnWall = new ToolBoardTool[4];
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool checkIfToolIsActive(ToolBoardTool checkingTool)
    {
        if (checkingTool.gameObject.activeInHierarchy)
        {
            return true;
        }
        return false;
    }

    public void setToolActive(ToolsObject settingActiveTool)
    {
        foreach (ToolBoardTool var in CurrentToolsOnWall)
        {
            if (settingActiveTool == var.thisToolObject)
            {
                var.gameObject.SetActive(true);
            }
        }
    }
}
