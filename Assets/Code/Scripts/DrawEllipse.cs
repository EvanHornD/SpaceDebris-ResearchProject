
using Unity.Collections;
using UnityEngine;

public class DrawEllipse : MonoBehaviour
{
    [SerializeField] float width = .05f;
    [SerializeField] bool visible = true;
    Color color = new Color(0, 64, 255, .5f);

    private LineRenderer lineRenderer;

    public DrawEllipse setWidth(float width) 
    {
        this.width = width;
        return this;
    }

    public DrawEllipse setColor(Color color)
    {
        this.color = color;
        return this;
    }

    public void createEllipse(Vector3[] points) 
    {
        gameObject.AddComponent<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.enabled = visible;
        lineRenderer.loop = true;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    public void updateEllipse(Vector3[] points) 
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    void OnDestroy() 
    { 
        DestroyImmediate(lineRenderer);
    }
}
