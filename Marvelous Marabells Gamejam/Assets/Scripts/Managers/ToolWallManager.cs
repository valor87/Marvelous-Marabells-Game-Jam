using UnityEngine;

public class ToolWallManager : MonoBehaviour
{
    [SerializeField] private ToolBoardTool[] CurrentToolsOnWall = new ToolBoardTool[4];
    [SerializeField] private ToolsObject _hammerTool;
    [SerializeField] private ToolsObject _axeTool;

    [SerializeField] private playerProgress _playerProgress;
    void Start()
    {
        if (_playerProgress.hasAxe)
        {
            setToolActive(_axeTool);
        }
        if (_playerProgress.hasHammer)
        {
            setToolActive(_hammerTool);
        }
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
