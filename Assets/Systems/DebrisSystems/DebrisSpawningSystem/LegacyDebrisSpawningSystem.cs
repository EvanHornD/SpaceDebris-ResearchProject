using UnityEngine;

public class LegacyDebrisSpawningSystem : MonoBehaviour
{
    [SerializeField] public DebrisManagementSystem managementSystem;

    [SerializeField] public OrbitalParameters LEOParams;
    [SerializeField] public OrbitalParameters MEOParams;
    [SerializeField] public OrbitalParameters HEOParams;
    [SerializeField] public OrbitalParameters GEOParams;

    static float earthDiameter = 12756f;

    [SerializeField]
    public int numDebris = 10;

    public void SpawnLEO()
    {
        Vector2 distanceRange = LEOParams.distanceRange;
        float rotationRange = 360f;
        if (LEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            spawnDebris(generateData(distanceRange / earthDiameter, rotationRange));
        }
    }

    public void SpawnMEO()
    {
        Vector2 distanceRange = MEOParams.distanceRange;
        float rotationRange = 360f;
        if (MEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            spawnDebris(generateData(distanceRange / earthDiameter, rotationRange));
        }
    }

    public void SpawnHEO()
    {
        Vector2 distanceRange = HEOParams.distanceRange;
        float rotationRange = 360f;
        if (HEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            spawnDebris(generateData(distanceRange / earthDiameter, rotationRange));
        }
    }

    public void SpawnGEO()
    {
        Vector2 distanceRange = GEOParams.distanceRange;
        float rotationRange = 360f;
        if (GEOParams.geoLocked)
        {
            rotationRange = 0.1f;
        }

        for (int i = 0; i < numDebris; i++)
        {
            spawnDebris(generateData(distanceRange / earthDiameter, rotationRange));
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

    public void spawnDebris(OrbitalData data) 
    {
        managementSystem.createDebris(data);
    }

    void Start()
    {
        Vector2 distanceRange = new Vector2(0, 35786);
        spawnDebris(generateData(distanceRange / earthDiameter, 0f));
    }
}
