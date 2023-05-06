using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform[] _pointInventory;
    [SerializeField] private Transform[] _pointPutResurse;
    [SerializeField] private GameObject _honeycombsObj;
    [SerializeField] private GameObject _eggObj;
    [SerializeField] private GameObject _honeycombsMiniObj;
    [SerializeField] private GameObject _eggMiniObj;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PutResurse();
        }
    }

    public bool CheckFreeCell()
    {
        bool freeCell = false;

        for (int i = 0; i < _pointInventory.Length; i++)
        {

            if (_pointInventory[i].childCount == 0)
            {
                freeCell = true;
                break;
            }

        }

        return freeCell;
    }

    public void GetResurse(ResurseType resurseType)
    {
        for (int i = 0; i < _pointInventory.Length; i++)
        {
            if (_pointInventory[i].childCount == 0)
            {
                if (resurseType == ResurseType.Honeycombs)
                {
                    GameObject newObject = Instantiate(_honeycombsMiniObj) as GameObject;
                    newObject.transform.SetParent(_pointInventory[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = new Quaternion(45, 45, 45, 0);
                }
                else 
                {
                    GameObject newObject = Instantiate(_eggMiniObj) as GameObject;
                    newObject.transform.SetParent(_pointInventory[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = new Quaternion(45, 45, 45, 0);
                }
                break;
            }
        }
    }

    public void PutResurse()
    {
        for (int i = 0; i < _pointInventory.Length; i++)
        {
            if (_pointInventory[i].childCount != 0)
            {
                if (_pointInventory[i].GetChild(0).GetComponent<Resurse>().resurseType == ResurseType.Honeycombs)
                {
                    GameObject newObject = Instantiate(_honeycombsObj);
                    newObject.transform.SetParent(_pointPutResurse[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = Quaternion.identity;
                    newObject.transform.SetParent(null);
                }
                else
                {
                    GameObject newObject = Instantiate(_eggObj);
                    newObject.transform.SetParent(_pointPutResurse[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = Quaternion.identity;
                    newObject.transform.SetParent(null);
                }
                Destroy(_pointInventory[i].transform.GetChild((0)).gameObject);
                break;
            }
        }
    }
}
