using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGame : UI
{
    public static UIGame Instance;

    [SerializeField] private TextMeshProUGUI _txtTime;
    [SerializeField] private TextMeshProUGUI _txtCollisions;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _continueButton;

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
            return;
            Debug.LogError("Un gameObject essaie de cr?e un deuxi?me UIGame");
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;  // Assurer que le jeu d?bute pas en pause
        _pausePanel.SetActive(false);

        Player.OnPlayerPaused += Player_OnPlayerPaused;
        CollisionManager.OnCollisionOccured += CollisionManager_OnCollisionOccured;
        CollisionDisplayUI();
    }

    private void Update()
    {
        TimeDisplayUI();
    }

    private void OnDestroy()
    {
        CollisionManager.OnCollisionOccured -= CollisionManager_OnCollisionOccured;
        Player.OnPlayerPaused -= Player_OnPlayerPaused;
    }

    private void Player_OnPlayerPaused(object sender, System.EventArgs e)
    {
        //Toggle(basculer) du panneau de pause
        _pausePanel.SetActive(!_pausePanel.activeSelf);

        if (_pausePanel.activeSelf)
        {
            Time.timeScale = 0f; 
            EventSystem.current.SetSelectedGameObject(_continueButton.gameObject);
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    private void TimeDisplayUI()
    {
        if (GameManager.Instance.TimerStarted)
        {
            float elapsedTime = Time.time - GameManager.Instance.StartTime;
            _txtTime.text = $"Temps : {elapsedTime:F2}";
        }
        else
        {
            _txtTime.text = "Temps : 0.00";
        }
    }

    private void CollisionDisplayUI()
    {
        _txtCollisions.text = $"Collisions : {GameManager.Instance.NbCollision}";
    }

    private void CollisionManager_OnCollisionOccured(object sender, CollisionManager.OnCollisionOccuredEventArgs e)
    {
        CollisionDisplayUI();
    }

    public void OnContinueClick()
    {
        // Reprendre le jeu
        Player.TriggerOnPlayerPaused(this);
    }

    private void ResetUI()
    {
        _txtTime.text = "Temps : 0.00";
        _txtCollisions.text = "Collisions : 0";
    }

    public void OnRestartLevelClick()
    {
        
        
        GameManager.Instance.ResetCurrentLevel();
        _pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        ResetUI();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}


