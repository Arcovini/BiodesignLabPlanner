using UnityEngine;
using System;

namespace BiodesignLab
{
    public static class FileEvents
    {
        public static Action OpenFileExplorer;
        public static Action OpenFolderExplorer;

        public static Action<Dicom> DicomLoaded;
        public static Action DicomUnloaded;
    }
}