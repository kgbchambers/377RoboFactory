using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObject/Robot")]
public class Robot : ScriptableObject
{
    public int tier;
    private int productionLevel = 0;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
}
