using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalData", menuName = "Scriptable Objects/OrbitalData")]
public class OrbitalData : ScriptableObject
{
    private static float EARTHDIAMETER = 12756f;

    public float meanAnomoly = 0;
    public DateTime epochTime;
    public float argumentOfPerigee = 0;
    public float inclination = 0;
    public float RAAN = 0;
    public float PeriodSeconds = 0;
    public float semiMajorAxis = 0;
    public float semiMinorAxis = 0;

    public OrbitalData initializePerigeeApogee(float perigee, float apogee)
    {
        float eccentricity = ((apogee + .5f) - (perigee + .5f)) / ((apogee + .5f) + (perigee + .5f)); // This equation was created by Dang Pham in under 2 hours. very impressive!!
        semiMajorAxis = .5f + ((perigee + apogee) / 2);
        semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - (eccentricity * eccentricity));
        PeriodSeconds = (2 * Mathf.PI) * Mathf.Sqrt(((semiMajorAxis * EARTHDIAMETER) * (semiMajorAxis * EARTHDIAMETER) * (semiMajorAxis * EARTHDIAMETER)) / 398600.4418f);
        return this;
    }

    public OrbitalData initializePerigeeEccentricity(float perigee, float eccentricity)
    {
        float apogee = (((perigee + .5f) * (1 + eccentricity)) / (1 - eccentricity)) - .5f;// This equation was created by Dang Pham in under 2 hours. very impressive!!
        semiMajorAxis = .5f + ((perigee + apogee) / 2);
        semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - (eccentricity * eccentricity));
        PeriodSeconds = (2 * Mathf.PI) * Mathf.Sqrt(((semiMajorAxis * EARTHDIAMETER) * (semiMajorAxis * EARTHDIAMETER) * (semiMajorAxis * EARTHDIAMETER)) / 398600.4418f);
        return this;
    }

    public OrbitalData initializeMeanMotionEccentricity(float meanMotion, float eccentricity)
    {
        float T = (1 / meanMotion) * 86400;
        PeriodSeconds = T;
        semiMajorAxis = Mathf.Pow((398600.4418f * (T * T)) / (4 * (Mathf.PI * Mathf.PI)), 1f / 3f) / EARTHDIAMETER;
        semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - (eccentricity * eccentricity));
        return this;
    }

    public OrbitalData initializeMajorMinorAxis(float semiMajorAxis, float semiMinorAxis)
    {
        this.semiMajorAxis = semiMajorAxis;
        this.semiMinorAxis = semiMinorAxis;
        PeriodSeconds = (2 * Mathf.PI) * Mathf.Sqrt(((semiMajorAxis * EARTHDIAMETER) * (semiMajorAxis * EARTHDIAMETER) * (semiMajorAxis * EARTHDIAMETER)) / 398600.4418f);
        return this;
    }

    public OrbitalData initializeRotation(float argumentOfPerigee, float inclination, float RAAN)
    {
        this.argumentOfPerigee = argumentOfPerigee;
        this.inclination = inclination;
        this.RAAN = RAAN;
        return this;
    }

    public OrbitalData initializeEpochAnomoly(DateTime epochTime, float meanAnomoly)
    {
        this.epochTime = epochTime;
        this.meanAnomoly = meanAnomoly;
        return this;
    }

    private void OnEnable()
    {
        if (semiMajorAxis != 0 || semiMinorAxis != 0 && PeriodSeconds == 0) 
        {
            initializeMajorMinorAxis(semiMajorAxis, semiMinorAxis);
        }
    }

}
