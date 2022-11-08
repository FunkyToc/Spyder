using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectAction : MonoBehaviour
{
    [SerializeField,Range(0f,10f)] float _detectionDelay = 1f;
    [SerializeField, Range(0f, 10f)] float _deathAnimTime = 1f;
    Coroutine _detectedCoroutine;

    [Header("Events")]
        public UnityEvent OnDetectionKill;

    // gère le timer de lock / kill
    // 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
        // sound + anim
        yield return new WaitForSeconds(delay);
        // death animation
        OnDetectionKill?.Invoke();
        StartCoroutine(GoCheckpointDelay(_deathAnimTime));
    }

    IEnumerator GoCheckpointDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.GM._dm.LastCheckPoint();
    }
}
