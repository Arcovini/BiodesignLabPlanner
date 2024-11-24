using UnityEngine;
using UnityEngine.UIElements;
using System;
using BiodesignLab.CustomVisualElements;

namespace BiodesignLab
{
    public class NewPlanView : IDisposable
    {
        private VisualElement root;

        private Button loadButton;
        private Button backButton;
        private Button nextButton;

        private SliceViewer axialSliceViewer;
        private SliceViewer coronalSliceViewer;
        
        private DicomVolume volume;
        
        public NewPlanView(VisualElement root)
        {
            this.root = root;

            SetVisualElements();
            RegisterCallbacks();
        }   
        
        private void SetVisualElements()
        {
            this.loadButton = this.root.Q<Button>("LoadButton");
            this.backButton = this.root.Q<Button>("BackButton");
            this.nextButton = this.root.Q<Button>("NextButton");

            this.axialSliceViewer = this.root.Q<SliceViewer>("AxialSliceViewer");
            this.coronalSliceViewer = this.root.Q<SliceViewer>("CoronalSliceViewer");
        }

        private void RegisterCallbacks()
        {
            this.loadButton.clicked += OpenFileExplorer;
            this.backButton.clicked += UnloadScene;

            FileEvents.DicomLoaded += OnDicomLoaded;
            FileEvents.DicomUnloaded += OnDicomUnloaded;
        }

        public void Dispose()
        {
            this.loadButton.clicked -= OpenFileExplorer;

            FileEvents.DicomLoaded -= OnDicomLoaded;
            FileEvents.DicomUnloaded -= OnDicomUnloaded;
        }

        private void OpenFileExplorer()
        {
            FileEvents.OpenFolderExplorer?.Invoke();
        }

        private void UnloadScene()
        {
            SceneEvents.UnloadScene?.Invoke();
        }

        private void LoadScene()
        {
            SceneEvents.LoadScene?.Invoke(Scene.Planner);
        }

        private void UnloadSegment()
        {
            var loader = this.root.Q<VisualElement>("Loader");
            var adjustment = this.root.Q<VisualElement>("Adjustment");
            
            this.coronalSliceViewer.Reset();
            
            loader.style.display = DisplayStyle.Flex;
            adjustment.style.display = DisplayStyle.None;

            this.backButton.clicked -= UnloadSegment;
            this.backButton.clicked += UnloadScene;

            this.nextButton.clicked -= LoadScene;
            this.nextButton.clicked += UnloadScene;
        }

        private void LoadSegment()
        {
            var loader = this.root.Q<VisualElement>("Loader");
            var adjustment = this.root.Q<VisualElement>("Adjustment");

            loader.style.display = DisplayStyle.None;
            adjustment.style.display = DisplayStyle.Flex;
            
            // calculate friedman axis and load coronal view
            var axialSlice = new SlicingPlane(this.volume, this.volume.PhysicalWidth, this.volume.PhysicalHeight, this.volume.Position, new Vector3(0.0f, -1.0f, 0.0f));
            this.coronalSliceViewer.SetSlice(axialSlice);

            this.backButton.clicked -= UnloadScene;
            this.backButton.clicked += UnloadSegment;

            this.nextButton.clicked -= LoadSegment;
            this.nextButton.clicked += LoadScene;
        }

        private void OnDicomLoaded(Dicom dicom)
        {
            this.volume = dicom.Volume;

            var axialSlice = new SlicingPlane(this.volume, this.volume.PhysicalWidth, this.volume.PhysicalHeight, this.volume.Position, new Vector3(0.0f, -1.0f, 0.0f));
            this.axialSliceViewer.SetSlice(axialSlice);

            this.nextButton.clicked += LoadSegment;
        }

        private void OnDicomUnloaded()
        {
            this.axialSliceViewer.Reset();
        }
    }
}