using UnityEngine;

public class testingScript : MonoBehaviour
{
    [SerializeField] RuntimeSet<GameObject> set;
    [SerializeField] GameObject prefab;

    public void Add()
    {
        GameObject testObj = Instantiate(prefab);
        testObj.name = testObj.name + (set.Items.Count+1);
        set.Add(testObj);
    }

    public void Remove() 
    {
        if (set.Items.Count > 0)
        {
            Destroy(set.Items[set.Items.Count - 1]);
            set.Remove(set.Items[set.Items.Count - 1]);
        }
    }
}
