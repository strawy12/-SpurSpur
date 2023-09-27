using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public static CameraChanger Inst{get; private set;}
    public static readonly int TPS_HASH = Animator.StringToHash("TPS_Cam");
    public static readonly int SPUR_START_HASH = Animator.StringToHash("SpurStart_Cam");

    private Animator _animator;

    private void Awake()
    {
        Inst = this;
        _animator = GetComponent<Animator>();
    }

    public void CameraChange(int stateHash)
    {
        _animator.Play(stateHash);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            CameraChange(SPUR_START_HASH);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CameraChange(TPS_HASH);
        }
    }
}
