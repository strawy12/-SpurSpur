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

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Explosion();
    }

    public void Explosion()
    {
        defaultObject.SetActive(false);
        destroyedObject.gameObject.SetActive(true);
        destroyedObject.Explosion(defaultObject.transform.position, power, radius);
    }
}
