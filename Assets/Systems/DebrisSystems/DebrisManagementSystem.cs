using UnityEngine;

public class DebrisManagementSystem : MonoBehaviour
{
    public Collection<GameObject> debrisSet;
    public DebrisFactory debrisFactory;
    public VoidEvent deselectDebrisEvent;

    public void clearSet()
    {
        deselectDebrisEvent.Raise();
        foreach (GameObject obj in debrisSet.Items)
        {
            Destroy(obj);
        }
        debrisSet.Clear();
    }

    public void createDebris(OrbitalData parameters) 
    {
        addDebris(debrisFactory.createDebris(parameters));
    }

    public void addDebris(GameObject debris) 
    {
        debrisSet.Add(debris);
    }

    public void removeDebris(GameObject debris) 
    {
        debrisSet.Remove(debris);
    }
}
