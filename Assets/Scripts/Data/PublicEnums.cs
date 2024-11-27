namespace Data
{
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