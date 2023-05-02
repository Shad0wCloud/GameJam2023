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
    [SerializeField] private Transform[] _spawnPosition;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Stage"))
        {
            _currentStage = PlayerPrefs.GetInt("Stage");
        }
        else
        {
            _currentStage = 0;
            PlayerPrefs.SetInt("Stage", _currentStage);
        }


        for (int i = 0; i < _stateStaff.Length; i++)
        {
            if (i != _currentStage)
            {
                _stateStaff[i].SetActive(false);
            }
            else
            {
                _stateStaff[i].SetActive(true);
                GameObject.FindGameObjectWithTag("Player").transform.position = _spawnPosition[i].position;
                Debug.Log(_spawnPosition[i]);
            }
        }
    }

    public void NewStage()
    {
        _dayLightManager.NewStage();
        _barrierStage[_currentStage].SetActive(false);
        _currentStage += 1;
        PlayerPrefs.SetInt("Stage", _currentStage);
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
        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale != 0f) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("Stage");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
