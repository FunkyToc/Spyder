using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    [SerializeField] PlayerSO _ref;

    private void Awake()
    {
        (_ref as ISetPlayerSO).Set(gameObject);
    }
}