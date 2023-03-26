using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    #region Panels/Rects
    [SerializeField] private GameObject _player_ui;
    #endregion

    #region TextFields
    [SerializeField] private TextMeshProUGUI _score_field;
    [SerializeField] private TextMeshProUGUI _next_lvl_field;
    #endregion

    private int _player_id;

    public int PlayerID
    {
        get { return _player_id; }
        set { _player_id = value; }
    }

    public void SetScores(int score, int nextLevel)
    {
        _score_field.text = ""+score;
        _next_lvl_field.text = "" + nextLevel;
    }


}
