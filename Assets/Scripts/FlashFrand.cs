using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFrand : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distantion;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _target.position) > _distantion)
        {
            Vector3 moveDir = new Vector3(_target.position.x - transform.position.x, _target.position.y - transform.position.y,_target.position.z - transform.position.z);
            transform.Translate(moveDir * _speed * Time.deltaTime);
        }
    }
}
