using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    [SerializeField] private GameObject _honeycombs;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private int _coolldownLittle;
    [SerializeField] private int _coolldownBig;
    private bool _isCooldawn;
    private SpawnHive _spawnHiveScript;

    private void Start()
    {
        _spawnHiveScript = GameObject.Find("PointSpawnHive").GetComponent<SpawnHive>();
        _hp = _maxHp;
    }

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

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _spawnHiveScript.DestroyHive();
            Destroy(this.gameObject);
        }
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
