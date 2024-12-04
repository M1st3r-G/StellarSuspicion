using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class HelpScreenController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> content;
        
        private int _currentIndex;

        //private void Awake() => DisplayContent();

        #region Content

        public void MoveLeft()
        {
            _currentIndex--;
            if (_currentIndex == -1) _currentIndex += content.Count;

            DisplayContent();
        }
        
        public void MoveRight()
        {
            _currentIndex++;
            if (_currentIndex == content.Count) _currentIndex = 0;

            DisplayContent();
        }
        
        private void DisplayContent() => content[_currentIndex] = null;
        #endregion
    }
}