using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace BiodesignLab
{
    public class MainMenuPresenter : MonoBehaviour
    {   
        // View
        private VisualElement root;
        private MainMenuView view;

        private void Start()
        {
            this.root = GetComponent<UIDocument>().rootVisualElement;
            this.view = new MainMenuView(root);
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}