using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinema_cam;

    [SerializeField] private GameValues _game_values;

    private IEnumerator _zoom_out_coroutine;
    void Start()
    {
        if (_cinema_cam is null)
            _cinema_cam = GetComponent<CinemachineVirtualCamera>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;
    }

   
    public void ZoomOutCamera()
    {
        //_cinema_cam.m_Lens.OrthographicSize+= zoomOut;
        _zoom_out_coroutine = zoomOut(_cinema_cam.m_Lens.OrthographicSize + _game_values.CameraZoomAmount);
        StartCoroutine(_zoom_out_coroutine);
    }

    private IEnumerator zoomOut(float targetZoom)
    {
        while(_cinema_cam.m_Lens.OrthographicSize < targetZoom)
        {
            _cinema_cam.m_Lens.OrthographicSize = Mathf.MoveTowards(_cinema_cam.m_Lens.OrthographicSize, targetZoom, _game_values.CameraZoomSpeed* Time.deltaTime);
            yield return null;
        }
        StopCoroutine("zoomOut");
    }
}
