using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Controller.UI
{
    public class PageContentController : MonoBehaviour
    {
        [SerializeField] private List<string> content;

        [SerializeField] private TextMeshProUGUI output;
    
        public void ShowContentAtIndex(int index) => output.text = content[index];
    }
}
