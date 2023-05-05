using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Animator _canvasAnimator;
    [SerializeField] private bool _isOpened = false;
    [SerializeField] private bool _isOneUse = false;
    private bool _isUse = true;
    private bool _isNearPlayer;
    [SerializeField] private bool _isClosed = false;
    [SerializeField] private GameObject[] _targetObjects;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_isOpened)
        {
            _animator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isClosed)
        {
            ButtonShow();
            _isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_isClosed)
        {
            ButtonHide();
            _isNearPlayer = false;
        }
    }

    private void Update()
    {
        if(_isNearPlayer && !_isClosed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Action();
            }
        }
    }

    private void ButtonShow()
    {
        _canvasAnimator.SetBool("isNearPlayer", true);
    }

    private void ButtonHide()
    {
        _canvasAnimator.SetBool("isNearPlayer", false);

    }

    public void Action()
    {
        if (!_isOneUse || (_isOneUse && _isUse))
        {
            if (!_isOpened)
            {
                _animator.SetBool("isOpen", true);
                _isOpened = true;
            }
            else
            {
                _animator.SetBool("isOpen", false);
                _isOpened = false;
            }

            if (_isOneUse)
            {
                _isClosed = true;
                ButtonHide();
            }
        }
    }

    public void OtherAction()
    {
        foreach (GameObject iter in _targetObjects)
        {
            if (iter.GetComponent<Door>()) iter.GetComponent<Door>().Action();
            else if (iter.GetComponent<OffObject>()) iter.GetComponent<OffObject>().Action();
            else if (iter.GetComponent<AdvantGhost>()) iter.GetComponent<AdvantGhost>().Action();
            else if (iter.GetComponent<ActiveOutline>()) iter.GetComponent<ActiveOutline>().Action();
            else if (iter.GetComponent<PlayerScale>()) iter.GetComponent<PlayerScale>().NewScale();
        }
    }
}
