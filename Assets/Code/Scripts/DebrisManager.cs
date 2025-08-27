using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    public RuntimeSet<GameObject> debrisSet;
    public VoidEvent debrisDeselectedEvent;

    public void clearSet()
    {
        debrisDeselectedEvent.Raise();
        foreach (GameObject obj in debrisSet.Items) Destroy(obj);
        debrisSet.Clear();
    }

    public void selectDebris(GameObject debris)
    {
        debris.GetComponent<DebrisSelector>().select(debris);
    }
}
