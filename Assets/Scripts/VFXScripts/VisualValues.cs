using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualValues", menuName = "ScriptableObjects/VisualValues")]
public class VisualValues : ScriptableObject
{
    #region VFXSO Values

    [SerializeField] [Range(1, 200)] public int NameTagBaseFontSize;
    [SerializeField] [Range(0.01f, 1f)] public float NameTagBalancing;


    [SerializeField] [Range(1f, 20f)] public float CameraBaseSize;
    [SerializeField] [Range(0f, 5f)] public float CameraZoomOutAmount;
    [SerializeField] [Range(0f, 10f)] public float CameraZoomSpeed;
    #endregion
}
