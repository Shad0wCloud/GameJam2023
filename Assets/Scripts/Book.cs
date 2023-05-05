using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private Animator _canvasAnimator;
    [SerializeField] private Dialog _dialogScript;
    [SerializeField] private int _numberReplic;
    private bool _isNearPlayer;
    public bool isRead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonShow();
            _isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonHide();
            _isNearPlayer = false;
        }
    }

    private void Update()
    {
        if (_isNearPlayer && !isRead)
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

    private void Action()
    {
        isRead = true;
        _dialogScript.StartDialog(_numberReplic);
        _dialogScript.ReadBoock(this);
    }
}
