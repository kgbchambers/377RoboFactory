using UnityEngine;

[CreateAssetMenu(fileName ="NewUpgrade", menuName = "ScriptableObject/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string name;
    public float modifier;
    public float cost;
    [Header("Select a single modifier")]
    public bool isModifyingConveyorSpeed;
    public bool isModifyingFabricatorPower;
    public bool isModifyingCash;
    public bool isModifyingScrap;
    [Header("Single additional modifier")]
    public bool isSECOND_MODIFIER;
    public bool alsoConveyorSpeed;
    public bool alsoFabricatorPower;
    public bool alsoCash;
    public bool alsoScrap;
}
