using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalParameters", menuName = "Scriptable Objects/OrbitalParameters")]
public class OrbitalParameters : ScriptableObject
{
    public Vector2 distanceRange;
    public bool geoLocked;
}
