using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Singleton<PlayerHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Cache Variables
    private Player playerRef;
    #endregion

    public void Initialize()
    {
        AddEventObservers();
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, OnHoleLevelUp);
    }

    private void OnHoleLevelUp(EventParameters param)
    {
        //Debug.Log("levelling up");
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);
        playerRef.ZoomOutPlayerCamera();
    }
}
