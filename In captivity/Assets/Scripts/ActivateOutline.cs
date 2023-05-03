using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOutline : MonoBehaviour
{
    private Outline _outlineScript;
    [SerializeField] private bool _isActive;

    private void Awake()
    {
        _outlineScript = GetComponent<Outline>();
        _outlineScript.enabled = _isActive;
    }

    public void Action()
    {
        _outlineScript.enabled = !_isActive;
        _isActive = !_isActive;
    }
}
