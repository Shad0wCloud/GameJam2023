using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResurseType
{
    Honeycombs,
    Egg,
    Soup
}

public class Resurse : MonoBehaviour
{
    public ResurseType resurseType;

    private Outline _outlineScript;
    private PlayerInventory _playerInventoryScript;
    [SerializeField] private Animator _canvasAnimator;

    private bool _isNearPlayer = false;

    private void Start()
    {
        _playerInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        if (GetComponent<Outline>()) _outlineScript = GetComponent<Outline>();
        StartCoroutine(OutlineOn());
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
        if (_isNearPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Action();
            }
        }
    }

    private void ButtonShow()
    {
        _canvasAnimator.SetBool("isNearPlayer", true);
    }

    private void ButtonHide()
    {
        _canvasAnimator.SetBool("isNearPlayer", false);

    }

    private void Action()
    {
        bool isCheck = _playerInventoryScript.CheckFreeCell();
        if (isCheck)
        {
            _playerInventoryScript.GetResurse(resurseType);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator OutlineOn()
    {
        float currTime = 0f;
        do
        {
            _outlineScript.OutlineWidth = Mathf.Lerp(1f, 6f, (currTime / 1));

            currTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        while (currTime <= 1);
        StartCoroutine(OutlineOff());
    }

    private IEnumerator OutlineOff()
    {
        float currTime = 0f;
        do
        {
            _outlineScript.OutlineWidth = Mathf.Lerp(6f, 1f, (currTime / 1));

            currTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        while (currTime <= 1);
        StartCoroutine(OutlineOn());
    }
}
