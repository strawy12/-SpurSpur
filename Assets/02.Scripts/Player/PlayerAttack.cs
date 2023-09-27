using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 0.75f;

    private bool _isAttack = false;

    public UnityEvent OnAttack;
    public UnityEvent OnAttackEnd;

    public Vector3 _offset;
    public Vector3 _size;

    public void StartAttack()
    {
        if (_isAttack) return;
        _isAttack = true;

        OnAttack?.Invoke();
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_attackDelay);

        if (_isAttack)
        {
            _isAttack = false;
            OnAttackEnd?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _offset, _size);

    }

    public void DetectedObject()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + _offset, _size);

        foreach (Collider hit in hits) 
        {
            Destructible a = hit.GetComponentInParent<Destructible>();
            if(a != null)
            {
                a.Explosion();
            }
        }

    }
}

