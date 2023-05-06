using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResurseType
{
    Honeycombs,
    Egg
}

public class Resurse : MonoBehaviour
{
    public ResurseType resurseType;

    private Outline _outlineScript;
    private PlayerInventory _playerInventoryScript;
    [SerializeField] private Animator _canvasAnimator;

    private bool _isNearPlayer = false;

    private void Start()
    {
        _playerInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

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
        if (_isNearPlayer)
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
        bool isCheck = _playerInventoryScript.CheckFreeCell();
        if (isCheck)
        {
            _playerInventoryScript.GetResurse(resurseType);
            Destroy(this.gameObject);
        }
    }


}
