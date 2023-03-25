using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    PROGRAM_START,
    MAIN_MENU,
    INGAME,
    PAUSED
}

public enum GameMode
{
    NONE_SELECTED,
    SINGLE_PLAYER,
    TWO_PLAYER
}
public class GameManager : Singleton<GameManager>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region StateHandler Variables
    private StateHandler<GameState> _game_state_handler;
    public StateHandler<GameState> GameStateHandler
    {
        get { return _game_state_handler; }
    }
    public GameState GameState
    {
        get { return _game_state_handler.CurrentState; }
    }


    private StateHandler<GameMode> _game_mode_handler;
    public StateHandler<GameMode> GameModeHandler
    {
        get { return _game_mode_handler; }
    }
    public GameMode GameMode
    {
        get { return _game_mode_handler.CurrentState; }
    }
    #endregion

    #region Program Values
    private GameValues _game_values;
    public GameValues GameValues
    {
        get { return _game_values; }
        set { _game_values = value; }
    }
    private VisualValues _visual_values;
    public VisualValues VisualValues
    {
        get { return _visual_values; }
        set { _visual_values = value; }
    }
    #endregion

    private int _current_biggest_hole_size;
    public int CurrentBiggestHole
    {
        get { return _current_biggest_hole_size; }
        set { _current_biggest_hole_size = value; }
    }


    public void Initialize()
    {
        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);

        _game_mode_handler = new StateHandler<GameMode>();
        _game_mode_handler.Initialize(GameMode.NONE_SELECTED);

        _current_biggest_hole_size = 1;
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.START_MENU, OnStartMenu);
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
        EventBroadcaster.Instance.AddObserver(EventKeys.START_GAME, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.PAUSE_GAME, OnGamePause);
    }

    public void SetPlayerSO(string player, string playerName)
    {
        PlayerScriptableObject playerSO;

        if (player == PlayerDictionary.PLAYER_ONE)
        {
            playerSO = ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO_1);
        }
        else { 
            playerSO = ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO_2);
        }
        playerSO.PlayerName = playerName;
    }

    #region Event Broadcaster Notifications
    public void OnStartMenu(EventParameters param=null)
    {
        _game_state_handler.Initialize(GameState.MAIN_MENU);
    }
    public void OnPlayPressed(EventParameters param)
    {
        _game_state_handler.SwitchState(GameState.INGAME);
        //_game_mode_handler.SwitchState(param.GetParameter<GameMode>(EventParamKeys.GAME_MODE_PARAM, GameMode.NONE_SELECTED));
        _game_mode_handler.SwitchState(GameMode.SINGLE_PLAYER);

        SceneManager.LoadScene(SceneNames.GAME_SCENE);
    }


    public void OnGameStart(EventParameters param=null)
    {
    }
    public void OnGamePause(EventParameters param = null)
    {
        _game_state_handler.SwitchState(GameState.PAUSED);

    }
    
    #endregion
}

