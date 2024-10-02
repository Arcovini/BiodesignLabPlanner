using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BiodesignLab
{
    // TODO: temporary / delete
    public class PlannerView : IDisposable
    {
        private Button backButton;

        public PlannerView(VisualElement root)
        {
            this.backButton = root.Q<Button>("BackButton");
            this.backButton.RegisterCallback<ClickEvent>(OnBackButtonPressed);
        }

        public void Dispose()
        {
            this.backButton.UnregisterCallback<ClickEvent>(OnBackButtonPressed);
        }

        private void OnBackButtonPressed(ClickEvent e)
        {
            SceneEvents.UnloadScene?.Invoke();
        }
    }
}