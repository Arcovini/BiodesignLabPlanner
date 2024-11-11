using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System;
using BiodesignLab.CustomVisualElements;

namespace BiodesignLab
{
    public class NewPlanLoader : IDisposable
    {
        private Button loadButton;
        private Button backButton;
        private Button nextButton;

        private SliceViewer axialSliceViewer;
        private SliceViewer coronalSliceViewer;

        public NewPlanLoader(VisualElement root)
        {
            this.loadButton = root.Q<Button>("LoadButton");
            this.backButton = root.Q<Button>("BackButton");
            this.nextButton = root.Q<Button>("NextButton");
            this.axialSliceViewer = root.Q<SliceViewer>("AxialSliceViewer");
            this.coronalSliceViewer = root.Q<SliceViewer>("CoronalSliceViewer");

            this.loadButton.RegisterCallback<ClickEvent>(OnLoadButtonPressed);
            this.backButton.RegisterCallback<ClickEvent>(OnBackButtonPressed);
            this.nextButton.RegisterCallback<ClickEvent>(OnNextButtonPressed);

            FileEvents.DicomLoaded += OnDicomLoaded;
            FileEvents.DicomUnloaded += OnDicomUnloaded;
        }

        public void Dispose()
        {
            this.loadButton.UnregisterCallback<ClickEvent>(OnLoadButtonPressed);
            this.backButton.UnregisterCallback<ClickEvent>(OnBackButtonPressed);
            this.nextButton.UnregisterCallback<ClickEvent>(OnNextButtonPressed);

            FileEvents.DicomLoaded -= OnDicomLoaded;
            FileEvents.DicomUnloaded -= OnDicomUnloaded;
        }

        private void OnLoadButtonPressed(ClickEvent e)
        {
            FileEvents.OpenFolderExplorer?.Invoke();
        }

        private void OnBackButtonPressed(ClickEvent e)
        {
            SceneEvents.UnloadScene?.Invoke();
        }

        private void OnNextButtonPressed(ClickEvent e)
        {
            throw new NotImplementedException();
        }

        private void OnDicomLoaded(Dicom dicom)
        {
            DicomVolume volume = dicom.Volume;

            var axialSlice = new SlicingPlane(volume.PhysicalWidth, volume.PhysicalHeight, volume.Position, new Vector3(0.0f, -1.0f, 0.0f));
            var coronalSlice = new SlicingPlane(volume.PhysicalHeight, volume.PhysicalLength, volume.Position, new Vector3(0.0f, 0.0f, -1.0f));

            axialSlice.SetVolume(volume);
            coronalSlice.SetVolume(volume);

            this.axialSliceViewer.SetSlice(axialSlice);
            this.coronalSliceViewer.SetSlice(coronalSlice);
        }

        private void OnDicomUnloaded()
        {
            // Slice viewer unset texture and delete link
        }
    }
}