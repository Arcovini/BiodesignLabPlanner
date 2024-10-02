using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEditor;

namespace BiodesignLab
{
    public class MainMenuView : IDisposable
    {
        private VisualElement root;

        private Button newPlanButton;
        private Button loadPlanButton;
        private Button settingsButton;
        private Button quitButton;

        public MainMenuView(VisualElement root)
        {
            this.root = root;

            this.newPlanButton = root.Q<Button>("NewPlanButton");
            this.loadPlanButton = root.Q<Button>("LoadPlanButton");
            this.settingsButton = root.Q<Button>("SettingsButton");
            this.quitButton = root.Q<Button>("QuitButton");

            this.newPlanButton.RegisterCallback<ClickEvent>(OnNewPlanButtonPressed);
            this.loadPlanButton.RegisterCallback<ClickEvent>(OnLoadPlanButtonPressed);
            this.settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonPressed);
            this.quitButton.RegisterCallback<ClickEvent>(OnQuitButtonPressed);
        }

        public void Dispose()
        {
            this.newPlanButton.UnregisterCallback<ClickEvent>(OnNewPlanButtonPressed);
            this.loadPlanButton.UnregisterCallback<ClickEvent>(OnLoadPlanButtonPressed);
            this.settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonPressed);
            this.quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonPressed);
        }

        private void OnNewPlanButtonPressed(ClickEvent e)
        {
            SceneEvents.LoadScene?.Invoke(Scene.NewPlan);
        }

        private void OnLoadPlanButtonPressed(ClickEvent e)
        {
            SceneEvents.LoadScene?.Invoke(Scene.Planner);
        }
        private void OnSettingsButtonPressed(ClickEvent e)
        {
            throw new NotImplementedException();
        }

        private void OnQuitButtonPressed(ClickEvent e)
        {
            SceneEvents.QuitApplication?.Invoke();
        }
    }
}