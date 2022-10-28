using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliperyWall : MonoBehaviour
{
    [SerializeField,Range(0f,1f)] float _sliperyFactor = 0.5f;
    [SerializeField,Range(0f,1f)] float _speedMultiplier = 0.8f;
    Coroutine _sliperyCoroutine;
    Spider _spider;
    Rigidbody _rb;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.TryGetComponent<SurfaceTrigger>(out SurfaceTrigger st))
        {
            // Set target
            _spider = st.gameObject.GetComponentInParent<Spider>();
            _rb = _spider.GetComponent<Rigidbody>();

            // Slow effect
            _spider.SurfaceSpeedMultiplier = _speedMultiplier;

            // Slipery Force
            if (_sliperyFactor != 0f)
            {
                _sliperyCoroutine = StartCoroutine(SliperyForce());
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.TryGetComponent<SurfaceTrigger>(out SurfaceTrigger st))
        {
            // Slow effect
            _spider.SurfaceSpeedMultiplier = 1f;

            // Slipery Force
            if (_sliperyCoroutine != null)
            {
                StopCoroutine(_sliperyCoroutine);
                _rb.AddForce(Vector3.up * _sliperyFactor * 100);
                _sliperyCoroutine = null;
            }
            
            // Unset target
            _spider = null;
            _rb = null;
        }
    }

    IEnumerator SliperyForce()
    {
        WaitForFixedUpdate waiter = new WaitForFixedUpdate();

        while (_rb != null)
        {
            _rb.AddForce(-Vector3.up * _sliperyFactor * 20);

            yield return waiter;
        }
    }
}
