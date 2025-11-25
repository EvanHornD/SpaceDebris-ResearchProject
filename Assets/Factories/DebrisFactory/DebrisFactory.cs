using System.Xml.Linq;
using UnityEngine;

public class DebrisFactory: MonoBehaviour
{
    public GameObject debrisPrefab;
    public GameObject target;
    [SerializeField] public GameObject parentObject;

    public GameObject createDebris(OrbitalData data, string name = "Debris") 
    {
        GameObject gameObject = Instantiate(debrisPrefab);
        if (parentObject != null) 
        {
            gameObject.transform.SetParent(parentObject.transform, false);
        }
        gameObject.name = name;
        OrbitalPath orbit = gameObject.GetComponent<OrbitalPath>();

        orbit.setTarget(target)
             .setOrbitalData(data);

        return gameObject;
    }
}
