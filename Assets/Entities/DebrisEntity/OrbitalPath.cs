using NUnit.Framework.Internal;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbitalPath : MonoBehaviour
{
    private static float twoPI = Mathf.PI+Mathf.PI;

    [SerializeField] public SimulationTime time;
    [SerializeField] public OrbitalData orbitalData;

    [SerializeField] private GameObject target;
    float scale = 1;
    private Vector3 offset = Vector3.zero;

    Matrix4x4 rotationMatrix;

    float fociOffset;

    private void Start()
    {
        if (target != null) 
        {
            scale = target.transform.localScale.x;
            offset = target.transform.localPosition;
        }

        if (orbitalData != null)
        {
            init();
        }
    }

    public OrbitalPath setTarget(GameObject target)
    {
        this.target = target;
        scale = target.transform.localScale.x;
        offset = target.transform.localPosition;
        return this;
    }

    public void setOrbitalData(OrbitalData orbitalData) 
    {
        this.orbitalData = orbitalData;
        init();
    }

    private OrbitalPath init() 
    {
        // get foci offset using the equation c = sqrt(|a^2 - b^2|)
        fociOffset = Mathf.Sqrt(Mathf.Abs(((orbitalData.semiMajorAxis) * (orbitalData.semiMajorAxis)) - ((orbitalData.semiMinorAxis) * (orbitalData.semiMinorAxis))));

        // in - plane roation of the y axis using argumentOfPerigee
        Quaternion inPlaneRotation = Quaternion.Euler(0, orbitalData.argumentOfPerigee, 0);
        Quaternion raanRotation = Quaternion.Euler(0, orbitalData.RAAN, 0);
        Quaternion inclinationRotation = Quaternion.Euler(0, 0, orbitalData.inclination);
        Quaternion finalRotation = raanRotation * inclinationRotation * inPlaneRotation;

        rotationMatrix = Matrix4x4.Rotate(finalRotation);

        initialized = true;
        return this;
    }

    bool initialized = false;
    float T = 0;
    private void Update()
    {
        if (!initialized) {
            return;
        }

        if (time.paused) {
            return;
        }

        scale = target.transform.localScale.x;
        offset = target.transform.localPosition;

        float orbitPercentage = ((time.changeInTime/ orbitalData.PeriodSeconds)%1);
        T += (orbitPercentage * (twoPI)); // Completely Temporary I will find the proper formula for this eventually
        T -= orbitalData.meanAnomoly;
        transform.localPosition = getPoint(T);
    }

    public Vector3 getPoint(float t)
    {
        float x = ((orbitalData.semiMajorAxis * scale) * Mathf.Cos(t));
        float z = ((orbitalData.semiMinorAxis * scale) * Mathf.Sin(t));

        Vector3 point = new Vector3(x + (fociOffset * scale), 0, z);
        point = rotationMatrix.MultiplyPoint3x4(point);
        point = point + offset;

        return point;
    }

    public Vector3[] getPoints(int numPoints)
    {
        Vector3[] points = new Vector3[numPoints];

        float t = 0;
        float step = (twoPI) / numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            points[i] = getPoint(t);
            t += step;
        }

        return points;
    }
}
