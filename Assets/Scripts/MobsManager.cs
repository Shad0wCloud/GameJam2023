using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsManager : MonoBehaviour
{
    [SerializeField] private RoomManager _roomManager;
    [SerializeField] private PlayerScale _playerScale;

    [SerializeField] private Door _beeDoorScript;
    [SerializeField] private Door _chickenDoorScript;

    [SerializeField] private bool[] _beeNumbers = { false, false ,false ,false};
    [SerializeField] private bool[] _chickenNumbers = { false, false, false, false, false, false};
    private bool _isAllBee;
    private bool _isAllChicken;

    [SerializeField] private bool _isPlayerInFarm;

    private void OnTriggerEnter(Collider other)
    {
        _isPlayerInFarm = true;

        if (_isAllBee) CloseDooor(_beeDoorScript);
        if (_isAllChicken) CloseDooor(_chickenDoorScript);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _isPlayerInFarm = false;
    }

    public void ActionBee(Bee bee)
    {
        _beeNumbers[bee.id] = true;

        CheckCountBee();

        _playerScale.NewScale();

        if (_isAllBee && _isPlayerInFarm)
        {
            CloseDooor(_beeDoorScript);
            CheckAllCount();
        }
    }

    public void ActionChecken(Chiken chicken)
    {
        _chickenNumbers[chicken.id] = true;

        CheckCountChicken();

        _playerScale.NewScale();

        if (_isAllBee && _isPlayerInFarm)
        {
            CloseDooor(_chickenDoorScript);
            CheckAllCount();
        }
    }

    private void CheckCountBee()
    {
        bool isAll = true;
        foreach (bool iter in _beeNumbers)
        {
            if (!iter)
            {
                isAll = false;
                break;
            }
        }
        if (isAll) _isAllBee = true;
    }

    private void CheckCountChicken()
    {
        bool isAll = true;
        foreach (bool iter in _chickenNumbers)
        {
            if (!iter)
            {
                isAll = false;
                break;
            }
        }
        if (isAll) _isAllChicken = true;
    }

    private void CheckAllCount()
    {
        if (_isAllBee && _isAllChicken) _roomManager.NewStage();
    }

    private void CloseDooor(Door door)
    {
        door.Action();
    }
}
