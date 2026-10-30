using System;
using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerInventory : MonoBehaviour
{
    public activeInventory inventory = activeInventory.Tool;

    public Tool[] playerTools = new Tool[3];
    public PickableCrop[] playerCrops = new PickableCrop[5];
    private PlayerActions _playerActions;
    private Eventcore _eventCore;
    private bool activeInvetoryState = true;
    private int inventoryIndex;

    [SerializeField] private GameObject _seedPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_PickPlant.AddListener(addCropToInventory);
        _playerActions = GetComponent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        changeInventory();
        if (inventory == activeInventory.Tool)
            changeTool();
        if (inventory == activeInventory.Crop)
            changeCrop();
    }

    void changeInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerCrops[inventoryIndex] != null)
                playerCrops[inventoryIndex].deactivateCrop();
            if (playerTools[Mathf.Clamp(inventoryIndex, 0, playerTools.Length - 1)] != null)
            {
                playerTools[inventoryIndex].deactivateTool();
            }
            inventoryIndex = 0;

            switch (activeInvetoryState)
            {
                case true:
                    inventory = activeInventory.Crop;
                    activeInvetoryState = !activeInvetoryState;
                    if (playerCrops[inventoryIndex] != null)
                        playerCrops[inventoryIndex].activateCrop();
                    _eventCore.UE_CropChange.Invoke(inventoryIndex);
                    break;

                case false:
                    inventory = activeInventory.Tool;
                    activeInvetoryState = !activeInvetoryState;
                    if (playerTools[inventoryIndex] != null)
                        playerTools[inventoryIndex].activateTool();
                    _eventCore.UE_ToolChange.Invoke(inventoryIndex);
                    break;
            }
        }
    }

    void changeTool()
    {
        Tool playerTool = null;

        if (Input.mouseScrollDelta.y > 0)
        {
            playerTool = _playerActions.currentlyEquipTool();

            inventoryIndex++;
            if (inventoryIndex > playerTools.Length - 1)
                inventoryIndex = 0;

            _eventCore.UE_ToolChange.Invoke(inventoryIndex);

            _playerActions.changeTool(playerTools[inventoryIndex]);

            if (playerTool != null)
                playerTool.deactivateTool();
            if (playerTools[inventoryIndex] != null)
                playerTools[inventoryIndex].activateTool();
        }

        // same as above but for the reverse
        if (Input.mouseScrollDelta.y < 0)
        {
            playerTool = _playerActions.currentlyEquipTool();

            inventoryIndex--;
            if (inventoryIndex < 0)
                inventoryIndex = playerTools.Length - 1;

            _eventCore.UE_ToolChange.Invoke(inventoryIndex);

            _playerActions.changeTool(playerTools[inventoryIndex]);

            if (playerTool != null)
                playerTool.deactivateTool();
            if (playerTools[inventoryIndex] != null)
                playerTools[inventoryIndex].activateTool();
        }
    }

    void changeCrop()
    {
        PickableCrop playerCrop = null;

        if (Input.mouseScrollDelta.y > 0)
        {
            playerCrop = _playerActions.currentlyEquipCrop();

            inventoryIndex++;
            if (inventoryIndex > playerCrops.Length - 1)
                inventoryIndex = 0;

            _eventCore.UE_CropChange.Invoke(inventoryIndex);

            _playerActions.changeCrop(playerCrops[inventoryIndex]);

            if (playerCrop != null)
                playerCrop.deactivateCrop();
            if (playerCrops[inventoryIndex] != null)
                playerCrops[inventoryIndex].activateCrop();
        }

        // same as above but for the reverse
        if (Input.mouseScrollDelta.y < 0)
        {
            playerCrop = _playerActions.currentlyEquipCrop();

            inventoryIndex--;
            if (inventoryIndex < 0)
                inventoryIndex = playerCrops.Length - 1;

            _eventCore.UE_CropChange.Invoke(inventoryIndex);

            _playerActions.changeCrop(playerCrops[inventoryIndex]);

            if (playerCrop != null)
                playerCrop.deactivateCrop();
            if (playerCrops[inventoryIndex] != null)
                playerCrops[inventoryIndex].activateCrop();
        }
    }
    public void addSeedBagToInventory(Plants seedBag)
    {
        foreach (Tool currentTool in playerTools)
        {
            if (currentTool == null)
            {
                GameObject createdSeed = Instantiate(_seedPrefab);
                Debug.Log(seedBag.seedTypes);
                createdSeed.GetComponent<Tool>().thisSeedType = seedBag.seedTypes;
                int index = Array.IndexOf(playerTools, currentTool);
                playerTools[index] = createdSeed.GetComponent<Tool>();
                return;
            }
        }
    }
    void addCropToInventory(PickableCrop harvestedPlant)
    {
        GameObject inventoryCrop = Instantiate(harvestedPlant.cropPrefab);
        inventoryCrop.SetActive(false);
        inventoryCrop.GetComponent<CapsuleCollider>().enabled = false;

        foreach (PickableCrop currentCrop in playerCrops)
        {

            if (currentCrop == null)
            {
                int index = Array.IndexOf(playerCrops, currentCrop);
                playerCrops[index] = inventoryCrop.GetComponent<PickableCrop>();
                return;
            }
        }
    }
}
