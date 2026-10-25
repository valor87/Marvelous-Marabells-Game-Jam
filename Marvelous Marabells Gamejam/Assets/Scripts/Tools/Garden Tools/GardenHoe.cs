using System.Collections;
using UnityEngine;

public class GardenHoe : Tool
{
    public float rotationChangeValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void useToolPrimary()
    {
        StartCoroutine(useHoeAnimation());
    }
    IEnumerator useHoeAnimation()
    {
        Debug.Log("Running");
        float rotationValue = 0;
        Vector3 tempValue = transform.eulerAngles;

        while (rotationValue <= 90)
        {
            Debug.Log("running " + rotationValue);
            rotationValue += rotationChangeValue * Time.deltaTime;
            tempValue.x += rotationChangeValue * Time.deltaTime;
            transform.eulerAngles = tempValue;
            yield return null;
        }
        while (rotationValue >= 0)
        {
            rotationValue -= rotationChangeValue * Time.deltaTime;
            tempValue.x -= rotationChangeValue * Time.deltaTime;
            transform.eulerAngles = tempValue;
            yield return null;
        }
    }
}
