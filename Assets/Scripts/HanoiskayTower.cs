using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HanoiskayTower : MonoBehaviour
{
    private CameraMove _cameraMoveScript;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private bool _isActive = false;

    private Vector3 pointScreen;
    private Vector3 offset;

    private void Start()
    {
        _cameraMoveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraMove>();
    }

    private void OnMouseDown()
    {
        pointScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointScreen.z));
    }

    private void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointScreen.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = currentPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _cameraMoveScript.SwitchToCameraForMission(_virtualCamera, !_isActive);
            _isActive = !_isActive;
        }
    }
}
