using UnityEngine;

[CreateAssetMenu(fileName ="NewUpgrade", menuName = "ScriptableObject/Upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("Title of Upgrade")]

    public string upgradeName;
    [Header("Upgrade tier level")]
    public int tier;
    [Header("Upgrade amount (additive)")]
    public float modifier;
    public bool makeModifierMultiplicative;
    public float cost;
    [Header("Select a single modifier")]
    public bool isModifyingScrapCapacity;
    public bool isModifyingScrapRecharge;
    public bool isModifyingConveyorSpeed;
    public bool isModifyingTruckCapacity;
    public bool isModifyingTruckSpeed;
    public bool isModifyingFabricatorSpeed;
    public bool isModifyingRobotValue;
}
