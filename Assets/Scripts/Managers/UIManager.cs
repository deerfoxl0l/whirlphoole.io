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

    [SerializeField] private float PanelMoveSpeed = 45;

    #region Coroutines Variables
    private IEnumerator _coroutine_1;
    private IEnumerator _coroutine_2;
    #endregion

    private MenuHandler _menu_handler;
    public MenuHandler MenuHandler
    {
        set { _menu_handler = value; }
    }

    public void Initialize()
    {
        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
    }

    public void StartGame()
    {
        //_menu_handler.ToggleVisibility();
        EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
    }

    #region Event Broadcaster Notifications
    public void OnPlayPressed(EventParameters param)
    {
        StartGame();
    }

    #endregion

}
