using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class DebrisGenerator : MonoBehaviour
{
    [SerializeField] private List<Transform> _plantableSoil = new List<Transform>();

    [SerializeField] int _amountOfDebris;
    [SerializeField] Vector3 _spawnOffset;
    [SerializeField] private GameObject _rockObject;
    [SerializeField] private GameObject _twigObject;
    void Start()
    {
        generateDebris();
    }
    void generateDebris()
    {
        List<Transform> changingTransform = _plantableSoil;
        for (int i = _amountOfDebris; i > 0; i--)
        {
            int randomTransform = Random.Range(0, changingTransform.Count - 1);
            int randomItem = Random.Range(0, 2);
            GameObject debrisObject = null;
            switch (randomItem)
            {
                case 0:
                    debrisObject = _rockObject;
                    break;
                case 1:
                    debrisObject = _twigObject;
                    break;
            }

            Instantiate(debrisObject, changingTransform[randomTransform].transform.position + _spawnOffset, Quaternion.identity, transform);
            changingTransform.Remove(changingTransform[randomTransform]);
        }
    }

    [ContextMenu("Generate List")]
    List<Transform> generateList()
    {
        List<Transform> list = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i));
            list[i].transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
            list[i].transform.GetChild(0).gameObject.transform.localScale = Vector3.one * 300;

        }
        return _plantableSoil = list;
    }
}
