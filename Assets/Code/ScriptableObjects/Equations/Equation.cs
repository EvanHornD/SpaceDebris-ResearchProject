using System;
using UnityEngine;

public abstract class Equation : ScriptableObject
{
    public abstract float evaluate(float x);

    public virtual float inverse(float y)
    {
        Debug.Log("The Inverse Method was not implemented");
        return float.NaN;
    }
}
