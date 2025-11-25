using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject defaultTarget;
    GameObject target;
    float scale = 1f;
    Vector3 offset = Vector3.zero;

    float horizontalAngle = 0;
    float verticalAngle = 0;
    [SerializeField] float distance = 2;

    [Range(.1f, 180)][SerializeField]public float distanceScaling = 10;

    [SerializeField]public float cameraDeceleration = 3;

    public Vector2 previousMousePosition = Vector2.zero;
    public Vector2 mouseVelocity = Vector2.zero;

    private void Awake()
    {
        resetTarget();
    }

    private void Start()
    {
        Vector2 normalized = new Vector2(position.x / Screen.width, position.y / Screen.height);
        previousMousePosition = normalized;
    }

    void LateUpdate()
    {
        scale = target.transform.localScale.x;
        offset = target.transform.localPosition;

        handleMouseInput();
        HandleZoomInput();
        UpdateCameraTransform();
    }

    private void UpdateCameraTransform()
    {
        Quaternion rotation = Quaternion.identity;
        rotation *= Quaternion.Euler(verticalAngle, horizontalAngle, 0);

        Vector3 cameraLocation = new Vector3(0, 0, -(distance + (.5f * scale)));
        cameraLocation = (rotation * cameraLocation) + offset;

        transform.position = cameraLocation;
        transform.rotation = rotation;
    }

    private void handleMouseInput()
    {
        Vector2 normalized = new Vector2(position.x / Screen.width, position.y / Screen.height);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            previousMousePosition = normalized;
        }

        if (Mouse.current.leftButton.IsPressed())
        {
            mouseVelocity = previousMousePosition - normalized;
            previousMousePosition = normalized;
        }
        else
        {
            mouseVelocity = Vector2.Lerp(mouseVelocity, Vector2.zero, Time.deltaTime * cameraDeceleration);
        }

        horizontalAngle -= mouseVelocity.x * 180;
        verticalAngle += mouseVelocity.y * 180;
        clampAngle();
    }

    private void clampAngle() 
    {
        if (horizontalAngle < 0)
        {
            horizontalAngle += 360;
        }
        if (verticalAngle < -90)
        {
            verticalAngle = -90;
        }

        if (horizontalAngle > 360)
        {
            horizontalAngle -= 360;
        }
        if (verticalAngle > 90)
        {
            verticalAngle = 90;
        }
    }

    private void HandleZoomInput()
    {
        float changeInDistance = (-Mouse.current.scroll.ReadValue().y) / distanceScaling;
        distance = distance + changeInDistance * Mathf.Min(distance/2, 1);
        clampDistance();
    }

    private void clampDistance() 
    {
        if (distance < (.2f) * scale) 
        {
            distance = (.2f) * scale;
        }
    }

    public void setTarget(GameObject target) 
    {
        this.target = target;
    }

    public void resetTarget() 
    {
        this.target = defaultTarget;
    }
}
