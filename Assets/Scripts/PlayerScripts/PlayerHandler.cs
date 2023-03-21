using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IEventObserver
{
    #region Player References
    private Player _player_reference;
    public Player PlayerReference
    {
        get { return _player_reference; }
    }
    public Vector3 PlayerLocation
    {
        get { return _player_reference.transform.position; }
    }

    #endregion

    #region Event Variables
    //Projectile projReference = null;
    #endregion

    void Start()
    {
        if (_player_reference == null)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        _player_reference = GameObject.FindGameObjectWithTag(TagNames.PLAYER).GetComponent<Player>();

        AddEventObservers();
    }


    public void AddEventObservers()
    {
        //EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);
    }
}
