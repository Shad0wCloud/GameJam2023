using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //    private Rigidbody _rigidbody;
    private CharacterController _characterController;
    private Animator _animator;

    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _jumpForse = 2f;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private Transform _groundChecer;
    [SerializeField] private float _groundChecerRange;
    [SerializeField] private float _jumpButtonGracePeriod;
    [SerializeField] private RoomManager _roomManagerScript;

    private float _ySpeed;
    [SerializeField] public bool _isGrpunded;
    private float _originalStepOffset;
    private float? _lastGroundTime;
    private float? _jumpButtonPressedTime;

    private void Awake()
    {
        //    _rigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _originalStepOffset = _characterController.stepOffset;
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Stage"))
        {
            _roomManagerScript.SpawnPlayer(PlayerPrefs.GetInt("Stage"), transform);
        }
        else
        {
            _roomManagerScript.SpawnPlayer(0, transform);
        }
    }

    public void SpawnPlayer()
    {
        _characterController.enabled = false;
        transform.position = _roomManagerScript.SpawnPosition();
        _characterController.enabled = true;
    }

    public void SpawnPlayer(Vector3 newPosition)
    {
        _characterController.enabled = false;
        transform.position = newPosition;
        _characterController.enabled = true;
    }

    private void FixedUpdate()
    {
        /*  Vector3 directionVector = new Vector3(v, 0, h);
        float h = Input.GetAxis("Horizontal");
        float v = - Input.GetAxis("Vertical");

          if (directionVector.magnitude > Mathf.Abs(0.1f))
          {
              transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), _rotationSpeed * Time.deltaTime);
          }

          Vector3 clampMagnitude = Vector3.ClampMagnitude(directionVector, 1f);

          if (Input.GetKey(KeyCode.LeftShift))
          {
              Vector3 moveDir = clampMagnitude * _speedRun;  
              _animator.SetFloat("speed", clampMagnitude.magnitude * 2, 0.05f, Time.deltaTime);
              _rigidbody.velocity =new Vector3(moveDir.x, _rigidbody.velocity.y, moveDir.z);
          }
          else
          {
              Vector3 moveDir = clampMagnitude * _speedWalk;  
              _animator.SetFloat("speed", clampMagnitude.magnitude, 0.05f, Time.deltaTime);
              _rigidbody.velocity =new Vector3(moveDir.x, _rigidbody.velocity.y, moveDir.z);
          }

          _rigidbody.angularVelocity = Vector3.zero;*/




    }

    private void Update()
    {

        /* if (Physics.CheckSphere(_groundChecer.position, 0.3f, _groundLayers))
         {
             _animator.SetBool("isInAir", false);
             _isInAir = false;
         }
         else
         {
             _animator.SetBool("isInAir", true);
             _isInAir = true;
         }*/

        _isGrpunded = _characterController.isGrounded;

        /*   if (Physics.Raycast(_groundChecer.position, Vector3.down, _groundChecerRange,_groundLayers))
           {
               _isGrpunded = true;
           }
           else
           {
               _isGrpunded = false;
           } */

        float h = Input.GetAxis("Horizontal");
        float v = -Input.GetAxis("Vertical");

        Vector3 movmentDirections = new Vector3(h, 0, -v);
        float inputMagnitude = Mathf.Clamp01(movmentDirections.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude *= 2;
        }
        _animator.SetFloat("speed", inputMagnitude, 0.05f, Time.deltaTime);

        float speed = inputMagnitude * _speedWalk;
        movmentDirections.Normalize();

        _ySpeed += Physics.gravity.y * Time.deltaTime;

        if (movmentDirections != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movmentDirections, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        if (_characterController.isGrounded)
        {
            _lastGroundTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpButtonPressedTime = Time.time;
        }

        if (Time.time - _lastGroundTime <= _jumpButtonGracePeriod)
        {
            _characterController.stepOffset = _originalStepOffset;
            _ySpeed = -0.5f;

            if (Time.time - _jumpButtonPressedTime <= _jumpButtonGracePeriod)
            {
                _ySpeed = _jumpForse;
                _jumpButtonPressedTime = null;
                _lastGroundTime = null;
                _animator.SetTrigger("Jump");
            }
        }
        else
        {
            _characterController.stepOffset = 0;
        }

        if (_isGrpunded)
        {
            _animator.SetBool("isInAir", false);
        }
        else
        {
            _animator.SetBool("isInAir", true);
        }

        Vector3 velocity = movmentDirections * speed;
        velocity.y = _ySpeed;

        _characterController.Move(velocity * Time.deltaTime); 
    }

}
