using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOutline : MonoBehaviour
{
    private Outline _outline;
    private bool _isActive;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = _isActive;
    }

    public void Action()
    {
        _isActive = !_isActive;
        _outline.enabled = _isActive;
    }
}
