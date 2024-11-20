using System.Linq;
using Data;

namespace Extern
{
    public static class EntryChecker
    {
        public static CreatureAlignment IsGood(this CreatureData creature) =>
            creature.GetAllParts().Aggregate(0, (c, part) => c + part.goodPart - part.badPart) switch
            {
                > 0 => CreatureAlignment.Good,
                < 0 => CreatureAlignment.Evil,
                _ => CreatureAlignment.Neutral
            };
    }
}