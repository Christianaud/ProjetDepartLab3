using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIEnd : UI
{
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private TextMeshProUGUI _txtTotalTime;
    [SerializeField] private TextMeshProUGUI _txtCollisions;
    [SerializeField] private TextMeshProUGUI _txtFinal;

    private void Awake()
    {
        // V?rifie s'il y a un UIGame si oui on le d?truit
        UIGame uiGame = FindAnyObjectByType<UIGame>();
        if (uiGame != null)
        {
            Destroy(uiGame.gameObject);
        }
    }

    private void Start()
    {
       // GameManager.Instance.SaveLevelData();
        EventSystem.current.SetSelectedGameObject(_buttonRestart.gameObject);
        _txtTotalTime.text = $"Temps total : {GameManager.Instance.GetTotalTime():F2} sec.";
        _txtCollisions.text = $"Collisions : {GameManager.Instance.GetTotalCollisions()}";
        float total = GameManager.Instance.GetTotalCollisions() + GameManager.Instance.GetTotalTime();
        _txtFinal.text = $"Temps final : {total:F2} sec.";
    }
}