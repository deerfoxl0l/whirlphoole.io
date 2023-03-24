using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    #region PlayerSO Values
    [SerializeField] private int _player_id;
    public int PlayerID
    {
        get { return _player_id; }
    }

    [SerializeField] private string _player_name;
    public string PlayerName
    {
        get { return _player_name; }
        set { _player_name = value; }
    }

    [SerializeField] private int _player_score;
    public int PlayerScore
    {
        get { return _player_score; }
       // set { _player_score += value; } // possible readability issue when using this
    }
    #endregion

    #region PlayerSO Methods
    public PlayerScriptableObject(int playerID, string playerName)
    {
        _player_id = playerID;
        _player_name = playerName;
    }
    public void IncrementScore(int increment)
    {
        _player_score += increment;
    }
    #endregion


}
