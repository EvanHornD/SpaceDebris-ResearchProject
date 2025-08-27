

using UnityEngine;

public class DebrisSelector : MonoBehaviour
{
    public VoidEventListener deselectListener;

    [SerializeField]
    [Range(3, 60)]
    public int resolution = 30;

    private OrbitalPath orbit;
    private DrawEllipse ellipse;

    private void Awake()
    {
        orbit = gameObject.GetComponent<OrbitalPath>();
    }

    public void showEllipse()
    {
        if (ellipse != null) return;

        gameObject.AddComponent<DrawEllipse>();
        ellipse = GetComponent<DrawEllipse>();
        ellipse.setWidth(.05f)
               .createEllipse(orbit.getPoints(resolution));
    }

    public void hideEllipse()
    {
        DestroyImmediate(ellipse);
        ellipse = null;
    }

    private void OnValidate()
    {
        updateEllipse();
    }

    public void updateEllipse() 
    {
        if (ellipse == null) return;

        ellipse.updateEllipse(orbit.getPoints(resolution));
    }

    public void select(GameObject debris)
    {
        showEllipse();
        deselectListener.Response.AddListener(deselect);
    }

    public void deselect()
    {
        hideEllipse();
        deselectListener.Response.RemoveListener(deselect);
    }

    private void OnDestroy()
    {
        deselect();
    }
}
