using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Start : MonoBehaviour
{
    [SerializeField] private GameObject[] _mobObject;

    [SerializeField] private Transform[] _mobSpawnPoint;

    [SerializeField] private Door[] _doors;


    public void Start3Stage()
    {
        for (int i = 0; i < _mobObject.Length; i++)
        {
            _mobObject[i].transform.position = _mobSpawnPoint[i].position;
            Debug.Log(_mobObject[i].transform.position + " " + _mobSpawnPoint[i].position);
            if (_mobObject[i].GetComponent<Bee>()) _mobObject[i].GetComponent<Bee>().isInRoom = true;
            else if (_mobObject[i].GetComponent<Chiken>()) _mobObject[i].GetComponent<Chiken>().isInRoom = true;
        }

        foreach (Door iter in _doors)
        {
            iter.Action();
        }
    }
}
