using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private Animator _canvasAnimator;
    [SerializeField] private PlayerInventory _playerInventoryScript;
    [SerializeField] private int _needHhoneycombs;
    [SerializeField] private int _needEggs;
    private bool _isNearPlayer;
    private int currentHoney;
    private int currentEggs;
    [SerializeField] private GameObject _empty;
    [SerializeField] private GameObject _fool;
    private bool _isFool;
    [SerializeField] private RoomManager _roomManager;

    private void Start()
    {
        _playerInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ButtonShow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        { 
            ButtonHide();
        }
    }

    private void Update()
    {
        if (_isNearPlayer)
        {
            if (!_isFool)
            {
                if (Input.GetKeyDown(KeyCode.E)) Action();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ActionFool();
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) ChengeMode();
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

    public void Action()
    {
        currentHoney += _playerInventoryScript.currentHoney;
        currentEggs += _playerInventoryScript.currentEggs;

        _playerInventoryScript.UseResurse();

        if (currentEggs >= _needEggs && currentHoney >= _needHhoneycombs)
        {
            ChengeMode();
            _roomManager.NewStage();
        }
    }

    public void ChengeMode()
    {
        _empty.SetActive(false);
        _fool.SetActive(true);
        _isFool = true;
    }

    private void ActionFool()
    {
        Debug.Log("+++");
        if (_playerInventoryScript.CheckFreeCell()) _playerInventoryScript.GetResurse(ResurseType.Soup);
    }
}
