using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleHandler : Singleton<HoleHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion


    #region Cache Variables
    private Prop propRef;
    private Hole holeRef;
    #endregion

    public void Initialize()
    {
        AddEventObservers();
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_HOLE_ENTER, OnEnterOuterHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_HOLE_EXIT, OnExitOuterHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_HOLE_ENTER, OnEnterInnerHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_HOLE_EXIT, OnExitInnerHole);
    }

    private void OnEnterOuterHole(EventParameters param)
    {

    }

    private void OnExitOuterHole(EventParameters param)
    {

    }
    private void OnEnterInnerHole(EventParameters param)
    {
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);

        if (propRef.PropSize <= holeRef.HoleLevel)
        {
            holeRef.AddHoleExperience(propRef.PropPoints);
            PropHandler.Instance.removeProp(propRef);
        }
    }

    private void OnExitInnerHole(EventParameters param)
    {

    }
}
