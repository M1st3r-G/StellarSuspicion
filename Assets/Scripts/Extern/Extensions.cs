using UnityEngine;

namespace Extern
{
    public static class Extensions
    {
        public static void SetGroupActive(this CanvasGroup menu, bool state)
        {
            menu.interactable = state;
            menu.blocksRaycasts = state;
            menu.alpha = state ? 1f : 0f;
        }
    }
}