using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private int _hashAttack = Animator.StringToHash("Attack");
    private int _hashVelocity = Animator.StringToHash("Velocity");

    public UnityEngine.Events.UnityEvent OnPlayAttackAnim;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_animator != null) return;

        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnim()
    {
        Init();
        OnPlayAttackAnim?.Invoke(); 
        _animator.SetTrigger(_hashAttack);
    }

    public void AnimateAgent(float velocity)
    {
        Init();
        _animator.SetFloat(_hashVelocity, velocity);
    }
}
