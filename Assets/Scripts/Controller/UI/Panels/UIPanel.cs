using Extern;
using UnityEngine;

namespace Controller.UI.Panels
{
    public class UIPanel : MonoBehaviour
    {
        private CanvasGroup _myGroup;

        protected bool IsVisible => _myGroup.alpha > 0.5;
        
        protected virtual void Awake()
        {
            _myGroup = GetComponent<CanvasGroup>();
        }
        
        public void SetMenuActive(bool b) => _myGroup.SetGroupActive(b);
    }
    
}