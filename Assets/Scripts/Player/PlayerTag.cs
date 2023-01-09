using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    [SerializeField] PlayerSO _ref;

    private void Start()
    {
        (_ref as ISetPlayerSO).Set(gameObject);
    }
}