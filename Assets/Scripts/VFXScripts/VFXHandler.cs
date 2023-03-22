using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Singleton<VFXHandler>, ISingleton, IPoolHandler, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private VFXObjectPool _sfx_op;

    #region Cache Params
    private PointsVFX pointsRef;
    private Prop absorbedPropRef;
    private Hole absorbedHoleRef;
    #endregion
    public void Initialize()
    {
        if (_sfx_op is null)
            _sfx_op = GetComponent<VFXObjectPool>();
        AddEventObservers();
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PROP_ABSORBED, onPropAbsorbed);
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_ABSORBED, onHoleAbsorbed);
    }
    private void onPropAbsorbed(EventParameters param)
    {
        //Debug.Log("Hole absorbed for VFX ");
        absorbedPropRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        pointsRef = _sfx_op.getPointsVFX().GetComponent<PointsVFX>();

        pointsRef.PointsText = "+ " + absorbedPropRef.PropPoints;
        pointsRef.gameObject.transform.localPosition = absorbedPropRef.transform.localPosition;

    }

    private void onHoleAbsorbed(EventParameters param)
    {
        // GETTING HOLE_PARAM_2, THE ONE THAT WAS ABSORBED, 
        absorbedHoleRef = param.GetParameter<Hole>(EventParamKeys.HOLE_PARAM_2, null);

        pointsRef = _sfx_op.getPointsVFX().GetComponent<PointsVFX>();

        pointsRef.PointsText = "+ " + absorbedHoleRef.HoleExperience;

        pointsRef.gameObject.transform.localPosition = absorbedHoleRef.transform.localPosition;
    }

    public void DeactivateObject(GameObject obj)
    {
        _sfx_op.GameObjectPool.Release(obj);
    }

}
