using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [SerializeField]
    private TextMeshProUGUI _uiTextLevel;
    [SerializeField]
    private TextMeshProUGUI _uiTextShots;
    [SerializeField]
    private TextMeshProUGUI _uiBtn;
    [SerializeField]
    private Vector3 _castlePos;
    [SerializeField]
    private GameObject[] _castles;
    [SerializeField]
    private GameObject _showBoth;

    private int _level;
    private int _maxLevel;
    private int _shots;
    private GameObject _castle;
    private GameMode _gameMode = GameMode.idle;
    private string showing = "Show Slingshot";

    private void Start()
    {
        _instance = this;
        _level = 0;
        _maxLevel = _castles.Length;
        StartLevel();
    }

    private void Update()
    {
        UpdateGUI();
        if (_gameMode == GameMode.playing && Goal.goalMet)
        {
            _gameMode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke(nameof(NextLevel), 2f);
        }
    }

    private void NextLevel()
    {
        _level++;
        if (_level == _maxLevel)
        {
            _level = 0;
        }
        StartLevel();
    }

    private void StartLevel()
    {
        if (_castle != null)
        {
            Destroy(_castle);
        }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject p in projectiles)
        {
            Destroy(p);
        }
        _castle = Instantiate(_castles[_level]);
        _castle.transform.position = _castlePos;
        _shots = 0;

        SwitchView("Show Both");
        ProjectileLine.Instance.Clear();
        Goal.goalMet = false;
        UpdateGUI();
        _gameMode = GameMode.playing;
    }

    public void SwitchView(string v)
    {
        if (v == "")
            v = _uiBtn.text;
        showing = v;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                _uiBtn.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = _castle;
                _uiBtn.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = _showBoth;
                _uiBtn.text = "Show Slingshot";
                break;

        }
    }

    private void UpdateGUI()
    {
        _uiTextLevel.text = "Level: " + (_level + 1).ToString();
        _uiTextShots.text = "Shots: " + _shots;
    }

    public void ShotFired() => _shots++;
}
