using NUnit.Framework.Internal;
using System;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public Boolean immoveable = true;
    public void toggleMovement(Boolean boolean) 
    {
        immoveable = boolean;
    }
    public void TestMethod()
    {
        if (immoveable) { return; }
        this.gameObject.transform.position += new Vector3(0,1,0);
    }
}
