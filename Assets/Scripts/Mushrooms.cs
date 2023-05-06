using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mushrooms : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _radiusView;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _curentSpeed;
    [SerializeField] private float _stoppingDistanse;

    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _canvasAnimator;

    [SerializeField] private List<Transform> _targets;
    private int _currentTarget;

    private PlayerInventory playerInventory;

    [SerializeField] private int _stepOnSlow;
    [SerializeField] private int _curentStep;

    [SerializeField] private UnderRoomDoor underDoor;


    private bool _isAttack;

    private bool _isNearPlayer;
    private bool isFull = false;


    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        _curentSpeed = _maxSpeed;
        _agent.speed = _curentSpeed;
        _agent.stoppingDistance = _stoppingDistanse;
        _currentTarget = -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonShow();
            _isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonHide();
            _isNearPlayer = false;
        }
    }

    private void Update()
    {
        if (_isNearPlayer && !isFull)
        {
            if (Input.GetKeyDown(KeyCode.E)) Action();
        }

        if (Vector3.Distance(_agent.transform.position, _agent.pathEndPosition) < 0.1f || _currentTarget < 0)
        {
            NewTarget();
        }

        _animator.SetFloat("speed", _agent.speed, 0.05f, Time.deltaTime);
    }

    private void NewTarget()
    {
        _currentTarget = Random.Range(0, _targets.Count);
        _agent.speed = _curentSpeed;
        _agent.stoppingDistance = 0f;
        _agent.SetDestination(_targets[_currentTarget].position);
    }

    private void ButtonShow()
    {
        _canvasAnimator.SetBool("isNearPlayer", true);
    }

    private void ButtonHide()
    {
        _canvasAnimator.SetBool("isNearPlayer", false);

    }

    public void Action()
    {
        _curentStep += playerInventory.UseSoup();
        _curentSpeed = _maxSpeed - (0.5f * _curentStep);

        if (_curentStep >= _stepOnSlow)
        {
            _curentSpeed = 0f;
            underDoor.Action();
            isFull = true;
        }
    }
}
