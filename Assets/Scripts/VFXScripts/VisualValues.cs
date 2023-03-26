using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualValues", menuName = "ScriptableObjects/VisualValues")]
public class VisualValues : ScriptableObject
{
    #region VFXSO Values

    [Header("Player Name Tag Values")]

    [Tooltip("The base font size of the name tag ")]
    [SerializeField] [Range(1, 200)] public int NameTagBaseFontSize;

    [Tooltip("The decrease in scale of name tags per level up ")]
    [SerializeField] [Range(0.01f, 1f)] public float NameTagBalancing;

    [Tooltip("The smallest scale the name tags can get ")]
    [SerializeField] [Range(0.01f, 1f)] public float NameTagFloor;

    [Header("Floating Absorbed Points Values")]

    [Tooltip("The base font size of the absorbed points")]
    [SerializeField] [Range(1, 200)] public int PointsFontSize;

    [Tooltip("The scaling size by hole level of the absorbed points")]
    [SerializeField] [Range(0.1f, 5f)] public float PointsFontSizeScale;

    [Tooltip("How fast the points scroll up out of view ")]
    [SerializeField] [Range(1f, 20f)] public float PointsScrollUpSpeed;

    [Tooltip("How fast the points shrink out of view ")]
    [SerializeField] [Range(0.1f, 2f)] public float PointsShrinkSpeed;


    [Header("Camera Values")]

    [Tooltip("The base size of cameras")]
    [SerializeField] [Range(1f, 20f)] public float CameraBaseSize;

    [Tooltip("The base size of cameras in two player mode")]
    [SerializeField] [Range(1f, 20f)] public float CameraBaseSizeTwoPlayer;

    [Tooltip("The base zoom amount per level gained ")]
    [SerializeField] [Range(0f, 5f)] public float CameraZoomOutAmount;

    [Tooltip("The base zoom amount per level gained on two player mode")]
    [SerializeField] [Range(0f, 5f)] public float CameraZoomOutAmountTwoPlayer;

    [Tooltip("How fast cameras zoom out")]
    [SerializeField] [Range(0f, 10f)] public float CameraZoomSpeed;
    #endregion
}
