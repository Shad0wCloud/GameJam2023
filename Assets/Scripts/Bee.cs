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

    private Transform _playerTransform;


    private bool _isAttack;

    public bool isPlayerGo;
    public bool isInRoom;


    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _agent.speed = _speed;
        _agent.stoppingDistance = _stoppingDistanse;
        _currentTarget = -1;
    }

    private void Update()
    {
        if (isInRoom)
        {
            if (Vector3.Distance(_agent.transform.position, _agent.pathEndPosition) < 0.1f || _currentTarget < 0)
            {
                NewTarget();
            }  
        }
        else
        {
            if (isPlayerGo)
            {
                PlayerDestination();
            }
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

    private void PlayerDestination()
    {
        _agent.speed = _speed;
        _agent.stoppingDistance = 1f;
        _agent.SetDestination(_playerTransform.position);
    }

    public void Action()
    {
        if (isPlayerGo && !isInRoom) isInRoom = true;
        else if (!isPlayerGo) isPlayerGo = true;
    }
}
