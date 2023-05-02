using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bee : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _radiusView;
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistanse;

    [SerializeField] private Animator _animator;

    [SerializeField] private List<Transform> _targets;
    private int _currentTarget;


    private bool _isAttack;


    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _agent.speed = _speed;
        _agent.stoppingDistance = _stoppingDistanse;
        _currentTarget = -1;
    }

    private void Update()
    {
        if (Vector3.Distance(_agent.transform.position, _agent.pathEndPosition) < 0.1f || _currentTarget < 0)
        {

            NewTarget();
        }        

        _animator.SetFloat("Speed", _agent.speed, 0.05f, Time.deltaTime);
    }

    private void NewTarget()
    {
        _currentTarget = Random.Range(0, _targets.Count);
        _agent.speed = _speed;
        _agent.stoppingDistance = 0f;
        _agent.SetDestination(_targets[_currentTarget].position);
    }
}
