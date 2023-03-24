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

    [SerializeField] private Transform _player_spawn_transform;
    [SerializeField] private Hole _player_hole_template;


    #region Cache Variables
    private Player playerRef;
    private Hole holeRef;
    #endregion

    public void Initialize()
    {
        AddEventObservers();
    }

    public void Start()
    {
        spawnPlayer(ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO_1));

        //spawnPlayer(ScriptableObjectsHelper.GetScriptableObject<PlayerScriptableObject>(FileNames.PLAYER_SO_2));
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, OnHoleLevelUp);
    }

    private void OnHoleLevelUp(EventParameters param)
    {
        //Debug.Log("levelling up");
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);

        if(playerRef.PlayerHoleLevel>GameManager.Instance.CurrentBiggestHole)
        {
            GameManager.Instance.CurrentBiggestHole = playerRef.PlayerHoleLevel;
        }
    }

    private void spawnPlayer(PlayerScriptableObject playerSO)
    {
        holeRef = GameObject.Instantiate(_player_hole_template, _player_spawn_transform);
        holeRef.gameObject.SetActive(true);
        holeRef.PlayerHole.InitializePlayer(playerSO);
        holeRef.PlayerHole.PlayerCamera = CameraHandler.Instance.Camera1;
        CameraHandler.Instance.SetCamFollowTarget(holeRef.transform, 1);
    }
}
