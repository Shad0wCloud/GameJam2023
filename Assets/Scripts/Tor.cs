using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Material _material;
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private Collider _collider;

    [SerializeField] private Transform _positionOnTable;
    [SerializeField] private HanoiskayTower _hanoiskayTowerScript;
    [SerializeField] private int _id;

    private bool _isNearPlayer;

    private bool _isOnTable = false;
    private bool _isOnTower = false;

    void Start()
    {
       // _animator = GetComponent<Animator>();
    //   _material = GetComponent<Material>();
        _material.SetFloat("Dissolve", -1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isOnTable && !_isOnTower)
        {
            ButtonShow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !_isOnTable && !_isOnTower)
        {
            ButtonHide();
        }
    }

    private void Update()
    {
        if (_isNearPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TorAction();
            }
        }
    }

    private void OnMouseDown()
    {
        if (_isOnTable)
        {
            MoveOnTower();
        }
    }

    private void ButtonShow()
    {
        _animator.SetBool("isNearPlayer", true);
        _isNearPlayer = true;
    }

    private void ButtonHide()
    {
        _animator.SetBool("isNearPlayer", false);
        _isNearPlayer = false;
    }

    public void TorAction()
    {
        ButtonHide();
        _triggerCollider.enabled = false;
        StartCoroutine(Dissolve(1f));
    }    

    public void MoveToTable()
    {
        transform.position = _positionOnTable.position;
        _isOnTower = false;
    }

    private void MoveOnTower()
    {
        if (!_isOnTower)
        {
            Vector3 newPosition = _hanoiskayTowerScript.TorPosotion(true, gameObject);
            transform.position = newPosition;
            _isOnTower = true;
        }
        else
        {
            _hanoiskayTowerScript.TorPosotion(false, gameObject);
        }
     /*   else
        {
            _hanoiskayTowerScript.TorPosotion(false, gameObject);
            transform.position = _positionOnTable.position;
            _isOnTower = false;
        }*/
    }

    private IEnumerator Dissolve(float time)
    {
        float currTime = 0f;
        do
        {
            _material.SetFloat("Dissolve", Mathf.Lerp(-0.1f, 0.1f, (currTime / time)));

            currTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        while (currTime <= time);

        MoveToTable();
        _isOnTable = true;
        StartCoroutine(Gather(1f));
    }

    private IEnumerator Gather(float time)
    {
        float currTime = 0f;
        do
        {
            _material.SetFloat("Dissolve", Mathf.Lerp(0.1f, -0.1f, (currTime / time)));

            currTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        while (currTime <= time);
    }
}
