using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    [SerializeField] private GameObject _honeycombs;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private int _coolldownLittle;
    [SerializeField] private int _coolldownBig;
    private bool _isCooldawn;

    private void Update()
    {
        if (_pointSpawn.childCount == 0 && !_isCooldawn)
        {
            StartCoroutine(Cooldawn());
        }
    }

    private void Spawn()
    {
        GameObject newObj = Instantiate(_honeycombs);
        newObj.transform.SetParent(_pointSpawn);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;

    }

    private IEnumerator Cooldawn()
    {
        int coldown = Random.Range(_coolldownLittle, _coolldownBig);
        _isCooldawn = true;
        yield return new WaitForSeconds(coldown);
        Spawn();
        _isCooldawn = false;
    }
}
