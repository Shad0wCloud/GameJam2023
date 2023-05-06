using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chiken : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _radiusView;
    [SerializeField] private float _radiusViewHive;
    private Transform _targetAttack;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistanse;

    [SerializeField] private Animator _animator;

    [SerializeField] private List<Transform> _targets;
    [SerializeField] private List<Transform> _targetsOnPlate;
    [SerializeField] private Transform[] _pointForRunning;
    private int _currentTarget;

    private Transform _playerTransform;
    [SerializeField] private GameObject _egg;


    private bool _isAttack;

    public bool isPlayerGo;
    public bool isInRoom;
    public int id;

    private NavMeshPath _path;


    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _path = new NavMeshPath();

        _agent.speed = _speed;
        _agent.stoppingDistance = _stoppingDistanse;
        _currentTarget = -1;
    }

    private void Update()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < _radiusView)
        {
            isPlayerGo = true;
            Vector3 dir = Vector3.zero;
            float range = 0f;
            foreach (Transform iter in _pointForRunning)
            {
                if (Vector3.Distance(_playerTransform.position, iter.position) > range)
                {
                    _agent.CalculatePath(iter.position, _path);
                    if (_path.status == NavMeshPathStatus.PathComplete)
                    {
                        range = (Vector3.Distance(_playerTransform.position, iter.position));
                        dir = iter.position;
                    }
                }
            }

            _currentTarget = -1;
            RunningFromPlayer(dir);
        }
        else
        {
            isPlayerGo = false;
            if (isInRoom)
            {
                if (_targetAttack)
                {
                    if (Vector3.Distance(transform.position, _targetAttack.position) < _attackRange) _animator.SetBool("Attack", true);
                    else _animator.SetBool("Attack", false);
                }
                else
                {
                    _animator.SetBool("Attack", false);

                    if (GameObject.FindGameObjectWithTag("Hive") != null)
                    {
                        GameObject[] hives = GameObject.FindGameObjectsWithTag("Hive");
                        Transform target = null;
                        float range = 10000f;
                        foreach (GameObject iter in hives)
                        {
                            float dist = Vector3.Distance(transform.position, iter.transform.position);
                            if (dist < _radiusViewHive && dist < range) target = iter.transform;
                        }

                        if (target != null) NewTargetHive(target);
                        else if (Vector3.Distance(_agent.transform.position, _agent.pathEndPosition) < 0.1f || _currentTarget < 0) NewTarget();

                    }
                    else if (Vector3.Distance(_agent.transform.position, _agent.pathEndPosition) < 0.1f || _currentTarget < 0)
                    {
                        NewTarget();
                        _targetAttack = null;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(_agent.transform.position, _agent.pathEndPosition) < 0.1f || _currentTarget < 0)
                {
                    NewTargetOnPlate();
                    _targetAttack = null;

                }
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
    
    private void NewTargetHive(Transform target)
    {
        _agent.speed = _speed * 2;
        _agent.stoppingDistance = _attackRange - 0.1f;
        _agent.SetDestination(target.position);
        _targetAttack = target;
    }

    private void NewTargetOnPlate()
    {
        _currentTarget = Random.Range(0, _targetsOnPlate.Count);
        _agent.speed = _speed;
        _agent.stoppingDistance = 0f;
        _agent.SetDestination(_targetsOnPlate[_currentTarget].position);
    }

    private void RunningFromPlayer(Vector3 dir)
    {
        _agent.speed = _speed * 2;
        _agent.stoppingDistance = 0f;
        _agent.SetDestination(dir);
        _targetAttack = null;
    }

    private void PlayerDestination()
    {
        _agent.speed = _speed;
        _agent.stoppingDistance = 1f;
        _agent.SetDestination(_playerTransform.position);
    }

    public void Action()
    {
        if (!isInRoom) isInRoom = true;
    }

    public void Attack()
    {
        if (_targetAttack) if (_targetAttack.GetComponent<Hive>()) _targetAttack.GetComponent<Hive>().TakeDamage(_damage);
    }

    public void DestroyHive()
    {
        _targetAttack = null;
    }

    public void GiveEgg()
    {
        GameObject newObj = Instantiate(_egg);
        newObj.transform.SetParent(transform);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;
        newObj.transform.SetParent(null);

    }
}
