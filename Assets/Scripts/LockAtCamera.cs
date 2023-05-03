using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAtCamera : MonoBehaviour
{
    private Camera _camera;
    private Transform _targetLokAt;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _targetLokAt = GameObject.Find("TargetLookAt").transform;
    }

    private void Update()
    {
        //   transform.LookAt(_camera.transform);
        transform.LookAt(_targetLokAt);

        Vector3 dir = _targetLokAt.position - transform.position;
        Debug.DrawRay(transform.position, dir, Color.yellow);
    }
}
