using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] LayerMask _walkableLayer;
    [SerializeField] Transform[] _groundedPoints;
    [SerializeField,Range(0.1f, 10f)] float _groundedRayLength = 3f;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _gravityScale;
    bool _grounded;
    Vector3 _normal;

    private void Reset()
    {
        _walkableLayer = LayerMask.GetMask("Ground", "Wall");
        _grounded = false;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        _grounded = getGrounded();
        
        if (_grounded)
        {
            _normal = getNormal();
            _rb.AddForce(-_normal * (_gravityScale * 100) * Time.fixedDeltaTime, ForceMode.Acceleration);
            transform.rotation = Quaternion.LookRotation(_rb.transform.forward * Time.fixedDeltaTime, _normal);
        }
        else
        {
            _rb.AddForce(-Vector3.up * (_gravityScale * 100) * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    public bool getGrounded()
    {
        foreach (Transform tr in _groundedPoints)
        {
            if (Physics.Raycast(tr.position, tr.forward, out RaycastHit hit, _groundedRayLength, _walkableLayer))
            {
                return true;
            }
        }

        return false;
    }

    Vector3 getNormal()
    {
        int points = 0;
        Vector3 normals = Vector3.zero;

        foreach (Transform tr in _groundedPoints)
        {
            if (Physics.Raycast(tr.position, tr.forward, out RaycastHit hit, _groundedRayLength, _walkableLayer))
            {
                normals += hit.normal;
                points++;
            }
        }

        if (points > 0)
        {
            normals /= points;

            return normals;
        }

        return _normal;
    }

    private void OnDrawGizmos()
    {
        // grounded
        Gizmos.color = Color.red;
        foreach (Transform tr in _groundedPoints)
        {
            Gizmos.DrawLine(tr.position, tr.position + tr.forward * _groundedRayLength);
        }

        // normal
        _normal = getNormal();
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(transform.position, _normal * 5));
    }
}
