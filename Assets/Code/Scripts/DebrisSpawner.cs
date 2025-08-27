using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [SerializeField] public OrbitalParameters LEOParams;
    [SerializeField] public OrbitalParameters MEOParams;
    [SerializeField] public OrbitalParameters HEOParams;
    [SerializeField] public OrbitalParameters GEOParams;

    public int numLeoDebris = 0;
    public int numMeoDebris = 0;
    public int numHeoDebris = 0;
    public int numGeoDebris = 0;

    static float earthDiameter = 12756f;

    [SerializeField]
    public int numDebris = 10;

    public GameObject debrisPrefab;

    [SerializeField] RuntimeSet<GameObject> debrisSet;

    public void clearDebris()
    {
        numLeoDebris = 0;
        numMeoDebris = 0;
        numHeoDebris = 0;
        numGeoDebris = 0;
    }

    public void SpawnLEODebris()
    {
        Vector2 distanceRange = LEOParams.distanceRange;
        float rotationRange = 360f;
        if (LEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            numLeoDebris++;
            debrisSet.Add(spawnDebris(generateData(distanceRange / earthDiameter, rotationRange), "LeoDebris " + numLeoDebris));
        }
    }

    public void SpawnMEODebris()
    {
        Vector2 distanceRange = MEOParams.distanceRange;
        float rotationRange = 360f;
        if (MEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            numMeoDebris++;
            debrisSet.Add(spawnDebris(generateData(distanceRange / earthDiameter, rotationRange), "MeoDebris " + numMeoDebris));
        }
    }

    public void SpawnHEODebris()
    {
        Vector2 distanceRange = HEOParams.distanceRange;
        float rotationRange = 360f;
        if (HEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            numHeoDebris++;
            debrisSet.Add(spawnDebris(generateData(distanceRange / earthDiameter, rotationRange), "HeoDebris " + numHeoDebris));
        }
    }

    public void SpawnGEODebris()
    {
        Vector2 distanceRange = GEOParams.distanceRange;
        float rotationRange = 360f;
        if (GEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            numGeoDebris++;
            debrisSet.Add(spawnDebris(generateData(distanceRange / earthDiameter, rotationRange), "GeoDebris " + numGeoDebris));
        }
    }

    private OrbitalData generateData(Vector2 distanceRange, float rotationRange)
    {

        float perigee = Random.Range(distanceRange.x, distanceRange.y);
        float apogee = Random.Range(perigee, distanceRange.y);

        float argumentOfPerigee = Random.Range(0f, 360f);
        float inclination = Random.Range(0f, rotationRange);
        float RAAN = Random.Range(0f, rotationRange);

        OrbitalData data = ScriptableObject.CreateInstance<OrbitalData>();

        data.initializePerigeeApogee(perigee, apogee)
             .initializeRotation(argumentOfPerigee, inclination, RAAN)
             .initializeEpochAnomoly(new System.DateTime(2000, 1, 1), 0);

        return data;
    }

    private GameObject spawnDebris(OrbitalData data, string name) 
    {

        GameObject gameObject = Instantiate(debrisPrefab);
        gameObject.name = name;
        OrbitalPath orbit = gameObject.GetComponent<OrbitalPath>();

        orbit.setTarget(this.gameObject)
             .setOrbitalData(data);

        return gameObject;
    }

    void Start()
    {
        Vector2 distanceRange = new Vector2(0, 35786);
        debrisSet.Clear();
        debrisSet.Add(spawnDebris(generateData(distanceRange / earthDiameter, 0f), "startingDebris"));
    }
}
