using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Image[] _toolImages = new Image[3];
    [SerializeField] private Transform _glowingBackground;

    Eventcore _eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 0;
        foreach (Tool item in _playerInventory.playerTools)
        {
            if (item == null)
            {
                i++;
                continue;
            }
            _toolImages[i].sprite = item.toolImage;
            i++;
        }    

        // find managers
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_ToolChange.AddListener(updateUIActiveTool);
    }

    private void updateUIActiveTool(Tool swappedTool)
    {
        int toolIndex = Array.IndexOf(_playerInventory.playerTools, swappedTool);
        _glowingBackground.position = _toolImages[toolIndex].transform.position;
    }
}
