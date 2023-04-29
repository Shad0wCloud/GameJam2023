using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _primaryCamera;
    [SerializeField] private CinemachineVirtualCamera[] _virtualCamera;

    [SerializeField] private CinemachineVirtualCamera _currentCamera;
    [SerializeField] public CinemachineVirtualCamera activeCamera;
    [SerializeField] private CinemachineVirtualCamera _temporaryCamera;

    [SerializeField] private LayerMask _groundLayerMask;

    private void Start()
    {
        SwitchToCamera(_primaryCamera, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CamZone2"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera, false);
        }
        else if (other.CompareTag("CamZone1"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CamZone1"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            if (activeCamera == targetCamera) SwitchToCamera(_primaryCamera, false);
            else _currentCamera = _primaryCamera;
        }
        else if (other.CompareTag("CamZone2"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            if (activeCamera = targetCamera) SwitchToCamera(_currentCamera, true);
        }
    }

    private void SwitchToCamera (CinemachineVirtualCamera targetCamera, bool isTemporary)
    {
        foreach (CinemachineVirtualCamera camera in _virtualCamera)
        {
            camera.enabled = camera == targetCamera;
        }

        if (!isTemporary)
        {
            _currentCamera = targetCamera;
        }

        activeCamera = targetCamera;
    }
}
