using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameQuad : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Start()
    {
        transform.LookAt(_target);
    }

    private void Update()
    {
        transform.LookAt(_target);        
    }
}
