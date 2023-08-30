using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakModel : MonoBehaviour
{
    private List<Rigidbody> modelRigidList = null;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (modelRigidList != null) return;
        modelRigidList = new List<Rigidbody>();
        modelRigidList.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public void Explosion(Vector3 explosionPos, float power, float radius)
    {
        Init();
        modelRigidList.ForEach((x) => x.AddExplosionForce(power, explosionPos, radius, 3.0f));
    }
}
