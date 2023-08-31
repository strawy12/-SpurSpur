using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private BreakModel destroyedObject;
    [SerializeField]
    private GameObject defaultObject;

    public float power = 100;
    public float radius = 100;

    [ContextMenu("Setting")]
    public void Setting()
    {
        defaultObject = transform.GetChild(0).gameObject;
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

        destroyedObject = transform.GetChild(1).AddComponent<BreakModel>();
        destroyedObject.gameObject.name = "BreakModel";

        defaultObject.AddComponent<Rigidbody>();
        defaultObject.GetComponent<Rigidbody>().isKinematic = true;
        defaultObject.AddComponent<BoxCollider>();

        foreach(Transform child in destroyedObject.transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<MeshCollider>().convex = true;
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        Explosion();
    }

    public void Explosion()
    {
        defaultObject.SetActive(false);
        destroyedObject.gameObject.SetActive(true);
        destroyedObject.Explosion(defaultObject.transform.position, power, radius);
    }


}
