using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObject/Robot")]
public class Robot : ScriptableObject
{
    public int tier;

    public GameObject small1;
    public GameObject small2;
    public GameObject small3;
    public GameObject med1;
    public GameObject med2;
    public GameObject med3;
    public GameObject full;
    public GameObject box;
}
