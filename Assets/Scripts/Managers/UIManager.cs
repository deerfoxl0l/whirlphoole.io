using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        EventBroadcaster.Instance.AddObserver(EventKeys.PAUSE_GAME, onPauseGame);
        EventBroadcaster.Instance.AddObserver(EventKeys.RESUME_GAME, onResumeGame);
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

        if (GameManager.Instance.GameMode == GameMode.TWO_PLAYER) 
        { // these two in the if and else body aren't really related, the condition is just convenienit
            newPlayerUI.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920/2, 1080);
        }
        else
        {
            _player_ui_list[0].ActivateTopRight();
        }

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
    private void onPauseGame(EventParameters param)
    {
        foreach (PlayerUI playerUI in _player_ui_list)
        {
            playerUI.OnPause();
        }
        
    }
    public void onResumeGame(EventParameters param=null)
    {
        foreach (PlayerUI playerUI in _player_ui_list)
        {
            playerUI.OnResume();
        }
    }

    private void onGameOver(EventParameters param)
    {
        if(GameManager.Instance.GameMode == GameMode.TWO_PLAYER)
        {
            int playerWin = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null).PlayerHole.PlayerID;
            int playerLose = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM_2, null).PlayerHole.PlayerID;

            foreach (PlayerUI playerUI in _player_ui_list)
            {
                if (playerUI.PlayerID == playerWin)
                {
                    playerUI.OnPlayerWin();
                }
                else if (playerUI.PlayerID == playerLose)
                {
                    playerUI.OnPlayerLose();
                }
            }
        }
        else
        {
            _player_ui_list[0].OnPlayerLose();
        }
        
    }

    #endregion

}
