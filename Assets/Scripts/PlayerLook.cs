using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] InputActionReference _lookInput;
    [SerializeField] float _lookSpeed;

    PlayerMove _pmoveScript;
    Animator _animator;
    float _lookVector;

    void Start()
    {
        _lookInput.action.started += lookStarted;
        _lookInput.action.performed += lookPerformed;
        _lookInput.action.canceled += lookCanceled;

        _animator = GetComponent<Animator>();
        _pmoveScript = GetComponent<PlayerMove>();
    }

    void Destroy()
    {
        _lookInput.action.started -= lookStarted;
        _lookInput.action.performed -= lookPerformed;
        _lookInput.action.canceled -= lookCanceled;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _lookVector);
    }

    private void lookStarted(InputAction.CallbackContext cc)
    {
        _lookVector = cc.ReadValue<Vector2>().x * _lookSpeed * Time.fixedDeltaTime;
        
        if (_pmoveScript.moveVector == Vector3.zero)
        {
            _animator.SetFloat("move", Mathf.Abs(_lookVector / 10));
        }
    }

    private void lookPerformed(InputAction.CallbackContext cc)
    {
        _lookVector = cc.ReadValue<Vector2>().x * _lookSpeed * Time.fixedDeltaTime;
        
        if (_pmoveScript.moveVector == Vector3.zero)
        {
            _animator.SetFloat("move", Mathf.Abs(_lookVector / 10));
        }
    }

    private void lookCanceled(InputAction.CallbackContext cc)
    {
        _lookVector = 0f;

        if (_pmoveScript.moveVector == Vector3.zero)
        {
            _animator.SetFloat("move", 0f);
        }
    }
}
