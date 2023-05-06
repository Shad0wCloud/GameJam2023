using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform[] _pointInventory;
    [SerializeField] private Transform[] _pointPutResurse;
    [SerializeField] private GameObject _honeycombsObj;
    [SerializeField] private GameObject _eggObj;
    [SerializeField] private GameObject _soupObj;
    [SerializeField] private GameObject _honeycombsMiniObj;
    [SerializeField] private GameObject _eggMiniObj;
    [SerializeField] private GameObject _soupMiniObj;
    public int currentHoney = 0;
    public int currentEggs = 0;


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
                    currentHoney++;
                }
                else if (resurseType == ResurseType.Egg)
                {
                    GameObject newObject = Instantiate(_eggMiniObj) as GameObject;
                    newObject.transform.SetParent(_pointInventory[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = new Quaternion(45, 45, 45, 0);
                    currentEggs++;
                }
                else
                {
                    Debug.Log("_____");
                    GameObject newObject = Instantiate(_soupMiniObj) as GameObject;
                    newObject.transform.SetParent(_pointInventory[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = Quaternion.identity;
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
                    currentHoney--;
                }
                else if(_pointInventory[i].GetChild(0).GetComponent<Resurse>().resurseType == ResurseType.Egg)
                {
                    GameObject newObject = Instantiate(_eggObj);
                    newObject.transform.SetParent(_pointPutResurse[i]);
                    newObject.transform.localPosition = Vector3.zero;
                    newObject.transform.localRotation = Quaternion.identity;
                    newObject.transform.SetParent(null);
                    currentEggs--;
                }
                else
                {
                    GameObject newObject = Instantiate(_soupObj);
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

    public void UseResurse()
    {
        for (int i = 0; i < _pointInventory.Length; i++)
        {
            if (_pointInventory[i].childCount != 0)
            {
                if (_pointInventory[i].GetChild(0).GetComponent<Resurse>().resurseType == ResurseType.Honeycombs)
                {
                    currentHoney--;
                }
                else if (_pointInventory[i].GetChild(0).GetComponent<Resurse>().resurseType == ResurseType.Egg)
                {
                    currentEggs--;
                }
                Destroy(_pointInventory[i].transform.GetChild((0)).gameObject);
            }
        }
    }

    public int UseSoup()
    {
        int soupCount = 0;
        for (int i = 0; i < _pointInventory.Length; i++)
        {
            if (_pointInventory[i].childCount != 0)
            {
                if (_pointInventory[i].GetChild(0).GetComponent<Resurse>().resurseType == ResurseType.Soup)
                {
                    soupCount++;
                    Destroy(_pointInventory[i].transform.GetChild((0)).gameObject);
                }
            }
        }
        return soupCount;
    }
}
