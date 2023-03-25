using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinema_cam;
    [SerializeField] private VisualValues _visual_values;

    #region Coroutines
    private IEnumerator _zoom_out_coroutine;
    #endregion

    #region Cache Variables
    private float zoomOutAmount;
    #endregion
    void Start()
    {
        if (_cinema_cam == null)
            _cinema_cam = GetComponent<CinemachineVirtualCamera>();

        if (_visual_values == null)
            _visual_values = GameManager.Instance.VisualValues;

    }

   
    public void ZoomOutCamera()
    {
        //_cinema_cam.m_Lens.OrthographicSize+= zoomOut;
        if(GameManager.Instance.GameMode == GameMode.SINGLE_PLAYER)
        {
            zoomOutAmount = _visual_values.CameraZoomOutAmount;
        }
        else
        {
            zoomOutAmount = _visual_values.CameraZoomOutAmountTwoPlayer;
        }

        _zoom_out_coroutine = zoomOut(_cinema_cam.m_Lens.OrthographicSize + zoomOutAmount);
        StartCoroutine(_zoom_out_coroutine);
    }

    private IEnumerator zoomOut(float targetZoom)
    {
        while(_cinema_cam.m_Lens.OrthographicSize < targetZoom)
        {
            _cinema_cam.m_Lens.OrthographicSize = Mathf.MoveTowards(_cinema_cam.m_Lens.OrthographicSize, targetZoom, _visual_values.CameraZoomSpeed* Time.deltaTime);
            yield return null;
        }
        StopCoroutine("zoomOut");
    }
}
