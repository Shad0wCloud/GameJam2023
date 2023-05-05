using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitUnderDoor : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private bool _isActive;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector3 _attackBoxScale;
    [SerializeField] private LayerMask _layerMask;
    private bool[] _activeNumber = {false, false, false, false, false};
    private bool[] _etalonNumber = {true, false, true, true, false};

    private void Start()
    {
        _fire.SetActive(_isActive);
    }

    private void FixedUpdate()
    {
        if (_isActive) Damage();
    }

    private void Action(bool isActiveBool)
    {
        _isActive = isActiveBool;
        _fire.SetActive(_isActive);
    }

    public void ActionNumber(int id)
    {
        _activeNumber[id] = !_activeNumber[id];
        CheckNumber();
    }

    private void CheckNumber()
    {
        bool isRight = true;
        for (int i = 0; i < 5; i++)
        {
            if (_activeNumber[i] != _etalonNumber[i])
            {
                isRight = false;
                break;
            }
        }

        if (isRight) Action(false);
        else Action(true);
    }

    private void Damage()
    {
        Collider[] colliders = Physics.OverlapBox(_attackPoint.position, _attackBoxScale, Quaternion.identity, _layerMask);
        foreach (Collider iter in colliders)
        {
            if (iter.CompareTag("Player"))
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("Damage");
            }
        }
    }
}
