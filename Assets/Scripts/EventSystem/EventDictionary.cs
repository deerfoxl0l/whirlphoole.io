public class EventDictionary
{
    

}

public class EventKeys{
    
    // SYSTEM EVENT KEYS
    public const string START_MENU = "PROGRAM_START";
    public const string START_GAME = "GAME_START";
    public const string PAUSE_GAME = "GAME_PAUSE";

    // GAME EVENT KEYS
    public const string OUTER_HOLE_ENTER = "OUTER_HOLE_ENTER";
    public const string OUTER_HOLE_EXIT = "OUTER_HOLE_EXIT";

    public const string INNER_HOLE_ENTER = "INNER_HOLE_ENTER";
    public const string INNER_HOLE_EXIT = "INNER_HOLE_EXIT";

    public const string HOLE_LEVEL_UP = "HOLE_LEVEL_UP";

    public const string PROP_ABSORBED = "PROP_ABSORBED";

    //UI EVENT KEYS
    public const string PLAY_PRESSED = "PLAY_BUTTON_PRESSED";
}

public class EventParamKeys
{
    public const string PLAYER_PARAM = "PLAYER";
    public const string HOLE_PARAM = "HOLE";
    public const string PROP_PARAM = "PROP";

    // UI EVENT PARAM KEYS
    public const string NAME_FIELD = "NAME_TEXT_FIELD";

}
