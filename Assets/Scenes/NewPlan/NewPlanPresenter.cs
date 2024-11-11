using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using FellowOakDicom;
using System;

namespace BiodesignLab
{
    public class NewPlanPresenter : MonoBehaviour
    {
        // Model
        private Dicom dicom;
        
        // View
        private NewPlanView view;

        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            this.view = new NewPlanView(root);
        }

        private void OnEnable()
        {
            FileEvents.OpenFolderExplorer += OnOpenFolderExplorer;
        }

        private void OnDisable()
        {
            FileEvents.OpenFolderExplorer -= OnOpenFolderExplorer;
        }

        private void OnOpenFolderExplorer()
        {
            string path = EditorUtility.OpenFolderPanel("Select a folder to open", "", "");

            if(String.IsNullOrEmpty(path))
                return;

            DicomFile[] files = FileLoader.OpenDicomFolder(path);
            
            if(files is not null)
                LoadDicom(files);
        }

        private void LoadDicom(DicomFile[] files)
        {
            if(this.dicom is not null)
            {
                Destroy(this.dicom.gameObject);
                FileEvents.DicomUnloaded?.Invoke();
            }

            this.dicom = Instantiate(Resources.Load<GameObject>("Prefabs/Dicom")).GetComponent<Dicom>();
            this.dicom.Init(files);

            FileEvents.DicomLoaded?.Invoke(this.dicom);
        }
    }
}