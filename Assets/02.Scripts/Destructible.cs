using System.Collections;
using System.Collections.Generic;
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
