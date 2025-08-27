
using System.Collections.Generic;
using UnityEngine;

public class SliderModifier : MonoBehaviour
{
    [SerializeField] List<GameEvent<float>> moveSlider;
    [Space]
    [SerializeField] Equation equation;
    [SerializeField] UnityEngine.UI.Slider slider;

    [SerializeField] int numberOfDecimalPlaces = 1;

    public enum StartPos {Left,Middle,Right}
    [SerializeField] StartPos SliderStartingPosition = StartPos.Middle;
    private void Start()
    {
        slider.value = SliderPositionToValue(SliderStartingPosition);
    }

    private float SliderPositionToValue(StartPos pos)
    {
        return pos switch
        {
            StartPos.Left => 0f,
            StartPos.Middle => 0.5f,
            StartPos.Right => 1f,
            _ => 0.5f
        };
    }

    public void SliderInput(float value)
    {
        if (justchanged) { justchanged = false; return; }
        float scaledValue = equation.evaluate(value);
        int roundingScale = (int)Mathf.Pow(10f, numberOfDecimalPlaces);
        float roundedValue = Mathf.Round(scaledValue*roundingScale)/roundingScale;

        foreach (GameEvent<float> e in moveSlider)
        {
            e.Raise(roundedValue);
        }
    }


    private bool justchanged = false;
    public void SystemInput(float value) 
    {
        justchanged = true;
        slider.value = equation.inverse(value);
    }

}
