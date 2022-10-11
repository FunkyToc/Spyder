using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _sprintInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] float _speed;
    [SerializeField] float _sprintMultiplier = 1.5f;
    [SerializeField] float _jumpForce;

    Rigidbody _rb;
    Animator _animator;
    PlayerGravity _PlayeGravityScript;
    float speed;
    Vector3 _moveVector;
    public Vector3 moveVector { get { return _moveVector; } private set { _moveVector = value; } }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _PlayeGravityScript = GetComponent<PlayerGravity>();
        speed = _speed;

        _moveInput.action.started += moveStarted;
        _moveInput.action.performed += moveStarted;
        _moveInput.action.canceled += moveCanceled;

        _sprintInput.action.started += sprintStarted;
        _sprintInput.action.canceled += sprintCanceled;

        _jumpInput.action.started += jumpStarted;
        _jumpInput.action.canceled += jumpCanceled;
    }

    void Destroy()
    {
        _moveInput.action.started -= moveStarted;
        _moveInput.action.performed -= moveStarted;
        _moveInput.action.canceled -= moveCanceled;

        _sprintInput.action.started -= sprintStarted;
        _sprintInput.action.canceled -= sprintCanceled;

        _jumpInput.action.started -= jumpStarted;
        _jumpInput.action.canceled -= jumpCanceled;
    }

    void FixedUpdate()
    {
        var dir = _moveVector * speed * Time.fixedDeltaTime;
        dir = _rb.transform.TransformDirection(dir);

        _rb.MovePosition(_rb.transform.position + dir);
    }

    private void moveStarted(InputAction.CallbackContext cc)
    {
        Vector2 t = cc.ReadValue<Vector2>();
        moveVector = new Vector3(t.x, 0, t.y);
        _animator.SetFloat("move", Mathf.Abs(moveVector.magnitude / 2));
    }

    private void moveCanceled(InputAction.CallbackContext cc)
    {
        moveVector = Vector3.zero;
        _animator.SetFloat("move", Mathf.Abs(moveVector.magnitude / 2));
        _animator.SetBool("sprint", false);
    }

    private void sprintStarted(InputAction.CallbackContext cc)
    {
        speed = _speed * _sprintMultiplier;
        _animator.SetBool("sprint", true);
    }
     
    private void sprintCanceled(InputAction.CallbackContext cc)
    {
        speed = _speed;
        _animator.SetBool("sprint", false);
    }

    private void jumpStarted(InputAction.CallbackContext cc)
    {
        if (_PlayeGravityScript.getGrounded())
        {
            _rb.AddForce(_rb.transform.up * _jumpForce * 100);
            _animator.SetBool("jump", true);
        }
    }

    private void jumpCanceled(InputAction.CallbackContext cc)
    {
        _animator.SetBool("jump", false);
    }

}
