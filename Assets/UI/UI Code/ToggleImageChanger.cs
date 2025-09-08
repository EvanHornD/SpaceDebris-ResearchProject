
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageChanger : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    [SerializeField] Image imageComponent;

    [SerializeField] Sprite toggledSprite;
    [SerializeField] Sprite untoggledSprite;

    void Awake()
    {
        ToggleImage(toggle.isOn);
    }

    public void ToggleImage(bool toggled) 
    {
        if (toggled) imageComponent.sprite = toggledSprite;
        else imageComponent.sprite = untoggledSprite;
    }
}
