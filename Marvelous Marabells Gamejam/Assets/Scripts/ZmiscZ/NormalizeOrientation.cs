using UnityEngine;

public class NormalizeOrientation : MonoBehaviour
{
    void Update()
    {
        Vector3 roation = transform.eulerAngles;
        roation.x = 0;
        roation.z = 0;
        transform.eulerAngles = roation;
    }
}
