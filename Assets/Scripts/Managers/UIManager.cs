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

    public void Initialize()
    {
        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
    }
    private void setPlayerSO(string playerSOPath, string playerName)
    {
        ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(playerSOPath).PlayerName = playerName;
    }

    #region Event Broadcaster Notifications
    public void OnPlayPressed(EventParameters param)
    {

        Debug.Log("Entered name 1: " + param.GetParameter<string>(EventParamKeys.NAME_FIELD_ONE, "NONE"));

        setPlayerSO(FileNames.PLAYER_SO_1, param.GetParameter<string>(EventParamKeys.NAME_FIELD_ONE, "NONE"));

        if (GameManager.Instance.GameMode == GameMode.TWO_PLAYER)
        {
            Debug.Log("Entered name 2: " + param.GetParameter<string>(EventParamKeys.NAME_FIELD_TWO, "NONE"));

            setPlayerSO(FileNames.PLAYER_SO_2, param.GetParameter<string>(EventParamKeys.NAME_FIELD_TWO, "NONE"));

        }

    }

    #endregion

}
