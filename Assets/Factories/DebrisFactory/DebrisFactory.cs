using UnityEngine;

public class DebrisFactory: MonoBehaviour
{
    public GameObject debrisPrefab;
    public GameObject target;
    [SerializeField] public GameObject parentObject;

    public GameObject createDebris(OrbitalData data) 
    {
        GameObject gameObject = Instantiate(debrisPrefab);
        if (parentObject != null) 
        {
            gameObject.transform.SetParent(parentObject.transform, false);
        }
        gameObject.name = "Debris";
        OrbitalPath orbit = gameObject.GetComponent<OrbitalPath>();

        orbit.setTarget(target)
             .setOrbitalData(data);

        return gameObject;
    }
}
