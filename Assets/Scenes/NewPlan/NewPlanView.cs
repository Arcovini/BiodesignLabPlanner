using UnityEngine;
using UnityEngine.UIElements;
using System;
using BiodesignLab.CustomVisualElements;

namespace BiodesignLab
{
    public class NewPlanView : IDisposable
    {
        // private Button loadButton;
        // private Button continueButton;
        // private Button confirmButton;

        // private Button back1Button;
        // private Button back2Button;

        // private VisualElement loaderContainer;
        // private VisualElement friedmanContainer;
        // private VisualElement coronalContainer;

        // private VisualElement variableSliceViewer;

        private NewPlanLoader loader;
        
        public NewPlanView(VisualElement root)
        {
            var loaderRoot = root.Q<VisualElement>("Loader");
            this.loader = new NewPlanLoader(loaderRoot);

            // this.loadButton = root.Q<Button>("LoadButton");
            // this.back1Button = root.Q<Button>("BackButton");
            // this.back2Button = root.Q<Button>("Back2Button");
            // this.continueButton = root.Q<Button>("ContinueButton");
            // this.confirmButton = root.Q<Button>("ConfirmAxisButton");
            
            // this .loaderContainer = root.Q<VisualElement>("LoaderContainer");
            // this .friedmanContainer = root.Q<VisualElement>("FriedmanContainer");
            // this .coronalContainer = root.Q<VisualElement>("CoronalContainer");
            // this .variableSliceViewer = root.Q<VisualElement>("VariableSliceViewer");


            // this.loaderContainer.style.display = DisplayStyle.Flex;
            // this.friedmanContainer.style.display = DisplayStyle.None;
            // this.coronalContainer.style.display = DisplayStyle.None;
            // this.variableSliceViewer.style.display = DisplayStyle.None;


            // this.loadButton.RegisterCallback<ClickEvent>(OnLoadButtonPressed);
            // this.continueButton.RegisterCallback<ClickEvent>(OnContinueButtonPressed);
            // this.confirmButton.RegisterCallback<ClickEvent>(OnConfirmAxisButtonPressed);

            // this.back1Button.RegisterCallback<ClickEvent>(OnBackButtonPressed);
        }
        
        public void Dispose()
        {
            // this.loadButton.UnregisterCallback<ClickEvent>(OnLoadButtonPressed);
            // this.continueButton.UnregisterCallback<ClickEvent>(OnContinueButtonPressed);
            // this.confirmButton.UnregisterCallback<ClickEvent>(OnConfirmAxisButtonPressed);
            // this.back1Button.UnregisterCallback<ClickEvent>(OnBackButtonPressed);
        }

        // private void OnLoadButtonPressed(ClickEvent e)
        // {
        //     // FileEvents.OpenFolderExplorer?.Invoke();
        //     this.loaderContainer.style.display = DisplayStyle.None;
        //     this.coronalContainer.style.display = DisplayStyle.None;
        //     this.friedmanContainer.style.display = DisplayStyle.Flex;
        // }


        // private void OnContinueButtonPressed(ClickEvent e) { 
        //     this.loaderContainer.style.display = DisplayStyle.None;
        //     this.coronalContainer.style.display = DisplayStyle.Flex;
        //     this.variableSliceViewer.style.display = DisplayStyle.Flex;

        //     this.friedmanContainer.style.display = DisplayStyle.None;
        // }

        // private void OnConfirmAxisButtonPressed(ClickEvent e) { 

        // }
    }
}