using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTarget : MonoBehaviour
{
    [SerializeField] NavMeshAgent _nma;
    [SerializeField] Transform _followTargets;
    [SerializeField] Collider _followRange;
    [SerializeField] Collider _attackRange;

    void Start()
    {

    }

    void Update()
    {
        if ((transform.position - _followTargets.position).magnitude < 2f)
        {
            _nma.SetDestination(_followTargets.position);
        }
    }
}
