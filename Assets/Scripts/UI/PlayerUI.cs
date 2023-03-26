using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour, IEventObserver
{
    #region Panels/GameObjects
    [SerializeField] private GameObject _score_panel;
    [SerializeField] private GameObject _shadow_overlay_panel;
    [SerializeField] private GameObject _middle_panel;

    [SerializeField] private GameObject _pause_label;
    [SerializeField] private GameObject _win_label;
    [SerializeField] private GameObject _lose_label;
    #endregion

    #region TextFields
    [SerializeField] private TextMeshProUGUI _score_field;
    [SerializeField] private TextMeshProUGUI _next_lvl_field;
    #endregion

    #region Buttons
    [SerializeField] private Button _resume_btn;
    [SerializeField] private Button _quit_btn;
    #endregion

    #region CacheRefs
    private int _player_id;
    #endregion

    public int PlayerID
    {
        get { return _player_id; }
        set { _player_id = value; }
    }
    void Start()
    {
        _resume_btn = _resume_btn.GetComponent<Button>();
        _resume_btn.onClick.AddListener(OnResumeClicked);

        _quit_btn = _quit_btn.GetComponent<Button>();
        _quit_btn.onClick.AddListener(OnQuitClicked);


        _quit_btn.gameObject.SetActive(true);

        deactivateMiddlePanel();
        AddEventObservers();
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PAUSE_GAME, onPauseGame);
    }

    public void SetScores(int score, int nextLevel)
    {
        _score_field.text = ""+score;
        _next_lvl_field.text = "" + nextLevel;
    }

    private void deactivateMiddlePanel()
    {
        _middle_panel.SetActive(false);
        _shadow_overlay_panel.SetActive(false);
    }
    private void activateMiddlePanel()
    {
        _middle_panel.SetActive(true);
        _shadow_overlay_panel.SetActive(true);
    }

    private void activatePause()
    {
        _pause_label.gameObject.SetActive(true);
        _resume_btn.gameObject.SetActive(true);

        _win_label.SetActive(false);
        _lose_label.SetActive(false);
    }

    private void activateWin()
    {
        _win_label.SetActive(true);

        _lose_label.SetActive(false);
        _pause_label.gameObject.SetActive(false);
        _resume_btn.gameObject.SetActive(false);
    }

    private void activateLose()
    {
        _lose_label.SetActive(true);

        _win_label.SetActive(false);
        _pause_label.gameObject.SetActive(false);
        _resume_btn.gameObject.SetActive(false);
    }

    public void PlayerWin()
    {
        activateMiddlePanel();
        activateWin();
    }

    public void PlayerLose()
    {
        activateMiddlePanel();
        activateLose();
    }


    #region OnClick Functions
    private void OnResumeClicked()
    {
        deactivateMiddlePanel();
        EventBroadcaster.Instance.PostEvent(EventKeys.RESUME_GAME, null);
    }
    private void OnQuitClicked()
    {
        EventBroadcaster.Instance.RemoveObserver(EventKeys.PAUSE_GAME);
        deactivateMiddlePanel();
        EventBroadcaster.Instance.PostEvent(EventKeys.QUIT_GAME, null);
    }
    #endregion

    #region Event Broadcaster Notifs
    private void onPauseGame(EventParameters param)
    {
        activateMiddlePanel();
        activatePause();
    }

    #endregion

}
