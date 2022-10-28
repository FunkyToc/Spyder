using UnityEngine;

interface ISetPlayerSO
{
    void Set(GameObject go);
}

[CreateAssetMenu()]
public class PlayerSO : ScriptableObject, ISetPlayerSO
{
    
    GameObject _player;
    public GameObject Player { get => _player; }

    void ISetPlayerSO.Set(GameObject go)
    {
        _player = go;
    }
}