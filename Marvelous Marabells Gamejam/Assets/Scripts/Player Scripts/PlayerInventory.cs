using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Tool[] playerTools = new Tool[3];
    private PlayerActions _playerActions;
    Eventcore _eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _playerActions = GetComponent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        changeTool();
    }

    void changeTool()
    {
        Tool playerTool = null;

        if (Input.mouseScrollDelta.y > 0)
        {
            Debug.Log("Scroll up");
            // get the tool that the player has
            playerTool = _playerActions.currentlyEquipTool();
            // get the index in the array
            int item = Array.IndexOf(playerTools, playerTool);
            // find the next element
            item++;
            // if its outside of the arry
            if (item > playerTools.Length - 1)
                item = 0;
            // set the tool to the active tool
            _playerActions.changeTool(playerTools[item]);
            
            // call the event for changing tools
            _eventCore.UE_ToolChange.Invoke(playerTools[item]);
            if(playerTool != null)
                playerTool.deactivateTool();
            if (playerTools[item] != null)
                playerTools[item].activateTool();
        }

        // same as above but for the reverse
        if (Input.mouseScrollDelta.y < 0)
        {
            Debug.Log("Scroll down");
            playerTool = _playerActions.currentlyEquipTool();

            int item = Array.IndexOf(playerTools, playerTool);
            item--;
            if (item < 0)
                item = playerTools.Length - 1;
            _playerActions.changeTool(playerTools[item]);
            _eventCore.UE_ToolChange.Invoke(playerTools[item]);
            if(playerTool != null)
                playerTool.deactivateTool();
            if (playerTools[item] != null)
                playerTools[item].activateTool();
        }
    }
}
