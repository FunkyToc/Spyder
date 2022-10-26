/* 
 * This file is part of Unity-Procedural-IK-Wall-Walking-Spider on github.com/PhilS94
 * Copyright (C) 2020 Philipp Schofield - All Rights Reserved
 */

using UnityEngine;
using System.Collections;
using Raycasting;
using UnityEngine.InputSystem;
using System;

/*
 * This class needs a reference to the Spider class and calls the walk and turn functions depending on player input.
 * So in essence, this class translates player input to spider movement. The input direction is relative to a camera and so a 
 * reference to one is needed.
 */

[DefaultExecutionOrder(-1)] // Make sure the players input movement is applied before the spider itself will do a ground check and possibly add gravity
public class SpiderController : MonoBehaviour {

    public Spider spider;

    [Header("Camera")]
    public SmoothCamera smoothCam;

    [Header("Inputs")]
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _run;
    [SerializeField] InputActionReference _release;
    Vector3 _moveInput = Vector2.zero;
    bool _runInput = false;
    bool _releaseInput = false;

    void Start()
    {
        _move.action.performed += MovePerformInput;
        _move.action.canceled += MoveCancelInput;

        _run.action.started += RunStartInput;
        _run.action.canceled += RunCancelInput;

        _release.action.started += ReleaseStartInput;
        _release.action.canceled += ReleaseCancelInput;
    }

    void OnDestroy()
    {
        _move.action.performed -= MovePerformInput;
        _move.action.canceled -= MoveCancelInput;

        _run.action.started -= RunStartInput;
        _run.action.canceled -= RunCancelInput;

        _release.action.started -= ReleaseStartInput;
        _release.action.canceled -= ReleaseCancelInput;
    }

    void MovePerformInput(InputAction.CallbackContext cc)
    {
        _moveInput = cc.ReadValue<Vector2>();
    }

    void MoveCancelInput(InputAction.CallbackContext cc)
    {
        _moveInput = Vector2.zero;
    }

    void RunStartInput(InputAction.CallbackContext cc)
    {
        _runInput = true;
    }

    void RunCancelInput(InputAction.CallbackContext cc)
    {
        _runInput = false;
    }

    void ReleaseStartInput(InputAction.CallbackContext cc)
    {
        _releaseInput = true;
    }

    void ReleaseCancelInput(InputAction.CallbackContext cc)
    {
        _releaseInput = false;
    }

    void Update()
    {
        spider.setGroundcheck(!_releaseInput);
    }

    void FixedUpdate()
    {
        //** Movement **//
        Vector3 input = getInput();

        if (_runInput)
        {
            spider.run(input);
        }
        else
        {
            spider.walk(input);
        }

        Quaternion tempCamTargetRotation = smoothCam.getCamTargetRotation();
        Vector3 tempCamTargetPosition = smoothCam.getCamTargetPosition();
        spider.turn(input);
        smoothCam.setTargetRotation(tempCamTargetRotation);
        smoothCam.setTargetPosition(tempCamTargetPosition);
    }

    private Vector3 getInput()
    {
        Vector3 up = spider.transform.up;
        Vector3 right = spider.transform.right;
        Vector3 input = Vector3.ProjectOnPlane(smoothCam.getCameraTarget().forward, up).normalized * _moveInput.y + (Vector3.ProjectOnPlane(smoothCam.getCameraTarget().right, up).normalized * _moveInput.x);
        Quaternion fromTo = Quaternion.AngleAxis(Vector3.SignedAngle(up, spider.getGroundNormal(), right), right);
        input = fromTo * input;
        float magnitude = input.magnitude;
        return (magnitude <= 1) ? input : input /= magnitude;
    }
}