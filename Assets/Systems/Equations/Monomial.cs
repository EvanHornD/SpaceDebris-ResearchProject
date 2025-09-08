using UnityEngine;

[CreateAssetMenu(fileName = "Equation", menuName = "Equations/Monomial")]
public class Monomial : Equation
{
    [SerializeField] public float Multiplier = 1f;
    [SerializeField] public float Exponent = 1f;
    [SerializeField] public float Constant = 0f;

    public override float evaluate(float x)
    {
        return (Multiplier*Mathf.Pow(x,Exponent))+Constant;
    }

    public override float inverse(float y)
    {
        return Mathf.Pow((y - Constant) / Multiplier,1f/Exponent);
    }
}
