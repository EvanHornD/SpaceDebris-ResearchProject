using UnityEngine;

public class TextSender : MonoBehaviour
{
    [SerializeField] public StringEvent stringEvent;
    [SerializeField] string text = "lol \nlol";

    public void sendText() 
    {
        stringEvent.Raise(text);
    }
}
