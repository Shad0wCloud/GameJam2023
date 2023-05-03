using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private bool _isActive;

    private void Awake()
    {
        if (_object == null) _object = gameObject;
        if (_isActive) _object.SetActive(true);
        else _object.SetActive(false);
    }

    public void Action()
    {
        _object.SetActive(!_isActive);
        _isActive = !_isActive;
    }
}
