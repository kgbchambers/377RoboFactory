using UnityEngine;

[CreateAssetMenu(fileName ="NewUpgrade", menuName = "ScriptableObject/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string Name;
    public float modifier;
    public bool Add;
    public bool multiply;
}
