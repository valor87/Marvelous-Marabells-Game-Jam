using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public playerProgress varPlayerProgress;
    public Eventcore Eventcore;
    public TimeManager TimeManager;
    FadeScript _fadeScript = new FadeScript();
    private void Start()
    {
        if (GameObject.FindWithTag("GameController") != this.gameObject)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if (varPlayerProgress.startDayLate == true)
        {
            TimeManager.currentHour = 10;
            varPlayerProgress.startDayLate = false;
        }
    }

    private void Update()
    {
        

        if (Eventcore == null)
        {
            Eventcore = GameObject.Find("EventCore").GetComponent<Eventcore>();
            TimeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
            Eventcore.UE_BreakableObjectTriggered.AddListener(increaseMaterialsCount);
        }
    }
    void saveData(int playerMoney = 0, int playerWoodMaterial = 0, int playerStoneMaterial = 0)
    {

    }

    void increaseMaterialsCount(toolTypes toolUsed, int amountOfMaterial)
    {
        if(toolUsed == toolTypes.Axe)
            varPlayerProgress.playerWoodMaterials += amountOfMaterial;
        if(toolUsed == toolTypes.Hammer)
            varPlayerProgress.playerStoneMaterials += amountOfMaterial;
    }
    public void gainedAxe()
    {
        varPlayerProgress.hasAxe = true;
    }

    public void gainedHammer()
    {
        varPlayerProgress.hasHammer = true;
    }
}
