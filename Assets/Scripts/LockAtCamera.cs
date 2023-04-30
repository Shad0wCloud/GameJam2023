using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAtCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_camera.transform);
    }
}
