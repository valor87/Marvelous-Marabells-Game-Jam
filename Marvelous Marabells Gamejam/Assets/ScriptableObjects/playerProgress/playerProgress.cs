using UnityEngine;

[CreateAssetMenu(fileName = "playerProgress", menuName = "Scriptable Objects/playerProgress")]
public class playerProgress : ScriptableObject
{
    public int day = 1;
    public int playerMoney = 100;
    public int playerWoodMaterials = 0;
    public int playerStoneMaterials = 0;

    public bool hasAxe = false;
    public bool hasHammer = false;
    public bool startDayLate = false;

    public Tool[] palyerTools = new Tool[3];
}
