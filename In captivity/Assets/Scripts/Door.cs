using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isOpen;
    [SerializeField] private Animator _animator;

    void Start()
    {
        if (_isOpen) _animator.SetBool("isOpen", true);
        else _animator.SetBool("isOpen", false);
    }

    public void Action()
    {
        if (_isOpen) _animator.SetBool("isOpen", false);
        else _animator.SetBool("isOpen", true);
        _isOpen = !_isOpen;
    }
}
