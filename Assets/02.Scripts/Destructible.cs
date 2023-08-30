using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyedObject;
    [SerializeField]
    private GameObject defaultObject;

    private void OnMouseDown()
    {
        destroyedObject.SetActive(true);
        defaultObject.SetActive(false);
    }
}
