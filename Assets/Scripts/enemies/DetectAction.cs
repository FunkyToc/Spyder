using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DetectAction : MonoBehaviour
{
    [SerializeField] PlayerSO _player;
    [SerializeField,Range(0f,10f)] float _detectionDelay = 1f;
    [SerializeField, Range(0f, 10f)] float _deathAnimTime = 1f;
    Coroutine _detectedCoroutine;
    GameObject _model;

    [Header("Events")]
        public UnityEvent OnDetectionKill;

    void Start()
    {
        _model = _player.Player.GetComponentInChildren<PlayerModelTag>().gameObject;
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
        yield return new WaitForSeconds(delay);

        // if coroutine still pending -> kill
        GameManager.GM._dm.SpawnRagdoll();
        OnDetectionKill?.Invoke();
        StartCoroutine(GoCheckpointDelay(_deathAnimTime));
        _player.Player.GetComponent<PlayerInput>().enabled = false;
        _model.SetActive(false);
    }

    IEnumerator GoCheckpointDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.GM._dm.LastCheckPoint();
        _model.SetActive(true);
        _player.Player.GetComponent<PlayerInput>().enabled = true;
    }
}
