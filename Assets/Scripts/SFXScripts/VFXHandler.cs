using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Singleton<VFXHandler>, ISingleton, IEventObserver
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
    private Prop propRef;
    private PointsVFX pointsRef;
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
    }
    private void onPropAbsorbed(EventParameters param)
    {
        //Debug.Log("Hole absorbed for VFX ");
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        pointsRef = _sfx_op.getPointsVFX().GetComponent<PointsVFX>();

        pointsRef.PointsText = "+ " + propRef.PropPoints;
        pointsRef.gameObject.transform.localPosition = propRef.transform.localPosition;

    }
}
