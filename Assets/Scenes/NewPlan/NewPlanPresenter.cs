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
        private Slice axialSlice;
        private Slice coronalSlice;
        
        // View
        private VisualElement root;
        private NewPlanView view;

        private void Start()
        {
            this.root = GetComponent<UIDocument>().rootVisualElement;
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
                Destroy(this.axialSlice);
                Destroy(this.coronalSlice);
                Destroy(this.dicom);
            }

            var dicomObj = Instantiate(Resources.Load<GameObject>("Prefabs/Dicom"));
            this.dicom = dicomObj.GetComponent<Dicom>();
            this.dicom.Init(files);

            var sliceObj = Instantiate(Resources.Load<GameObject>("Prefabs/Slice"));
            this.axialSlice = sliceObj.GetComponent<Slice>();
        }
    }
}