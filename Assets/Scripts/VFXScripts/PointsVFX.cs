using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsVFX : Poolable
{
    [SerializeField] private TextMesh _text_mesh;

    [SerializeField] private float _scroll_up_speed; // TEMPORARY
    [SerializeField] private float _shrink_speed;
    [SerializeField] private int _font_size=100;

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
                Mathf.MoveTowards(this.transform.localPosition.y, this.transform.localPosition.y+10, _scroll_up_speed*Time.deltaTime), 1);

            _text_mesh.fontSize = (int)Math.Round(Mathf.MoveTowards(_text_mesh.fontSize, 0, _shrink_speed));
            yield return null;
        }
        StopCoroutine("pointsAnimation");
        VFXHandler.Instance.DeactivateObject(this.gameObject);
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        if(_text_mesh is null)
            _text_mesh = GetComponent<TextMesh>();
        _text_mesh.fontSize = _font_size;
    }

    public override void OnActivate()
    {
        StartCoroutine("pointsAnimation");
    }

    public override void OnDeactivate()
    {
        _text_mesh.text = "NO_VALUE";
        _text_mesh.fontSize = _font_size;
    }
    #endregion
}
