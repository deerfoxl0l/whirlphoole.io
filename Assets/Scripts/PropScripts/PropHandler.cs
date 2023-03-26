using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHandler : Singleton<PropHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = true;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private PropStaff _prop_staff;
    public PropStaff PropStaff
    {
        get { return _prop_staff; }
        set { _prop_staff = value; }
    }
    /*
    [SerializeField] private PropLifetime _prop_lifetime;
    public PropLifetime PropLifetime
    {
        get { return _prop_lifetime; }
    }

    [SerializeField] private PropHelper _prop_helper;
    public PropHelper PropHelper
    {
        get { return _prop_helper; }
    }
    */

    private List<PropMovable> _prop_movable_list;

    public void Initialize()
    {
        _prop_movable_list = new List<PropMovable>();

        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PROP_ABSORBED, onPropAbsorbed);
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return;
        /*
        foreach (PropMovable propM in _prop_movable_list.ToArray())
        {
            propM.MoveProp(new Vector2(), 3f );
        }*/
    }

    public void addPropMovable(PropMovable propM)
    {
        _prop_movable_list.Add(propM);
    }

    private void removeProp(Prop prop)
    {
        _prop_staff.PropLifetime.DeactivateObject(prop.transform.gameObject);
    }


    #region Events
    private void onPropAbsorbed(EventParameters param)
    {
        removeProp(param.GetParameter<Prop>(EventParamKeys.PROP_PARAM, null));
    }

    #endregion
}
