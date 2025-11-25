using UnityEngine;

public class DebrisSpawningSystem : MonoBehaviour
{
    [SerializeField] DebrisFactory factory;
    static float earthDiameter = 12756f;
    static float leo = 2000f / earthDiameter;
    static float meo = 35786f / earthDiameter;
    static float geo = 35786f / earthDiameter;
    static float heo = 375000f / earthDiameter;

    public GameObject spawnDebris(DebrisEntry entry, bool fillWithRandom) 
    {
        OrbitalData data = GenerateData(entry, fillWithRandom);

        return factory.createDebris(data, entry.NAME);
    }

    static private OrbitalData GenerateData(DebrisEntry entry, bool fillWithRandom) 
    {
        OrbitalData data = new OrbitalData();

        if (ParseElipticalData(data, entry)) { }
        else if (fillWithRandom)
        {
            float perigee = Random.Range(160f / earthDiameter, 375000f / earthDiameter);
            float apogee = Random.Range(perigee, 375000f / earthDiameter);

            data.initializePerigeeApogee(perigee, apogee);
        }
        else
        {
            data.initializePerigeeApogee(0,0);
        }

        if (ParseRotation(data, entry.TLELINE0, entry.TLELINE1, entry.TLELINE2)) { }
        else if (fillWithRandom)
        {
            float argumentOfPerigee = Random.Range(0f, 360f);
            float inclination = Random.Range(0f, 360f);
            float RAAN = Random.Range(0f, 360f);

            data.initializeRotation(argumentOfPerigee, inclination, RAAN);
        }
        else
        {
            data.initializeRotation(0,0,0);
        }

        return data;
    }

    static private bool ParseElipticalData(OrbitalData data, DebrisEntry entry)
    {
//        if (ParseTLE(data, entry.TLELINE0, entry.TLELINE1, entry.TLELINE2)) return true;
//        if (ParseMajorMinorAxis(data, entry.TLELINE0, entry.TLELINE1)) return true;
//        if (ParsePerigeeApogee(data, entry.TLELINE0, entry.TLELINE1, entry.TLELINE2)) return true;
//        if (ParsePerigeeEccentricity(data, entry.TLELINE0, entry.TLELINE1, entry.TLELINE2)) return true;
//        if (ParseMeanMotionEccentricity(data, entry.TLELINE0, entry.TLELINE1, entry.TLELINE2)) return true;
        if (ParseOrbitalClassification(data, entry.ORBITALCLASSIFICATION)) return true;

        return false;
    }
    static private bool ParseTLE(OrbitalData data ,string TLELINE0, string TLELINE1, string TLELINE2)
    {
        return false;
    }

    static private bool ParseMajorMinorAxis(OrbitalData data, string SEMIMAJORAXIS, string SEMIMINORAXIS)
    {
        return false;
    }

    static private bool ParsePerigeeApogee(OrbitalData data, string TLELINE0, string TLELINE1, string TLELINE2)
    {
        return false;
    }

    static private bool ParsePerigeeEccentricity(OrbitalData data, string TLELINE0, string TLELINE1, string TLELINE2)
    {
        return false;
    }

    static private bool ParseMeanMotionEccentricity(OrbitalData data, string TLELINE0, string TLELINE1, string TLELINE2)
    {
        return false;
    }

    static private bool ParseOrbitalClassification(OrbitalData data, string ORBITALCLASSIFICATION) 
    {
        Debug.Log(ORBITALCLASSIFICATION);

        Vector2 distanceRange;

        switch (ORBITALCLASSIFICATION)
        {
            case "LEO": distanceRange = new Vector2(160f / earthDiameter, leo); break;
            case "MEO": distanceRange = new Vector2(leo, meo); break;
            case "HEO": distanceRange = new Vector2(meo, heo); break;
            case "GEO": distanceRange = new Vector2(geo, geo); break;
            default: return false;
        }

        float perigee = Random.Range(distanceRange.x, distanceRange.y);
        float apogee = Random.Range(perigee, distanceRange.y);

        data.initializePerigeeApogee(perigee, apogee);

        return true;
    }

    static private bool ParseRotation(OrbitalData data, string TLELINE0, string TLELINE1, string TLELINE2)
    {
        return false;
    }      


    // unparsed information todo
    private string NAME;
    private int CATALOGNUMBER;
    private string OBJECTTYPE;
    private float MEANANOMOLY;
    private int EPOCHTIME;
    private float ARGUMENTOFPERIGEE;
    private float INCLINATION;
    private float RAAN;
    private float PERIOD;
    private int REVOLUTIONNUMBER;
    private float DRAGCOEFFICIENT;
    private float AREA;
    private float MASS;
}
