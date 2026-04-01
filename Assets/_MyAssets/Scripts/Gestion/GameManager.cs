using UnityEngine;

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

    private void Start()
    {
        _nbCollisions = 0;
        _startTime = Time.time;
        _isPaused = false;
        Player.OnPlayerPaused += Player_OnPlayerPaused;
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
        _nbCollisions += e.CollisionValue;
    }
}


