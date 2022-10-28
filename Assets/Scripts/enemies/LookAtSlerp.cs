using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LookAtSlerp : MonoBehaviour
{
    enum LookAtPriority {
        Closest,
        ListOrder
    }

    enum PatrolPattern
    {
        Static,
        Rotation,
        PatrolList        
    }

    [System.Serializable]
    struct PatrolPoint
    {
        [SerializeField] Vector3 position;
        [SerializeField] float transitionDuration;
        [SerializeField] AnimationCurve SlerpCurve;
    }

    [Header("Script Settings")]

        [Tooltip("Look rotation update frequency. Lower value use more performances.")]
        [SerializeField,Range(0.001f, 0.1f)] float _updateFrequency = 0.02f;

    
    [Header("Targets")]

        [Tooltip("Targets to look at when multiple targets detected in followed areas.")]
        [SerializeField] LookAtPriority _lookAtPriority;

        [Tooltip("Targets to look at when in one followed area. Odered by priority.")]
        [SerializeField] Transform[] _targets;

    
    [Header("Detection")]

        [Tooltip("Colliders wich represent the area of detection. If a target comes in, it looks at target. If no target, it patrols instead.")]
        [SerializeField] Collider[] _detectionAreas;
    
    
    [Header("Patroling")]

        [Tooltip("Targets to look at when multiple targets detected in followed areas.")]
        [SerializeField] PatrolPattern _patrolPattern;

        [Tooltip("If Static pattern, look at this gameobject. If null, default self.forward")]
        [SerializeField] Transform _staticPatrolPos;

        [Tooltip("If Rotation pattern, define the rotation speed per seconds. Can be a negative value.")]
        [SerializeField,Range(0f,359f)] float _rotationSpeedPatrol = 45f;

        [Tooltip("If PatrolList pattern, iterate with the list")]
        [SerializeField] List<PatrolPoint> _patrolPoints;


    [Header("Head Rotations")]

        [Tooltip("Look rotation speed multiplier.")]
        [SerializeField, Range(0.0f, 10.0f)] float _rotationSpeed = 3f;

        [Tooltip("Lock X gameobject rotation")]
        [SerializeField] bool _lockX = false;

        [Tooltip("Lock Y gameobject rotation")]
        [SerializeField] bool _lockY = false;

    private Vector3 _targetStartPos;
    private Vector3 _lookPos;
    private GameObject _currentTarget = null;

    void Start()
    {
        // notices
        if (_targets == null) throw new Exception("No assigned target: this gameobject will only patrol.");
        if (_detectionAreas == null) throw new Exception("No assigned detection areas: this gameobject won't be able to detect targets.");

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
            switch (_currentTarget)
            {
                case null:
                    Patrol();
                    break;

                default :
                    Follow();
                    break;
            }

            yield return waiter;
        }
    }

    void Patrol()
    {
        switch (_patrolPattern)
        {
            case PatrolPattern.Static:
                _lookPos = (_staticPatrolPos != null ? _staticPatrolPos.position : _targetStartPos);
                break;

            case PatrolPattern.Rotation:
                transform.rotation = transform.rotation * Quaternion.Euler(0, _rotationSpeedPatrol * _updateFrequency, 0);
                break;

            case PatrolPattern.PatrolList:
                // TODO
                _lookPos = Vector3.zero;
                break;

            default:
                _lookPos = _targetStartPos;
                break;
        }

        CheckTarget();
    }

    void Follow()
    {
        _lookPos = _currentTarget.transform.position;        
        if (!CheckTarget())
        {
            _lookPos = _targetStartPos;
        }

        LookAtTarget(_lookPos);
    }

    void LookAtTarget(Vector3 pos)
    {
        pos.x = _lockX ? transform.localPosition.x : pos.x;
        pos.y = _lockY ? transform.localPosition.y : pos.y;
        Quaternion lookRotation = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _updateFrequency * _rotationSpeed);
    }

    /*
     * Update _currentTarget
     * Set it to GameObject if found, or null if not found
     * Return bool : found or not found
     */
    bool CheckTarget()
    {
        GameObject selectedTarget = null;
        
        // check targets
        if (_targets != null || _detectionAreas != null)
        {
            Collider[] colls;

            foreach (Transform t in _targets)
            {
                colls = Physics.OverlapSphere(t.position, 0f);

                foreach (Collider coll in colls)
                {
                    foreach (Collider coll2 in _detectionAreas)
                    {
                        if (coll.GetInstanceID() == coll2.GetInstanceID())
                        {
                            switch (_lookAtPriority)
                            {
                                case LookAtPriority.ListOrder:
                                    _currentTarget = t.gameObject;
                                    return true; ;

                                case LookAtPriority.Closest:
                                    if (selectedTarget == null) selectedTarget = t.gameObject; // first iteration
                                    if ((t.position - transform.position).magnitude < (selectedTarget.transform.position - transform.position).magnitude)
                                    {
                                        selectedTarget = t.gameObject;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        // found target
        if (selectedTarget != null)
        {
            _currentTarget = selectedTarget;
            return true;
        }

        // no target
        _currentTarget = null;
        return false;
    }

}