namespace Data
{
    public enum AudioEffect
    {
        Knocking = 1,
        PowerOff = 2,
        TrapdoorStuck = 3,
        EnemyFall = 4,
        DoorCreak = 5,
        ButtonClick = 6,
        Accept = 7,
        Error = 8
    }

    public enum CreatureAction
    {
        Hello = 0,
        Talk = 1,
        Exit = 2,
        Die = 3
    }
    
    public enum CreatureAlignment
    {
        Good = 1,
        Neutral = 0,
        Evil = -1,
    }
    
    public enum CreatureComponentType
    {
        Eye,
        Mouth, 
        Nose, 
        Head, 
        Body,
        Gear
    }

    public enum AcceptMode
    {
        Rejected = -1,
        Allowed = 1
    }
}