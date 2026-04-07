using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* Classe qui d?finit un singleton */

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CollisionManager.OnCollisionOccured += CollisionManager_OnCollisionOccured;
    }

    private void OnDestroy()
    {
        CollisionManager.OnCollisionOccured -= CollisionManager_OnCollisionOccured;
    }

    private int _nbCollisions;
    public int NbCollision => _nbCollisions; 

    private float _startTime;
    public float StartTime => _startTime;

    private float _endTime;
    public float EndTime { get => _endTime; set => _endTime = value; }

    private bool _isPaused = false;

    private bool _timerStarted = false;
    public bool TimerStarted => _timerStarted;

    private float _timeDebutLevel;

    // --- Listes pour stocker les temps et collisions par niveau ---
    private List<float> _tempsParNiveau = new List<float>();
    private List<int> _collisionsParNiveau = new List<int>();
    private void Start()
    {
        _nbCollisions = 0;
        //_startTime = Time.time;
        _isPaused = false;
        Player.OnPlayerPaused += Player_OnPlayerPaused;
    }

    public void StartTimer()
    {
        if (!_timerStarted)
        {
            _startTime = Time.time;
            _timeDebutLevel = Time.time;
            _timerStarted = true;
            //Debug.Log("Timer démarré à : " + _startTime);
        }
    }

    private void Player_OnPlayerPaused(object sender, System.EventArgs e)
    {
        if (_isPaused)
        {
            //Repars le jeu
            Time.timeScale = 1.0f;
            _isPaused = false;

        }
        else
        {
            //Arrete le jeu
            Time.timeScale = 0f;
            _isPaused = true;
        }
    }

    private void CollisionManager_OnCollisionOccured(object sender, CollisionManager.OnCollisionOccuredEventArgs e)
    {
        //Debug.Log("GameManager reçoit collision");
        _nbCollisions += e.CollisionValue;
    }

    // Dans GameManager.cs
    public void ResetCurrentLevel()
    {
        _timeDebutLevel = Time.time;
        _nbCollisions = 0;
        _startTime = Time.time;
        _timerStarted = false;
    }

    public void SaveLevelData()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        while (_tempsParNiveau.Count <= index)
            _tempsParNiveau.Add(0f);
        while (_collisionsParNiveau.Count <= index)
            _collisionsParNiveau.Add(0);
        if (_tempsParNiveau[index] == 0f)
        {
            _tempsParNiveau[index] = TimerStarted ? Time.time - _timeDebutLevel : 0f;
            _collisionsParNiveau[index] = _nbCollisions;
        }
    }

    // --- Récupérer temps et collisions d’un niveau ---
    public float GetLevelTime(int niveau)
    {
        if (niveau < _tempsParNiveau.Count)
            return _tempsParNiveau[niveau];
        return 0f;
    }

    public int GetLevelCollisions(int niveau)
    {
        if (niveau < _collisionsParNiveau.Count)
            return _collisionsParNiveau[niveau];
        return 0;
    }

    // --- Cumul total sur tous les niveaux ---
    public float GetTotalTime()
    {
        float total = 0f;
        foreach (float t in _tempsParNiveau)
            total += t;
        if (_timerStarted)
            total += Time.time - _startTime;

        return total;
    }

    public int GetTotalCollisions()
    {
        int total = 0;
        foreach (int c in _collisionsParNiveau)
            total += c;

        total += _nbCollisions;
        return total;
    }
}


