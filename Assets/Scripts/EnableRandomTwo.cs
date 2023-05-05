using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomTwo : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetObjects;

    private void Start()
    {
        int random = Random.Range(0, _targetObjects.Length);

        for (int i = 0; i < _targetObjects.Length; i++)
        {
            if (i == random) _targetObjects[i].SetActive(true);
            else _targetObjects[i].SetActive(false);
        }
    }
}
