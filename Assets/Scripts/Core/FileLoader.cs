using System;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using FellowOakDicom;

namespace BiodesignLab
{
    public static class FileLoader
    {
        public static DicomFile OpenDicomFile(string path)
        {
            DicomFile file = null;

            try
            {
                file = DicomFile.Open(path);
            }
            catch(Exception e)
            {
                // TODO: Add runtime handler
                Debug.Log(e);
            }

            return file;
        }

        public static DicomFile[] OpenDicomFolder(string path)
        {
            DicomFile[] files = null;
            
            try
            {
                FileInfo[] fileInfo = new DirectoryInfo(path).GetFiles("*.dcm");
                files = new DicomFile[fileInfo.Length];

                for(int i = 0; i < fileInfo.Length; i++)
                    files[i] = OpenDicomFile(fileInfo[i].FullName);
            }
            catch(Exception e)
            {
                // TODO: Add runtime handler
                Debug.Log(e);
            }

            return files;
        }
    }
}