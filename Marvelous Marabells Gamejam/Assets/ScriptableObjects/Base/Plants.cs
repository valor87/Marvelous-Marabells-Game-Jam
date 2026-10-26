using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Scriptable Objects/Plants")]
public class Plants : ScriptableObject
{
    public string plantName;
    public seedTypes seedTypes = seedTypes.None;
    public float sellAmount;
    public GameObject plantObject;
}
