using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HanoiskayTower : MonoBehaviour
{
    private bool _isReady = false;

    private Vector3 pointScreen;
    private Vector3 offset;

    public Transform[] _positionOnTower;

    [SerializeField] public GameObject[] _idEtalon;
    private GameObject[] _idArray = { null, null, null, null, null, null, null};

    [SerializeField] private Door _doorScript;
    [SerializeField] private RoomManager _roomManagerScript;
    [SerializeField] private Dialog _dialogScript;


    public Vector3 TorPosotion(bool isStand, GameObject obj)
    {
        if (isStand)
        {
            int i = 0;
            foreach (GameObject iter in _idArray)
            {
                if (iter == null)
                {
                    _idArray[i] = obj;
                    if (!_isReady) CheckArray();
                    break;
                }
                i++;
            }
            return _positionOnTower[i].position;
        }
        else
        {
            for (int i = 6; i >= 0; i--)
            {
                Debug.Log(_idArray[i]);
                if (_idArray[i] != null)
                {
                    _idArray[i].GetComponent<Tor>().MoveToTable();
                    _idArray[i] = null;
                    break;
                }
            }

        }
        return default;
    }

    public void CheckArray()
    {
        bool isRight = true;
        for (int i = 0; i < 7; i++)
        {
            if (_idArray[i] != _idEtalon[i])
            {
                isRight = false;
                break;
            }
        }

        if (isRight)
        {
            _doorScript.Action();
            _roomManagerScript.ActiveStuff(1, true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScale>().NewScale();
            _isReady = true;
            _dialogScript.StartDialog(1);
        }
    }
        
}
