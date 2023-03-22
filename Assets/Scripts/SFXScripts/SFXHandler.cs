using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : Singleton<SFXHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private SFXObjectPool _sfx_op;

    #region Cache Params
    private Prop propRef;
    private PointsSFX pointsRef;
    #endregion
    public void Initialize()
    {
        if (_sfx_op is null)
            _sfx_op = GetComponent<SFXObjectPool>();
        AddEventObservers();
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PROP_ABSORBED, onPropAbsorbed);
    }
    private void onPropAbsorbed(EventParameters param)
    {
        Debug.Log("Hole absorbed for SFX ");
        propRef = param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null);
        pointsRef = _sfx_op.getPointsSFX().GetComponent<PointsSFX>();

        pointsRef.PointsText = "+ " + propRef.PropPoints;
        pointsRef.gameObject.transform.localPosition = propRef.transform.localPosition;

    }
}
