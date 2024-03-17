using UnityEngine;

public class PanelPlayerInformer : MonoBehaviour, IPlayerInform
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    public void InformLoseEvent()
    {
        _losePanel.SetActive(true);
    }

    public void InformWinEvent()
    {
        _winPanel.SetActive(true);
    }
}
