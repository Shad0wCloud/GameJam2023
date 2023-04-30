using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isOpened;
    private Animator _animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Action();
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_isOpened) _animator.SetBool("isOpen", true);
    }

    public void Action()
    {
        if (_isOpened) _animator.SetBool("isOpen", false);
        else _animator.SetBool("isOpen", true);
        _isOpened = !_isOpened;
    }




}
