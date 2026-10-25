using UnityEngine;

public class PlantManager : MonoBehaviour
{
    /*This manager will listen for a planting event
     * the event will tell this script the plot of land the is being
     * planted on and the seed type that being planted
     * The script will have reference to all speed types and setup the plot
     * of land with the right seed
     */

    [SerializeField] private GameObject[] _allPlants = new GameObject[0];

    private Eventcore _eventCore;

    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();       
    }

}
