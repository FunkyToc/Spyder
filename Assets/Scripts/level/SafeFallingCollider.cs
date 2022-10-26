using UnityEngine;

public class SafeFallingCollider : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;

    private void OnCollisionEnter(Collision coll)
    {
        coll.rigidbody.transform.position = _spawnPoint.position;
    }
}