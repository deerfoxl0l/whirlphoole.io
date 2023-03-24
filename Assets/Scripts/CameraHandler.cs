using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : Singleton<CameraHandler>, ISingleton
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private CinemachineVirtualCamera _cam_1;
    public CameraScript Camera1
    {
        get { return _cam_1.GetComponent<CameraScript>(); }
    }
    [SerializeField] private CinemachineVirtualCamera _cam_2;
    public CameraScript Camera2
    {
        get { return _cam_2.GetComponent<CameraScript>(); }
    }
    public void Initialize()
    {
        if (_cam_1 == null)
            _cam_1 = GameObject.FindGameObjectWithTag(TagNames.CAMERA_1).GetComponent<CinemachineVirtualCamera>();
        /*
        if (_cam_2 == null)
            _cam_2 = GameObject.FindGameObjectWithTag(TagNames.CAMERA_2).GetComponent<CinemachineVirtualCamera>();
        */

    }

    public void SetCamFollowTarget(Transform targetTransform, int camNum)
    {
        switch (camNum)
        {
            case 1:
                _cam_1.Follow = targetTransform;
                break;
            case 2:
                _cam_2.Follow = targetTransform;
                break;
        }
    }
}
