using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsVFX : Poolable
{
    [SerializeField] private VisualValues _visual_values;
    [SerializeField] private TextMesh _text_mesh;

    //private IEnumerator _fade_out;

    public string PointsText
    {
        set { _text_mesh.text = value; }
    }

    private IEnumerator pointsAnimation()
    {
        while (_text_mesh.fontSize > 0)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 
                Mathf.MoveTowards(this.transform.localPosition.y, this.transform.localPosition.y+10, _visual_values.PointsScrollUpSpeed * Time.deltaTime), 1);

            _text_mesh.fontSize = (int)Math.Round(Mathf.MoveTowards(_text_mesh.fontSize, 0, _visual_values.PointsShrinkSpeed));
            yield return null;
        }
        StopCoroutine("pointsAnimation");
        VFXHandler.Instance.DeactivateObject(this.gameObject);
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        if (_visual_values == null)
            _visual_values = GameManager.Instance.VisualValues;

        if (_text_mesh == null)
            _text_mesh = GetComponent<TextMesh>();
        _text_mesh.fontSize = _visual_values.PointsFontSize;
    }

    public override void OnActivate()
    {
        StartCoroutine("pointsAnimation");
    }

    public override void OnDeactivate()
    {
        _text_mesh.text = "NO_VALUE";
        _text_mesh.fontSize = (int) Math.Round(_visual_values.PointsFontSize * _visual_values.PointsFontSizeScale);
    }
    #endregion
}
