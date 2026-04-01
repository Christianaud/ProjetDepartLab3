using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIStart : UI
{
    [SerializeField] private GameObject _instructionsPanel;
    [SerializeField] private GameObject _gameButtons;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonClose;

    private void Awake()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }

        UIGame uiGame = FindAnyObjectByType<UIGame>();
        if (uiGame != null)
        {
            Destroy(uiGame.gameObject);
        }
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_buttonStart.gameObject);
    }

    public void OnInstructionsClick()
    {
        _instructionsPanel.SetActive(true);
        _gameButtons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_buttonClose.gameObject);
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCloseClick()
    {
        _instructionsPanel.SetActive(false);
        _gameButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_buttonStart.gameObject);
    }
}

