using UnityEngine;

public class DebrisEventsHandler : MonoBehaviour
{
    [SerializeField] public DebrisManagementSystem managementSystem;
    [SerializeField] public DebrisSelectionSystem selectionSystem;
    [SerializeField] public LegacyDebrisSpawningSystem spawningSystem;

    public void removeDebris(GameObject debris) 
    {
        managementSystem.removeDebris(debris);
        selectionSystem.checkRemovedDebris(debris);
    }

    public void clearSet()
    {
        managementSystem.clearSet();
    }

    public void selectDebris(GameObject debris)
    {
        selectionSystem.selectDebris(debris);
    }

    public void deselectDebris() 
    {
        selectionSystem.deselectDebris();
    }

    public void spawnLeo() 
    {
        spawningSystem.SpawnLEO();
    }

    public void spawnMeo()
    {
        spawningSystem.SpawnMEO();
    }

    public void spawnHeo()
    {
        spawningSystem.SpawnHEO();
    }

    public void spawnGeo()
    {
        spawningSystem.SpawnGEO();
    }

}
