using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectAction : MonoBehaviour
{
    [SerializeField] PlayerSO _player;
    [SerializeField,Range(0f,10f)] float _detectionDelay = 1f;
    [SerializeField, Range(0f, 10f)] float _deathAnimTime = 1f;
    Coroutine _detectedCoroutine;

    [Header("Events")]
        public UnityEvent OnDetectionKill;

    public void OnDetectionEnter()
    {
        _detectedCoroutine = StartCoroutine(DetectDelay(_detectionDelay));
    }

    public void OnDetectionExit()
    {
        StopCoroutine(_detectedCoroutine);
    }

    IEnumerator DetectDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // if coroutine still pending -> kill
        OnDetectionKill?.Invoke();
        GameManager.GM._dm.DeathWithRagdoll(_deathAnimTime, true);
    }
}
