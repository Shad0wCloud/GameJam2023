using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private int _currentStage;
    [SerializeField] private DayLightManager _dayLightManager;
    [SerializeField] private GameObject[] _barrierStage;
    [SerializeField] private GameObject[] _stateStaff;
    [SerializeField] private GameObject[] _stateStaffEnableOff;
    [SerializeField] private Transform[] _spawnPosition;
    [SerializeField] private int[] _scaleStageOnStage;
    private GameObject _player;
    [SerializeField] private Dialog _dialogScript;
    

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (PlayerPrefs.HasKey("Stage"))
        {
            _currentStage = PlayerPrefs.GetInt("Stage");
        }
        else
        {
            _currentStage = 0;
            PlayerPrefs.SetInt("Stage", _currentStage);
        }


        SelectStage(_currentStage);
    }

    public void SelectStage(int stage)
    {
        for (int i = 0; i < _stateStaff.Length; i++)
        {
            if ((i == 0 && stage != 0) || (stage == 0 && i != 0))
            {
                _stateStaff[i].SetActive(false);
            }
            else
            {
                _stateStaff[i].SetActive(true);
            }
        }

        _dayLightManager.StageNumber(stage);
        _currentStage = stage;
        PlayerPrefs.SetInt("Stage", stage);

        for (int i = 0; i < _barrierStage.Length; i++)
        {
            if (i < stage)
            {
                _barrierStage[i].SetActive(false);
                if (_stateStaffEnableOff[i] != null) _stateStaffEnableOff[i].SetActive(false);
            }
            else break;
        }

        StartCoroutine(CooldownToDialog());
    }

    public void NewStage()
    {
        _dayLightManager.NewStage();
        _currentStage += 1;
        SelectStage(_currentStage);
        _player.GetComponent<PlayerScale>().NewScale();
        PlayerPrefs.SetInt("Stage", _currentStage);
    }

    private void SpawnPlayer(int positionNumber)
    {
        _player.GetComponent<PlayerController>().SpawnPlayer(_spawnPosition[positionNumber].position);
        Debug.Log(_spawnPosition[positionNumber]);
    }

    public Vector3 SpawnPosition()
    {
        int posNumber;
        if (PlayerPrefs.HasKey("Stage")) posNumber = PlayerPrefs.GetInt("Stage");
        else posNumber = 0;
        return _spawnPosition[posNumber].position;
    }

    public int StageScale()
    {
        int scaleNumber;
        if (PlayerPrefs.HasKey("Stage")) scaleNumber = PlayerPrefs.GetInt("Stage");
        else scaleNumber = 0;
        return _scaleStageOnStage[scaleNumber];
    }

    public void WriteStage(int stage)
    {
        _currentStage = stage;
        PlayerPrefs.SetInt("Stage", _currentStage);
        Debug.Log(_currentStage);
    }

    public void ActiveStuff(int state, bool isActive)
    {
        _stateStaff[state].SetActive(isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.P))
            if (Time.timeScale != 0f) Time.timeScale = 0f;
            else Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("Stage");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("Stage", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("Stage", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("Stage", 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator CooldownToDialog()
    {
        yield return new WaitForSeconds(3f);
        if (_currentStage == 0) _dialogScript.StartDialog(0);
    }
}
