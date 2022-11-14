using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

enum Danger
{
    Kill,
    KillDelay,
    KillAfterDelay
}

public class DangerWall : MonoBehaviour
{
    [Header("Params")]
        [SerializeField] Danger _killTarget;
        [SerializeField] Collider _killCollider;
        [SerializeField,Range(0f,10f)] float _killDelay = 3f;
        [SerializeField, Range(0f, 10f)] float _deathAnimTime = 1f;

    [Header("Events")]
        [SerializeField] UnityEvent _OnCollide;
        [SerializeField] UnityEvent _OnExit;
        [SerializeField] UnityEvent _OnKill;

    Coroutine _killCoroutine;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.TryGetComponent<SurfaceTrigger>(out SurfaceTrigger st))
        {
            switch (_killTarget)
            {
                // Insta kill
                case Danger.Kill:
                    _OnCollide?.Invoke();
                    _OnKill?.Invoke();
                    _killCoroutine = StartCoroutine(KillCoroutine(0));
                    break;
                
                // Kill 100%, but wait delay
                case Danger.KillDelay:
                    _OnCollide?.Invoke();
                    _killCoroutine = StartCoroutine(KillCoroutine(_killDelay));
                    break;
                
                // May kill, if the target still collide after the delay
                case Danger.KillAfterDelay:
                    _OnCollide?.Invoke();
                    _killCoroutine = StartCoroutine(KillCoroutine(_killDelay));
                    break;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.TryGetComponent<SurfaceTrigger>(out SurfaceTrigger st))
        {
            switch (_killTarget)
            {
                // Cancel death
                case Danger.KillAfterDelay:
                    _OnExit?.Invoke();
                    Reset();
                    break;
            }
        }
    }

    IEnumerator KillCoroutine(float wait)
    {
        yield return new WaitForSeconds(wait);
        
        _OnKill?.Invoke();
        GameManager.GM._dm.DeathWithRagdoll(_deathAnimTime);
        Reset();
    }

    void Reset()
    {
        if (_killCoroutine != null) StopCoroutine(_killCoroutine);
        _killCoroutine = null;
    }
}
