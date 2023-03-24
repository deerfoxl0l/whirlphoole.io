using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScoller : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private float _scroll_speed_x = 0.2f;
    [SerializeField] private float _scroll_speed_y = 0.2f;

    void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position 
            + new Vector2(_scroll_speed_x * Time.deltaTime , _scroll_speed_y * Time.deltaTime), _image.uvRect.size);
    }
}
