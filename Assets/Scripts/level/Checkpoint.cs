using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Collider _checkpointCollider;
    [SerializeField] Transform _respawnPosition;
    [SerializeField] SpawnSO _spawnSO;

    void OnValidate()
    {
        if (!_checkpointCollider && TryGetComponent<Collider>(out Collider coll))
        {
            _checkpointCollider = coll;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        (_spawnSO as ISetSpawnSO).Set(_respawnPosition.transform);
    }
}
