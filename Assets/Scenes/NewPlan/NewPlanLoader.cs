using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System;
using BiodesignLab.CustomVisualElements;

namespace BiodesignLab
{
    public class NewPlanLoader : IDisposable
    {
        private VisualElement root;

        private Button loadButton;
        private SliceViewer axialSliceViewer;
        private SliceViewer coronalSliceViewer;

        public NewPlanLoader(VisualElement root)
        {
            this.root = root;

            SetVisualElements();
            RegisterCallbacks();
        }

        private void SetVisualElements()
        {
            this.loadButton = this.root.Q<Button>("LoadButton");
            this.axialSliceViewer = this.root.Q<SliceViewer>("AxialSliceViewer");
            this.coronalSliceViewer = this.root.Q<SliceViewer>("CoronalSliceViewer");
        }

        private void RegisterCallbacks()
        {
            this.loadButton.RegisterCallback<ClickEvent>(OnLoadButtonPressed);
            
            FileEvents.DicomLoaded += OnDicomLoaded;
            FileEvents.DicomUnloaded += OnDicomUnloaded;
        }

        public void Dispose()
        {
            this.loadButton.UnregisterCallback<ClickEvent>(OnLoadButtonPressed);

            FileEvents.DicomLoaded -= OnDicomLoaded;
            FileEvents.DicomUnloaded -= OnDicomUnloaded;
        }

        public void SetVisible(bool visible)
        {
            if(visible)
                this.root.style.display = DisplayStyle.Flex;
            else
                this.root.style.display = DisplayStyle.None;
        }

        private void OnLoadButtonPressed(ClickEvent e)
        {
            FileEvents.OpenFolderExplorer?.Invoke();
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
            this.axialSliceViewer.Reset();
            this.coronalSliceViewer.Reset();
        }
    }
}