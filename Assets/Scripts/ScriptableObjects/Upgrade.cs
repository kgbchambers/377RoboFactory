using UnityEngine;

[CreateAssetMenu(fileName ="NewUpgrade", menuName = "ScriptableObject/Upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("Title of Upgrade")]

    public string upgradeName;
    [Header("Upgrade amount (additive)")]
    public float tierMod1;
    public float tierMod2;
    public float tierMod3;
    public float tierMod4;
    public float tierMod5;
    public float tierMod6;
    public float tierMod7;
    public float tierMod8;
    public float tierMod9;
    public bool makeModifierMultiplicative;
    public float cost;
    public float modifier;

    [Header("Select a single modifier")]
    public bool isModifyingScrapCapacity;
    public bool isModifyingScrapRecharge;
    public bool isModifyingConveyorSpeed;
    public bool isModifyingTruckCapacity;
    public bool isModifyingTruckSpeed;
    public bool isModifyingFabricatorSpeed;
    public bool isModifyingRobotValue;
}
