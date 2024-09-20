using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BiodesignLab
{
    public class FriedmanView : IDisposable
    {
        private Button loadButton;
        
        public FriedmanView(VisualElement root)
        {
            this.loadButton = root.Q<Button>("LoadButton");

            this.loadButton.clicked += () => FileEvents.OpenFolderExplorer?.Invoke();
        }
        
        public void Dispose()
        {

        }
    }
}