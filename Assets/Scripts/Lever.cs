using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private bool _isLock = false;
    [SerializeField] private bool _isRight;
    [SerializeField] private bool _isNearPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _canvasAnimator;
    [SerializeField] private bool _isOneUse;
    private bool _isUse;

    [SerializeField] private GameObject[] _targetObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isLock)
        {
            ButtonShow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !_isLock)
        {
            ButtonHide();
        }
    }

    void Start()
    {
        if (_isRight) _animator.SetBool("isRight", true);
        else _animator.SetBool("isRight", false);
    }

    private void Update()
    {
        if (!_isLock)
        {
            if (_isNearPlayer)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    LeverActivate();
                }
            }
        }
    }

    private void ButtonShow()
    {
        _canvasAnimator.SetBool("isNearPlayer", true);
        _isNearPlayer = true;
    }

    private void ButtonHide()
    {
        _canvasAnimator.SetBool("isNearPlayer", false);
        _isNearPlayer = false;
    }

    public void LeverActivate()
    {
        if (_isRight) _animator.SetBool("isRight", false);
        else _animator.SetBool("isRight", true);
        _isRight = !_isRight;
    }

    public void Action()
    {
        foreach (GameObject iter in _targetObjects)
        {
            if (iter.GetComponent<Door>()) iter.GetComponent<Door>().Action();
            else if (iter.GetComponent<OffObject>()) iter.GetComponent<OffObject>().Action();
            else if (iter.GetComponent<RoomManager>())
            {
                iter.GetComponent<RoomManager>().NewStage();
               // gameObject.SetActive(false);
            }
        }

        if(_isOneUse)
        {
            _isLock = true;
            ButtonHide();
        }
    }

    public void UnLock()
    {
        _isLock = false;
    }
}
