using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RagdollDeath : MonoBehaviour
{
    [SerializeField] bool _enabled = false;
    [SerializeField] UnityEvent _OnEnable;

    void Start()
    {
        gameObject.SetActive(_enabled);
        StartCoroutine(CleanRagdoll());
    }

    public void EnableRagdoll()
    {
        _enabled = true;
        gameObject.SetActive(_enabled);
        _OnEnable?.Invoke();
    }

    public void DisableRagdoll()
    {
        _enabled = false;
        gameObject.SetActive(_enabled);
    }

    IEnumerator CleanRagdoll()
    {
        yield return new WaitForSeconds(3f);
        // clean unneeded comp
    }

    public void Remove()
    {
        DisableRagdoll();
        Destroy(gameObject);
    }
}
