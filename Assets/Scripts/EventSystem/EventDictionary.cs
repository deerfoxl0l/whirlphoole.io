public class EventDictionary
{
    

}

public class EventKeys{
    
    // SYSTEM EVENT KEYS
    public const string START_MENU = "PROGRAM_START";
    public const string START_GAME = "GAME_START";
    public const string PAUSE_GAME = "GAME_PAUSE";

    // GAME EVENT KEYS
    public const string OUTER_ENTER_PROP = "OUTER_ENTER_PROP";
    public const string OUTER_STAY_PROP = "OUTER_STAY_PROP";
    public const string OUTER_EXIT_PROP = "OUTER_EXIT_PROP";

    public const string INNER_ENTER_PROP = "INNER_ENTER_PROP";
    public const string INNER_STAY_PROP = "INNER_STAY_PROP";
    public const string INNER_EXIT_PROP = "INNER_EXIT_PROP";

    public const string PROP_ABSORBED = "PROP_ABSORBED";

    public const string OUTER_ENTER_HOLE= "OUTER_ENTER_HOLE";
    public const string OUTER_EXIT_HOLE = "OUTER_EXIT_HOLE";

    public const string INNER_ENTER_HOLE = "INNER_ENTER_HOLE";
    public const string INNER_EXIT_HOLE = "INNER_EXIT_HOLE";

    public const string HOLE_ABSORBED = "HOLE_ABSORBED";

    public const string HOLE_LEVEL_UP = "HOLE_LEVEL_UP";


    //UI EVENT KEYS
    public const string PLAY_PRESSED = "PLAY_BUTTON_PRESSED";
}

public class EventParamKeys
{
    public const string PLAYER_PARAM = "PLAYER";
    public const string HOLE_PARAM = "HOLE";
    public const string HOLE_PARAM_2 = "HOLE_2";

    public const string PROP_PARAM = "PROP";

    // UI EVENT PARAM KEYS
    public const string NAME_FIELD = "NAME_TEXT_FIELD";

}
