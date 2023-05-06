using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private string[] _replics0;
    [SerializeField] private string[] _replics1;
    [SerializeField] private string[] _replics2;
    [SerializeField] private string[] _replics3;
    [SerializeField] private string[] _replics4;
    [SerializeField] private string[] _replics5;
    [SerializeField] private string[] _replics6;
    [SerializeField] private string[] _replics7;
    [SerializeField] private string[] _replics8;
    [SerializeField] private string[] _replics9;
    [SerializeField] private string[] _replics10;
    [SerializeField] private string[] _replics11;
    [SerializeField] private string[] _replics12;
    [SerializeField] private string[] _replics13;
    [SerializeField] private string[] _replics14;
    [SerializeField] private string[] _replics15;
    [SerializeField] private string[] _replics16;
    [SerializeField] private string[] _replics17;
    [SerializeField] private string[] _replics18;
    [SerializeField] private string[] _replics19;
    [SerializeField] private string[] _replics20;
    [SerializeField] private string[] _replics21;

    [SerializeField] private Animator _animator;

    [SerializeField] private Text _dialogText;
    [SerializeField] private float _slowText;

    [SerializeField] private AdvantGhost _advantGhostScript;
    private bool _isGhost = false;

    private Coroutine _coroutine;
    private string[] _currentText;
    private bool _isActive = false;

    private int index;
    private Book _temporaryBook;

    private PlayerController _playerControllerScript;

    private void Start()
    {
        _dialogText.text = string.Empty;
        _playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_isActive)
        {
            if (Input.anyKeyDown)
            {
                SkipText();
            }
        }
    }

    public void StartDialog(int numberReplic)
    {
        _animator.SetTrigger("Start");
        _playerControllerScript.isCanMove = false;

        switch (numberReplic)
        {
            case 0:
                Replic(_replics0);
                break;
            
            case 1:
                Replic(_replics1);
                break;
                        
            case 2:
                Replic(_replics2);
                break;
                        
            case 3:
                _isGhost = true;
                Replic(_replics3);
                break;
                        
            case 4:
                Replic(_replics4);
                break;
                        
            case 5:
                Replic(_replics5);
                break;
                                        
            case 6:
                Replic(_replics6);
                break;
                                        
            case 7:
                Replic(_replics7);
                break;
                                        
            case 8:
                Replic(_replics8);
                break;
                                        
            case 9:
                Replic(_replics9);
                break;
                                        
            case 10:
                Replic(_replics10);
                break;
                                        
            case 11:
                Replic(_replics11);
                break;
                                        
            case 12:
                Replic(_replics12);
                break;
                                                        
            case 13:
                Replic(_replics13);
                break;
                                                        
            case 14:
                Replic(_replics14);
                break;
                                                        
            case 15:
                Replic(_replics15);
                break;
                                                        
            case 16:
                Replic(_replics16);
                break;
                                                        
            case 17:
                Replic(_replics17);
                break;
                                                        
            case 18:
                Replic(_replics18);
                break;
                                                        
            case 19:
                Replic(_replics19);
                break;
                                                                        
            case 20:
                Replic(_replics12);
                break;
                                                                        
            case 21:
                Replic(_replics12);
                break;

        }
    }

    private void Replic(string[] lines)
    {
        index = 0;
        _dialogText.text = string.Empty;
        _isActive = true;
        _currentText = lines;
        _coroutine = StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char ch in _currentText[index].ToCharArray())
        {
            _dialogText.text += ch;
            yield return new WaitForSeconds(_slowText);
        }
    }

    public void SkipText()
    {
        if (_dialogText.text == _currentText[index])
        {
            NextLines();
            Debug.Log("Next Lines");
        }
        else
        {
            StopCoroutine(_coroutine);
            _dialogText.text = _currentText[index];
        }
    }

    private void NextLines()
    {
        if (index < _currentText.Length - 1) 
        {
            index++;
            _dialogText.text = string.Empty; 
            _coroutine = StartCoroutine(TypeLine());
        }
        else
        {
            Debug.Log("Vse");
            _animator.SetTrigger("End");
            _isActive = false;
            _playerControllerScript.isCanMove = true;

            if (_isGhost) StartCoroutine(Timer());
            if (_temporaryBook != null) _temporaryBook.isRead = false;
            _temporaryBook = null;  
        }
    }

    public void ReadBoock(Book book)
    {
        _temporaryBook = book;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.5f);
        if (_isGhost) _advantGhostScript.EnabledComponents();
    }

}
