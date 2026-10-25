using UnityEngine;

public class PlantableSoil : MonoBehaviour, IInteractable
{
    [SerializeField] Material grassMaterial;
    [SerializeField] Material tilledMaterial;
    public toolTypes toolRequired { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().material = grassMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(toolTypes usedTool)
    {
        GetComponent<MeshRenderer>().material = tilledMaterial;
        Debug.Log("The correct Item has been used");
    }
}
