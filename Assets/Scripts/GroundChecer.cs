using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecer : MonoBehaviour
{
    [SerializeField] private PlayerController _playerControllerScript;
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _playerControllerScript = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _groundLayer)
        {
            _playerControllerScript._isGrpunded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _groundLayer)
        {
            _playerControllerScript._isGrpunded = false;
        }
    }


}
