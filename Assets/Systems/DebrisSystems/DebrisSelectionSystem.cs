using UnityEngine;

public class DebrisSelectionSystem : MonoBehaviour
{
    private GameObject selectedDebris;
    public void selectDebris(GameObject debris)
    {
        deselectDebris();
        selectedDebris = debris;
        debris.GetComponent<DebrisSelector>().select(debris);
    }

    public void deselectDebris() 
    {
        if (selectedDebris == null) return;

        selectedDebris.GetComponent<DebrisSelector>().deselect();
    }

    public void checkRemovedDebris(GameObject removedDebris) 
    {
        if (removedDebris == selectedDebris) deselectDebris();
    }
}
