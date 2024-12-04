using System.Diagnostics;
using System.Linq;
using Controller.Creature;
using Data;

namespace Extern
{
    public static class EntryChecker
    {
        public static int GetGoodness(this CreatureController controller, out CreatureAlignment alignment)
        {
            Debug.Assert(controller.CurrentCreature != null, "controller.CurrentCreature != null");

            int goodness = controller.CurrentCreature.Value.GetAllParts()
                .Aggregate(controller.RatingFromQuestions, (c, part) => c + part.goodness); 
            
            alignment = goodness switch
            {
                > 0 => CreatureAlignment.Good,
                < 0 => CreatureAlignment.Evil,
                _ => CreatureAlignment.Neutral
            };

            return goodness;
        }
    }
}