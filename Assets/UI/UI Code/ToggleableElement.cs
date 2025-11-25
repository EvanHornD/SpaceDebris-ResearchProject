using UnityEngine;
public class ToggleableElement : MonoBehaviour
{

    [SerializeField] RectTransform content;
    RectTransform rectTransform;
    public enum Direction {Left, Right, Up, Down};

    [SerializeField] bool startToggledOn = true;
    [SerializeField] bool MoveOnToggle = false;
    [SerializeField] Direction ToggleDirection = Direction.Right;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (!startToggledOn) Toggle();
    }

    bool toggled = true;
    public void Toggle()
    {
        toggled = !toggled;
        content.gameObject.SetActive(toggled);

        if (!MoveOnToggle) return;
        if  (toggled) transform.position -= getToggleOffset(ToggleDirection);
        if (!toggled) transform.position += getToggleOffset(ToggleDirection);
    }

    private Vector3 getToggleOffset(Direction direction)
    {
        return direction switch
        {
            Direction.Left  => new Vector3(-rectTransform.rect.width, 0, 0),
            Direction.Right => new Vector3( rectTransform.rect.width, 0, 0),
            Direction.Up    => new Vector3(0, rectTransform.rect.height, 0),
            Direction.Down  => new Vector3(0,-rectTransform.rect.height, 0),
            _ => new Vector3(0, 0, 0)
        };
    }
}
