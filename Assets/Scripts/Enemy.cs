using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _radiusView;
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistanse;

    [SerializeField] private Animator _animator;

    [SerializeField] private List<Transform> _targets;
    private int _currentTarget;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerMask;

    private bool _isAttack;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _agent.speed = _speed;
        _agent.stoppingDistance = _stoppingDistanse;
    }

    private void Update()
    {
        if (!_isAttack)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < _stoppingDistanse + 0.1f) 
            {
                Attack();
            }
            else if (Vector3.Distance(_player.transform.position, transform.position) < _radiusView)
            {
                LayerMask mask = LayerMask.GetMask("Player", "Ground", "Obstacle");

                Vector3 dir = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.25f, _player.transform.position.z) - _attackPoint.position;
                Debug.DrawRay(_attackPoint.position, dir, Color.red);

                RaycastHit hit;
                if (Physics.Raycast(_attackPoint.position, dir, out hit,_radiusView, mask))
                {
                    if (hit.transform.tag == "Player")
                    {
                        _agent.speed = _speed * 2f;
                        _agent.stoppingDistance = _stoppingDistanse;
                        _agent.SetDestination(_player.transform.position);
                        _currentTarget = -1;
                    }
                    else
                    {
                        if (_agent.transform.position == _agent.pathEndPosition || _currentTarget < 0)
                        {
                            NewTarget();
                        }
                    }
                }
            }
            else
            {
                if (_agent.transform.position == _agent.pathEndPosition || _currentTarget < 0)
                {
                    NewTarget();
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

    private void Attack()
    {
        _isAttack = true;
        _currentTarget = -1;
        _animator.SetTrigger("Attack");
    }

    public void Damage()
    {
        Collider[] colliders = Physics.OverlapSphere(_attackPoint.position, _attackRange, _layerMask);
        foreach (Collider iter in colliders)
        {
            if (iter.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("Damage");
            }
        }
        _isAttack = false;
    }
}
