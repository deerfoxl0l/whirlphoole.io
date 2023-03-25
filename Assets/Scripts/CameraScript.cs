using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinema_cam;
    [SerializeField] private VisualValues _visual_values;

    private IEnumerator _zoom_out_coroutine;
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
        _zoom_out_coroutine = zoomOut(_cinema_cam.m_Lens.OrthographicSize + _visual_values.CameraZoomOutAmount);
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
