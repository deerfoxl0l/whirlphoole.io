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
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, onHoleLevelUp);

        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_HOLE_ENTER, onEnterOuterHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_HOLE_EXIT, onExitOuterHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_HOLE_ENTER, onEnterInnerHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_HOLE_EXIT, onExitInnerHole);

        EventBroadcaster.Instance.AddObserver(EventKeys.PROP_ABSORBED, onPropAbsorbed);

    }
    private void onHoleLevelUp(EventParameters param)
    {
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);
        holeRef.IncreaseHoleSize();
    }
    private void onPropAbsorbed(EventParameters param)
    {
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);

        holeRef.AddHoleExperience(propRef.PropPoints);
        PropHandler.Instance.removeProp(propRef);
        
    }
    private void onEnterOuterHole(EventParameters param)
    {

    }

    private void onExitOuterHole(EventParameters param)
    {

    }
    private void onEnterInnerHole(EventParameters param)
    {
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);

        if (propRef.PropSize <= holeRef.HoleLevel)
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.PROP_ABSORBED, param);
        }
    }

    private void onExitInnerHole(EventParameters param)
    {

    }
}
