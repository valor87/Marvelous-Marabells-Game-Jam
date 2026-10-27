using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Image[] _toolImages = new Image[3];
    [SerializeField] private Image[] _cropImages = new Image[5];
    [SerializeField] private Transform _glowingBackground;

    Eventcore _eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setToolInventoryImages();   
        // find managers
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_ToolChange.AddListener(updateUIActiveTool);
        _eventCore.UE_CropChange.AddListener(updateUIActiveCrop);
        _eventCore.UE_ToolInventoryChange.AddListener(setToolInventoryImages);
    }

    private void updateUIActiveTool(int swappedTool)
    {
        _glowingBackground.position = _toolImages[swappedTool].transform.position;
    }

    private void updateUIActiveCrop(int swappedPlant)
    {
        _glowingBackground.position = _cropImages[swappedPlant].transform.position;
    }
    void setToolInventoryImages()
    {
        int i = 0;
        foreach (Tool item in _playerInventory.playerTools)
        {
            if (item == null)
            {
                _toolImages[i].sprite = null;
                i++;
                continue;
            }
            _toolImages[i].sprite = item.toolImage;
            i++;
        }

    }
}
