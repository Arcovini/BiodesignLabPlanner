using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BiodesignLab
{
    public class NewPlanView : IDisposable
    {
        private Button loadButton;
        
        private Button backButton;

        public NewPlanView(VisualElement root)
        {
            this.loadButton = root.Q<Button>("LoadButton");
            this.backButton = root.Q<Button>("BackButton");

            this.loadButton.RegisterCallback<ClickEvent>(OnLoadButtonPressed);
            this.backButton.RegisterCallback<ClickEvent>(OnBackButtonPressed);
        }
        
        public void Dispose()
        {
            this.loadButton.UnregisterCallback<ClickEvent>(OnLoadButtonPressed);
            this.backButton.UnregisterCallback<ClickEvent>(OnBackButtonPressed);
        }

        private void OnLoadButtonPressed(ClickEvent e)
        {
            FileEvents.OpenFolderExplorer?.Invoke();
        }

        private void OnBackButtonPressed(ClickEvent e)
        {
            SceneEvents.UnloadScene?.Invoke();
        }
    }
}