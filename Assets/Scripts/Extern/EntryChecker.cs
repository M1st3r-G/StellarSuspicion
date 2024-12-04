using System.Diagnostics;
using System.Linq;
using Controller.Creature;
using Data;

namespace Extern
{
    public static class EntryChecker
    {
        public static CreatureAlignment IsGood(this CreatureController controller)
        {
            Debug.Assert(controller.CurrentCreature != null, "controller.CurrentCreature != null");
            return controller.CurrentCreature.Value.GetAllParts()
                    .Aggregate(controller.RatingFromQuestions, (c, part) => c + part.goodness) switch
                {
                    > 0 => CreatureAlignment.Good,
                    < 0 => CreatureAlignment.Evil,
                    _ => CreatureAlignment.Neutral
                };
        }
    }
}