using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakModel : MonoBehaviour
{
    private List<Rigidbody> _modelRigidList = null;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_modelRigidList != null) return;
        _modelRigidList = new List<Rigidbody>();
        _modelRigidList.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public void Explosion(Vector3 explosionPos, float power, float radius)
    {
        Init();
        _modelRigidList.ForEach((x) => x.AddExplosionForce(power, explosionPos, radius, 3.0f));
    }
}
