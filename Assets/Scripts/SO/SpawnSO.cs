using UnityEngine;

interface ISetSpawnSO
{
    void Set(Transform go);
}

[CreateAssetMenu()]
public class SpawnSO : ScriptableObject, ISetSpawnSO
{
    [SerializeField] Transform _spawn;
    public Transform Spawn { get => _spawn; }

    void ISetSpawnSO.Set(Transform go)
    {
        _spawn = go;
    }
}