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

    [SerializeField] private Camera _cam_template;
    [SerializeField] private Transform _cam_spawn;

    [SerializeField] private List<Camera> _cam_list;
    [SerializeField] private List<CinemachineVirtualCamera> _virtual_cam_list;


    #region Cache Params
    private Player playerRef;
    private float _x, _w;
    #endregion

    public void Initialize()
    {
        if (_visual_values == null)
            _visual_values = GameManager.Instance.VisualValues;

        _cam_list = new List<Camera>();

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
            _cam_list.Add(Instantiate(_cam_template, _cam_spawn));

            _virtual_cam_list.Add(_cam_list[i].GetComponentInChildren<CinemachineVirtualCamera>());


            if (camAmount == 1)
            {
                _virtual_cam_list[i].m_Lens.OrthographicSize = _visual_values.CameraBaseSize;

                // values for viewportrect
                _x = 0;
                _w = 1;
            }
            else
            {
                _virtual_cam_list[i].m_Lens.OrthographicSize = _visual_values.CameraBaseSizeTwoPlayer;

                _w = .5f;
                _x = .5f - (.5f * i);
            }
            _cam_list[i].rect = new Rect(new Vector2(_x, 0), new Vector2(_w, 1));


            //Debug.Log("culling mask: " + getNewCullingMask(i));
            _cam_list[i].cullingMask = getNewCullingMask(i);

            _cam_list[i].gameObject.layer = Dictionary.CAM_LAYER_START + i;
            _virtual_cam_list[i].gameObject.layer = Dictionary.CAM_LAYER_START + i;


            _cam_list[i].gameObject.SetActive(false);
        }
    }

    private int getNewCullingMask(int i)
    {
        return (1 << 0 | 1 << 1 | 1 << 2 | 1 << 3 | 1 << 4 | 1 << 5 | 1 << (6+i));
    }
    public void SetCamFollowTarget(Transform targetTransform)
    {
        foreach(CinemachineVirtualCamera cam in _virtual_cam_list)
        {
            if(cam.Follow == null)
            {
                cam.Follow = targetTransform;
                cam.transform.parent.gameObject.SetActive(true);
                return;
            }
        }
    }
    public Camera GetCameraForInput()
    {
        return _cam_list[0];
    }

    public Camera GetCameraForUI(int playerID)
    {
        for(int i=0;i< _cam_list.Count; i++)
        {
            if(playerID-1 == i)
            {
                return _cam_list[i];
            }
        }
        return null;
    }

    #region Event Broadcaster Notifications
    private void OnHoleLevelUp(EventParameters param)
    {
        playerRef = param.GetParameter<Player>(EventParamKeys.PLAYER_PARAM, null);

        foreach (CinemachineVirtualCamera cam in _virtual_cam_list)
        {
            if (GameObject.ReferenceEquals(cam.Follow.gameObject, playerRef.transform.parent.gameObject))
            {
                cam.GetComponent<CameraScript>().ZoomOutCamera();
            }

        }

    }

    #endregion
}
