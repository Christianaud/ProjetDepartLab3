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
            Debug.LogError("Un gameObject essaie de cr?e un deuxi?me UIGame");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Time.timeScale = 1.0f;  // Assurer que le jeu d?bute pas en pause
        //_pausePanel.SetActive(false);

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
        EventSystem.current.SetSelectedGameObject(_continueButton.gameObject);
    }

    private void TimeDisplayUI()
    {
        float elapsedTime = Time.time - GameManager.Instance.StartTime;
        _txtTime.text = $"Temps : {elapsedTime:F2}";
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
}


