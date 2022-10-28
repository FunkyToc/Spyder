using UnityEngine;

public class SpawnTag : MonoBehaviour
{
    [SerializeField] SpawnSO _ref;

    private void Awake()
    {
        (_ref as ISetSpawnSO).Set(gameObject.transform);
    }
}