using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Scriptable Objects/Plants")]
public class Plants : ScriptableObject
{
    public string plantName;
    public int sellAmount;
    public float growDuration;
    public float growSpeed;
    public bool reGrow = false;
    public Sprite plantSprite;

    public seedTypes seedTypes;
    public GameObject plantObject;
}
