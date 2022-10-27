using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ViewInput : MonoBehaviour
{
    [SerializeField] InputActionReference _zoomInput;
    [SerializeField] CinemachineFreeLook _freelookCam;

    void Start()
    {
        _zoomInput.action.started += ZoomStarted;
        _zoomInput.action.canceled += ZoomCanceled;
    }

    void OnDestroy()
    {
        _zoomInput.action.started -= ZoomStarted;
        _zoomInput.action.canceled -= ZoomCanceled;
    }

    void Update()
    {
        
    }

    private void ZoomStarted(InputAction.CallbackContext obj)
    {
        _freelookCam.gameObject.SetActive(true);
    }
    
    private void ZoomCanceled(InputAction.CallbackContext obj)
    {
        _freelookCam.gameObject.SetActive(false);
    }
}
