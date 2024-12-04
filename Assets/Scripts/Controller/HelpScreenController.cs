using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class HelpScreenController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> content;
        [SerializeField] private GameObject currentSelection;
        private int _currentIndex = -1;

        #region Content

        public void MoveLeft()
        {
            _currentIndex--;
            if (_currentIndex < 0) _currentIndex += content.Count;

            DisplayContent();
        }
        
        public void MoveRight()
        {
            _currentIndex++;
            if (_currentIndex == content.Count) _currentIndex = 0;

            DisplayContent();
        }
        
        private void DisplayContent()
        {
            currentSelection.SetActive(false);
            currentSelection = content[_currentIndex];
            currentSelection.SetActive(true);
        }

        #endregion
    }
}