using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    PROGRAM_START,
    MAIN_MENU,
    INGAME,
    GAME_OVER,
    PAUSED,
    EXITING
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

        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.START_MENU, OnStartMenu);
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
        EventBroadcaster.Instance.AddObserver(EventKeys.START_GAME, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.PAUSE_GAME, OnGamePause);
        EventBroadcaster.Instance.AddObserver(EventKeys.RESUME_GAME, OnGameResume);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_OVER, onGameOver);
        EventBroadcaster.Instance.AddObserver(EventKeys.QUIT_GAME, OnGameQuit);
        EventBroadcaster.Instance.AddObserver(EventKeys.EXIT_PROGRAM, OnProgramExit);
    }

    #region Event Broadcaster Notifications
    public void OnStartMenu(EventParameters param=null)
    {
        _game_state_handler.SwitchState(GameState.MAIN_MENU);
        _game_mode_handler.SwitchState(GameMode.NONE_SELECTED);
    }
    public void OnPlayPressed(EventParameters param)
    {
        _game_state_handler.SwitchState(GameState.INGAME);

        SceneManager.LoadScene(SceneNames.GAME_SCENE);
    }


    public void OnGameStart(EventParameters param=null)
    {
        _current_biggest_hole_size = 1;
    }
    public void OnGamePause(EventParameters param = null)
    {
        _game_state_handler.SwitchState(GameState.PAUSED);
        Time.timeScale = 0;
        InputHandler.Instance.toggleInputAllow(false);
    }
    public void OnGameResume(EventParameters param = null)
    {
        _game_state_handler.SwitchState(GameState.INGAME);
        Time.timeScale = 1;
        InputHandler.Instance.toggleInputAllow(true);
    }
    public void onGameOver(EventParameters param = null)
    {
        _game_state_handler.SwitchState(GameState.GAME_OVER);

        InputHandler.Instance.toggleInputAllow(false);
    }
    public void OnGameQuit(EventParameters param = null)
    {
        StopAllCoroutines();
        _game_state_handler.SwitchState(GameState.MAIN_MENU);
        InputHandler.Instance.toggleInputAllow(false);
        removeEventObservers();
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
        //Application.Quit();
    }
    public void OnProgramExit(EventParameters param = null)
    {
        _game_state_handler.SwitchState(GameState.EXITING);
    }

    #endregion


    private void removeEventObservers()
    {

        EventBroadcaster.Instance.RemoveObserver(EventKeys.PLAYER_SO_UPDATE);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.PLAYER_SCORE_UPDATE);

        EventBroadcaster.Instance.RemoveObserver(EventKeys.OUTER_ENTER_PROP);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.OUTER_STAY_PROP);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.OUTER_EXIT_PROP);

        EventBroadcaster.Instance.RemoveObserver(EventKeys.INNER_ENTER_PROP);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.INNER_STAY_PROP);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.INNER_EXIT_PROP);

        EventBroadcaster.Instance.RemoveObserver(EventKeys.PROP_ABSORBED);

        EventBroadcaster.Instance.RemoveObserver(EventKeys.OUTER_ENTER_HOLE);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.OUTER_EXIT_HOLE);

        EventBroadcaster.Instance.RemoveObserver(EventKeys.INNER_ENTER_HOLE);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.INNER_STAY_HOLE);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.INNER_EXIT_HOLE);

        EventBroadcaster.Instance.RemoveObserver(EventKeys.HOLE_ABSORBED);
        EventBroadcaster.Instance.RemoveObserver(EventKeys.HOLE_LEVEL_UP);

    }
}

