using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : Singleton<CameraHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private VisualValues _visual_values;

    [SerializeField] private CinemachineVirtualCamera _virtual_cam_template;
    [SerializeField] private Transform _virtual_cam_spawn;

    [SerializeField] private List<CinemachineVirtualCamera> _cam_list;


    #region Cache Params
    private Player playerRef;
    #endregion

    public void Initialize()
    {
        if (_visual_values == null)
            _visual_values = GameManager.Instance.VisualValues;

        _cam_list = new List<CinemachineVirtualCamera>();

        switch (GameManager.Instance.GameMode){
            case GameMode.NONE_SELECTED:
                Debug.Log("Game Not Initialized");
                break;
            case GameMode.SINGLE_PLAYER:
                initializeCameras(1);
                break;
            case GameMode.TWO_PLAYER:
                initializeCameras(2);
                break;
        }

        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.HOLE_LEVEL_UP, OnHoleLevelUp);
    }

    private void initializeCameras(int camAmount)
    {
        for(int i=0; i< camAmount; i++)
        {
            _cam_list.Add(Instantiate(_virtual_cam_template, _virtual_cam_spawn));
            _cam_list[i].m_Lens.OrthographicSize = _visual_values.CameraBaseSize;
            _cam_list[i].gameObject.SetActive(true);
        }
    }

    public void SetCamFollowTarget(Transform targetTransform)
    {
        foreach(CinemachineVirtualCamera cam in _cam_list)
        {
            if(cam.Follow == null)
            {
                cam.Follow = targetTransform;
            }
        }
    }

    #region Event Broadcaster Notifications
    private void OnHoleLevelUp(EventParameters param)
    {
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);

        foreach (CinemachineVirtualCamera cam in _cam_list)
        {
            if (GameObject.ReferenceEquals(cam.Follow.gameObject, playerRef.transform.parent.gameObject))
            {
                cam.GetComponent<CameraScript>().ZoomOutCamera();
            }

        }

    }

    #endregion
}
