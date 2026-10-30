using UnityEngine;

public class PlantManager : MonoBehaviour
{
    /*This manager will listen for a planting event
     * the event will tell this script the plot of land the is being
     * planted on and the seed type that being planted
     * The script will have reference to all speed types and setup the plot
     * of land with the right seed
     */

    [SerializeField] private Plants[] _allPlants = new Plants[0];
    
    private Eventcore _eventCore;

    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_PlantItem.AddListener(startPlantGrowing);
    }

    void startPlantGrowing(seedTypes desiredPlant, Transform soilTransform)
    {
        foreach (Plants plant in _allPlants)
        {
            if (plant.seedTypes == desiredPlant)
            {
                GameObject _plantObject = Instantiate(plant.plantObject);
                _plantObject.transform.position = soilTransform.position;
                _plantObject.transform.localScale = Vector3.one;
                _plantObject.GetComponent<BasePlant>().soilPlantedOn = soilTransform.GetComponent<PlantableSoil>();
                soilTransform.GetComponent<PlantableSoil>().growingPlant = _plantObject.GetComponent<BasePlant>();
            }
        }
    }
}
