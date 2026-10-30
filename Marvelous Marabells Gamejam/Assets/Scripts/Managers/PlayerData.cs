using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public playerProgress varPlayerProgress;
    public Eventcore Eventcore;
    FadeScript _fadeScript = new FadeScript();
    private void Start()
    {
        if (GameObject.FindWithTag("GameController") != this.gameObject)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Eventcore == null)
        {
            Eventcore = GameObject.Find("EventCore").GetComponent<Eventcore>();
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
