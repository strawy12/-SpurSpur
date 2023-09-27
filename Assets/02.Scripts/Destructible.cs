using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private BreakModel _destroyedObject;
    [SerializeField]
    private GameObject _defaultObject;

    public float _power = 100;
    public float _radius = 100;

    private bool _isDestructed = false;

    [ContextMenu("Setting")]
    public void Setting()
    {
        _defaultObject = transform.GetChild(0).gameObject;
        if(transform.childCount > 2)
        {
            GameObject parent = new GameObject("BreakModel");
            parent.transform.SetParent(transform);
            List<Transform> children = new List<Transform>();
            for (int i = 1;  i <  transform.childCount; i++)
            {
                if (transform.GetChild(i) == parent.transform) continue;
                children.Add(transform.GetChild(i));
            }

            children.ForEach(x=>x.transform.SetParent(parent.transform));
        }

        _destroyedObject = transform.GetChild(1).AddComponent<BreakModel>();
        _destroyedObject.gameObject.name = "BreakModel";

        _defaultObject.AddComponent<Rigidbody>();
        _defaultObject.GetComponent<Rigidbody>().isKinematic = true;
        _defaultObject.AddComponent<BoxCollider>();

        foreach(Transform child in _destroyedObject.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<MeshCollider>().convex = true;
        }
    }

    public void Explosion()
    {
        if (_isDestructed) return;
        _isDestructed = true;
        _defaultObject.SetActive(false);
        _destroyedObject.gameObject.SetActive(true);
        _destroyedObject.Explosion(_defaultObject.transform.position, _power, _radius);
    }


}
