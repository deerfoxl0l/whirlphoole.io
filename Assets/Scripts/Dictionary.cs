using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dictionary
{
}
public static class FileNames
{
    public const string PLAYER_SO = "ScriptableObjects/Players/Player";
    public const string PLAYER_SO_1 = "ScriptableObjects/Players/Player1";
    public const string PLAYER_SO_2 = "ScriptableObjects/Players/Player2";

    public const string GAME_VALUES = "ScriptableObjects/GameValues/GameValues1";
    public const string VISUAL_VALUES = "ScriptableObjects/VisualValues/VisualValues1";
}

public static class TagNames
{
    public const string MAIN_CAMERA = "MainCamera";
    public const string CAMERA_1 = "VirtualCam";

    public const string PLAYER = "Player";

    public const string HOLE = "Hole";
    public const string PROP = "Prop";
    public const string PROP_STAFF = "PropStaff";
    public const string PROP_BOUNDS = "PropSpawnBounds";

}
public static class PlayerDictionary
{
    public const string PLAYER_ONE = "PLAYER_ONE";
    public const string PLAYER_TWO= "PLAYER_TWO";
}

public static class HoleDictionary
{
    public const string OUTER_COLLIDER = "OUTER_COLLIDER";
    public const string INNER_COLLIDER = "INNER_COLLIDER";

}

public static class PropDictionary
{
    public const string PROP_SPAWN_RANDOM = "PROP_SPAWN_RANDOM";
    public const string PROP_SPAWN_PREDETERMINED = "PROP_SPAWN_PREDETERMINED";

    public const string PROP_SQUARE = "PropSquare";
    public const string PROP_CIRCLE = "PropCirlcle";
    public const string PROP_CAPSULE = "PropCapsule";
    public const string PROP_HEXAGON = "PropHexagon";
}