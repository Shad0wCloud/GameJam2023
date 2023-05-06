using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isOpened;
    [SerializeField] private bool _isWork = true;
    [SerializeField] private bool _isOneUse = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_isOpened) _animator.SetBool("isOpen", true);
    }

    public void Action()
    {
        if (_isWork)
        {
            if (_isOpened) _animator.SetBool("isOpen", false);
            else _animator.SetBool("isOpen", true);
            _isOpened = !_isOpened;
        }

        if (_isOneUse) _isWork = false;
    }




}
