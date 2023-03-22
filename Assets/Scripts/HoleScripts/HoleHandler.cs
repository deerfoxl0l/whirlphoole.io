using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HoleHandler : Singleton<HoleHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private GameValues _game_values ;

    #region Cache Variables
    private Prop propRef;
    private Hole holeRef;
    private Hole holeRef2;
    private bool passHole;
    #endregion

    public void Initialize()
    {
        if(_game_values is null)
            _game_values = GameManager.Instance.GameValues;
        AddEventObservers();
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, onHoleLevelUp);

        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_ENTER_PROP, onEnterOuterProp);
        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_EXIT_PROP, onExitOuterProp);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_ENTER_PROP, onEnterInnerProp);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_EXIT_PROP, onExitInnerProp);

        EventBroadcaster.Instance.AddObserver(EventKeys.PROP_ABSORBED, onPropAbsorbed);

        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_ENTER_HOLE, onEnterOuterHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.OUTER_EXIT_HOLE, onExitOuterHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_ENTER_HOLE, onEnterInnerHole);
        EventBroadcaster.Instance.AddObserver(EventKeys.INNER_EXIT_HOLE, onExitInnerHole);

        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_ABSORBED, onHoleAbsorbed);

    }

    #region Prop Events

    private void onEnterOuterProp(EventParameters param)
    {

    }
    private void onExitOuterProp(EventParameters param)
    {

    }
    private void onEnterInnerProp(EventParameters param)
    {
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);

        if (propRef.PropSize <= holeRef.HoleLevel)
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.PROP_ABSORBED, param);
        }
    }
    private void onExitInnerProp(EventParameters param)
    {

    }
    private void onPropAbsorbed(EventParameters param)
    {
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);

        holeRef.AddHoleExperience(propRef.PropPoints);
        PropHandler.Instance.removeProp(propRef);

    }
    #endregion

    #region Hole Events
    private void onEnterOuterHole(EventParameters param)
    {

    }
    private void onExitOuterHole(EventParameters param)
    {

    }
    private void onEnterInnerHole(EventParameters param)
    {
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);
        holeRef2 = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM_2, null);

        if (holeRef.HoleLevel == holeRef2.HoleLevel)
            return;

        if(holeRef.HoleLevel - holeRef2.HoleLevel  == _game_values.HoleAbsorbDifference)
        {
            passHole = true;
        }
        else if (holeRef2.HoleLevel - holeRef.HoleLevel >= _game_values.HoleAbsorbDifference)
        {
            param.AddParameter(EventParamKeys.HOLE_PARAM, holeRef2);
            param.AddParameter(EventParamKeys.HOLE_PARAM_2, holeRef);
            passHole = true;
        }

        if(passHole)
            EventBroadcaster.Instance.PostEvent(EventKeys.HOLE_ABSORBED, param);

        passHole = false;
    }
    private void onExitInnerHole(EventParameters param)
    {

    }
    private void onHoleAbsorbed(EventParameters param)
    {
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);
        holeRef2 = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM_2, null);

        holeRef.AddHoleExperience( (int)Math.Round(holeRef2.HoleExperience * _game_values.HoleCannibalExpMultiplier));
        holeRef2.gameObject.SetActive(false);
    }
    private void onHoleLevelUp(EventParameters param)
    {
        holeRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM, null);
        holeRef.IncreaseHoleSize();
    }
    #endregion
}
