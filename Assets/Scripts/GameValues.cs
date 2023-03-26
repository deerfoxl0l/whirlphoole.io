using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValue", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    #region Game Values

    #region Player Values
    [Header("Player Values")]
    [Tooltip("The offset value for the player to stop within of the mouse location")]
    [SerializeField] [Range(0.1f, 2.0f)] public float PlayerCursorOffset;

    [Tooltip("The player'ss base move speed")]
    [SerializeField] [Range(0.1f, 20f)] public float PlayerBaseSpeed;

    // USAGE: movespeed = PlayerBaseSpeed - (_player_level * PlayerSpeedDecreaseMultiplier)
    [Tooltip("The decrease multiplier in speed for each level the player grows. ")]
    [SerializeField] [Range(0.1f, 1.0f)] public float PlayerSpeedDecreaseMultiplier;

    [Tooltip("The floor at which the player's speed can decrease towards ")]
    [SerializeField] [Range(0.1f, 1.0f)] public float PlayerSpeedDecreaseFloor;

    #endregion

    #region Hole Values
    [Header("Hole Values")]
    // USAGE: replaces hole transform.localScale;
    [Tooltip("The base size of holes ")]
    [SerializeField] [Range(0.1f, 1.5f)] public float HoleBaseSize;

    [Tooltip("The speed at which a hole grows in scale after levelling up")]
    [SerializeField] [Range(0.1f, 10f)] public float HoleGrowSpeed;

    // USAGE: size += _hole_level* HoleBaseSize*HoleSizeMultiplier
    [Tooltip("Used to scale how much a hole grows in size ")]
    [SerializeField][Range(0.1f, 1.5f)] public float HoleSizeMultiplier;

    [Tooltip("How strong holes pull props in ")]
    [SerializeField] [Range(.1f, 3f)] public float HolePullStrengthProp;

    [Tooltip("How strong holes pull prop anchors(parent gameobjects) in ")]
    [SerializeField] [Range(.1f, 3f)] public float HolePullStrengthPropAnchor;

    [Tooltip("How strong holes 'whirl' props across their radius ")]
    [SerializeField] [Range(10f, 300f)] public float HoleWhirlStrength;

    [Tooltip("How strong holes absorb and sink props in ")]
    [SerializeField] [Range(1f, 10f)] public float HoleAbsorbStrengthProp;

    [Tooltip("The base threshold of how long until holes level up ")]
    [SerializeField] [Range(1, 100)] public int HoleExpBaseThreshold;

    //USAGE: HoleExpThreshold  + (HoleExpThreshold *(_hole_level -1)* HoleExpThresholdMultiplier)
    [Tooltip("The multiplier of which a holes threshold is scaled up by level ")]
    [SerializeField][Range(0.5f, 3.0f)] public float HoleExpMultiplier;

    [Tooltip("Rival Holes' movement speed ")]
    [SerializeField] [Range(0.1f, 20f)] public float HoleRivalSpeed;

    [Tooltip("The multiplier of absorbed Rival holes (first layer of  HoleExpCannibalMultiplier)")]
    [SerializeField] [Range(1, 10)] public int HoleRivalExpMultiplier;

    [Tooltip("How Strong holes absorb other holes ")]
    [SerializeField] [Range(.01f, 1f)] public float HoleAbsorbStrengthHole;

    [Tooltip("The required difference between holes for them to absorb each other ")]
    [SerializeField] [Range(1, 5)] public int HoleAbsorbDifference;

    [Tooltip("The floor scale of which a hole will finall despawn ")]
    [SerializeField] [Range(0.01f, 1.0f)] public float HoleScaleDespawn;

    // the multiplier to be multiplied to the exp points from a hole absorbed by a bigger hole
    [Tooltip("Multiplier of absorbing other holes (second layer of HoleRivalExpMultiplier ")]
    [SerializeField] [Range(0.1f, 1.5f)] public float HoleExpCannibalMultiplier;

    #endregion

    #region Prop Values
    [Header("Prop Values")]
    [Tooltip("Max amount of props that can be spawned")]
    [SerializeField] [Range(1f, 300f)] public float PropSpawnMax;

    [Tooltip("The rate of which props spawn")]
    [SerializeField] [Range(0.1f, 2f)] public float PropSpawnRate;

    [Tooltip("The smallest size props can be spawned")]
    [SerializeField] [Range(0, 100)] public int PropSpawnSizeFloor;

    [Tooltip("The biggest size props can be spawned. Leave at -1 to use the current game's biggest present hole+1")]
    [SerializeField] [Range(-1, 100)] public int PropSpawnSizeCeiling;

    [Tooltip("The base amount of points an absorbed prop gives")]
    [SerializeField] [Range(1, 50)] public int PropsPointsBase;

    [Tooltip("The multiplier of points props give")]
    [SerializeField] [Range(1, 20)] public int PropsPointsMultiplier;

    // USAGE: replaces prop transform.localScale;
    [Tooltip("The base scale size of props ")]
    [SerializeField] [Range(0.1f, 2f)] public float PropsScaleBase;

    [Tooltip("The point at which absorbed props will despawn")]
    [SerializeField] [Range(0.01f, 1.0f)] public float PropScaleDespawn;

    // USAGE: _prop_size += _prop_size* PropsScaleBase* PropsScaleMultiplier
    [Tooltip("The multiplier that props scale with given size")]
    [SerializeField] [Range(0.1f, 1.5f)] public float PropsScaleMultiplier;

    [Tooltip("The aim of the prop anchor/parent when sucked/pulled in by a hole (PropAnchorAim*holecenter.position)")]
    [SerializeField] [Range(0.5f, 2.0f)] public float PropAnchorAim;
    #endregion


    #endregion
}
