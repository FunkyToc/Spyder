using UnityEngine;
using UnityEngine.Events;

public class SafeFallingCollider : MonoBehaviour
{
    [SerializeField] UnityEvent _OnCollide;

    private void OnCollisionEnter(Collision coll)
    {
        GameManager.GM._dm.LastCheckPoint();
        _OnCollide?.Invoke();
    }
}