using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObject/Robot")]
public class Robot : ScriptableObject
{
    public int tier;
    private int productionLevel = 0;

    public GameObject fullRobot;
    public GameObject chassisLegs;
    public GameObject legs;
}
