using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>, ISingleton, IEventObserver
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    private PlayerUI _player_ui_template;
    private GameObject _player_canvases;

    private List<PlayerUI> _player_ui_list;

    #region Cache Parameters
    private Player playerRef;
    #endregion

    public void Initialize()
    {
        _player_ui_list = new List<PlayerUI>();

        AddEventObservers();

        isDone = true;
    }
    public void SetPlayerUI(PlayerUI playerUi, GameObject playerCanvases)
    {
        _player_ui_template = playerUi;
        _player_canvases = playerCanvases;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_SCORE_UPDATE, OnScoreUpdate);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_OVER, onGameOver);
    }
    private void setPlayerSO(string playerSOPath, string playerName)
    {
        ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(playerSOPath).PlayerName = playerName;
    }

    public void SetPlayerUI(int playerID, Camera camera)
    {
        GameObject newPlayerUI = Instantiate(_player_ui_template.gameObject, _player_canvases.transform);
        newPlayerUI.GetComponent<Canvas>().worldCamera = camera;

        _player_ui_list.Add(newPlayerUI.GetComponent<PlayerUI>());
        _player_ui_list[_player_ui_list.Count - 1].PlayerID = playerID;

        newPlayerUI.SetActive(true);
    }

    #region Event Broadcaster Notifications
    public void OnPlayPressed(EventParameters param)
    {
        setPlayerSO(FileNames.PLAYER_SO_1, param.GetParameter<string>(EventParamKeys.NAME_FIELD_ONE, "NONE"));

        if (GameManager.Instance.GameMode == GameMode.TWO_PLAYER)
        {
            setPlayerSO(FileNames.PLAYER_SO_2, param.GetParameter<string>(EventParamKeys.NAME_FIELD_TWO, "NONE"));

        }
    }

    public void OnScoreUpdate(EventParameters param)
    {
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);

        foreach ( PlayerUI plyrUI in _player_ui_list)
        {
            if (plyrUI.PlayerID == playerRef.PlayerID)
            {
                plyrUI.SetScores(playerRef.PlayerScore, (playerRef.PlayerNextLvl- playerRef.PlayerScore));
                return;
            }
        }
    }

    private void onGameOver(EventParameters param)
    {
        int playerWin = param.GetParameter<int>(EventParamKeys.PLAYER_WIN_PARAM, 0);
        int playerLose = param.GetParameter<int>(EventParamKeys.PLAYER_LOSE_PARAM, 0);

        foreach (PlayerUI plyrUI in _player_ui_list)
        {
            if (plyrUI.PlayerID == playerWin)
            {
                plyrUI.PlayerWin();
            }
            if(plyrUI.PlayerID == playerLose)
            {
                plyrUI.PlayerLose();
            }
        }
    }

    #endregion

}
