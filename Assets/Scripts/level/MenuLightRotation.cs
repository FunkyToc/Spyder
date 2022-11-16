using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLightRotation : MonoBehaviour
{
    [SerializeField] bool _active = false;
    [SerializeField,Range(0f,10f)] float _speed = 1f;
    Quaternion _rotation;

    void Update()
    {
        if (_active)
        {
            _rotation = transform.rotation;
            _rotation *= Quaternion.Euler(0, _speed * Time.deltaTime, 0);
            transform.rotation = _rotation;
        }
    }
}
