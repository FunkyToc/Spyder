using UnityEngine;
using System.Collections;

public class LookAtSlerp : MonoBehaviour
{
    enum LookAtStrategy {
        ListOrder,
        Closest
    }

    [Tooltip("Look rotation update frequency. Lower value use more performances.")]
    [SerializeField,Range(0.001f, 0.1f)] float _updateFrequency = 0.02f;

    [Tooltip("Targets to look at when in one followed area. Odered by priority.")]
    [SerializeField] Transform[] _followTargets;

    [Tooltip("Collider wich represent the area of detection. If no target, LookAt fall back instead.")]
    [SerializeField] Collider[] _followAreas;

    [Tooltip("Targets to look at when in one followed area.")]
    [SerializeField] LookAtStrategy _followStrategy;

    [Tooltip("If the target is not in range, Fallback will be pickup instead. If null, default self.forward")]
    [SerializeField] Transform _targetFallback;

    [Tooltip("Look rotation speed multiplier.")]
    [SerializeField, Range(0.0f, 10.0f)] float _rotationSpeed = 3f;

    [Tooltip("Lock X gameobject rotation")]
    [SerializeField] bool _lockX = false;

    [Tooltip("Lock Y gameobject rotation")]
    [SerializeField] bool _lockY = false;

    private Vector3 _targetStartPos;
    private Vector3 _lookPos;

    void Start()
    {
        _targetStartPos = transform.position + transform.forward;
        _lookPos = _targetStartPos;
        StartCoroutine(UpdateLookRotation());
    }

    void OnDestroy()
    {
        StopCoroutine(UpdateLookRotation());
    }

    IEnumerator UpdateLookRotation()
    {
        WaitForSeconds waiter = new WaitForSeconds(_updateFrequency);

        while (true)
        {
            _lookPos = GetLookPos();
            LookAtTarget();

            yield return waiter;
        }
    }

    void LookAtTarget()
    {
        _lookPos.x = _lockX ? transform.localPosition.x : _lookPos.x;
        _lookPos.y = _lockY ? transform.localPosition.y : _lookPos.y;
        Quaternion lookRotation = Quaternion.LookRotation(_lookPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _updateFrequency * _rotationSpeed);
    }

    Vector3 GetLookPos()
    {
        if (_followTargets == null || _followAreas == null)
        {
            return _targetFallback != null ? _targetFallback.position : _targetStartPos;
        }
        else
        {
            Collider[] colls;
            Vector3 selectedTarget = Vector3.zero;

            foreach (Transform t in _followTargets)
            {
                colls = Physics.OverlapSphere(t.position, 0f);

                foreach (Collider coll in colls)
                {
                    foreach (Collider coll2 in _followAreas)
                    {
                        if (coll.GetInstanceID() == coll2.GetInstanceID())
                        {
                            switch (_followStrategy)
                            {
                                case LookAtStrategy.ListOrder:
                                    return t.position;

                                case LookAtStrategy.Closest:
                                    if (selectedTarget == Vector3.zero) selectedTarget =  t.position; // first iteration
                                    if ((t.position - transform.position).magnitude < (selectedTarget - transform.position).magnitude)
                                    {
                                        selectedTarget = t.position;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return selectedTarget;
        }
    }
}