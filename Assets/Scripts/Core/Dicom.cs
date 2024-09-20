using UnityEngine;
using FellowOakDicom;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Render;
using System;
using System.Linq;

namespace BiodesignLab
{
    public class Dicom : MonoBehaviour
    {
        [Header("Metadata")]

        [Header("Patient")]
        [ReadOnly] public string PatientName;
        [ReadOnly] public string PatientID;
        [ReadOnly] public string PatientSex;

        [Header("Image Plane")]
        [ReadOnly] public float SliceThickness;
        [ReadOnly] public float[] ImagePositionPatient;
        [ReadOnly] public float[] ImageOrientationPatient;
        [ReadOnly] public float[] PixelSpacing;

        [Header("VOI LUT")]
        [ReadOnly] public float WindowWidth;
        [ReadOnly] public float WindowCenter;

        [Header("Modality LUT")]
        [ReadOnly] public float RescaleSlope;
        [ReadOnly] public float RescaleIntercept;
        
        public DicomFile[] Files => this.files;
        public DicomVolume Volume => this.volume;

        private DicomFile[] files;
        private DicomVolume volume;

        public void Init(DicomFile[] files)
        {
            this.files = files;

            // Preamble
            DicomFile file = files.First();

            PatientName = file.Dataset.GetSingleValue<string>(DicomTag.PatientName);
            PatientID = file.Dataset.GetSingleValue<string>(DicomTag.PatientID);
            PatientSex = file.Dataset.GetSingleValue<string>(DicomTag.PatientSex);

            SliceThickness = TypeConverter.StringToFloat(file.Dataset.GetSingleValue<string>(DicomTag.SliceThickness));
            ImagePositionPatient = TypeConverter.StringArrayToFloat(file.Dataset.GetValues<string>(DicomTag.ImagePositionPatient));
            ImageOrientationPatient = TypeConverter.StringArrayToFloat(file.Dataset.GetValues<string>(DicomTag.ImageOrientationPatient));
            PixelSpacing = TypeConverter.StringArrayToFloat(file.Dataset.GetValues<string>(DicomTag.PixelSpacing));

            WindowWidth = file.Dataset.GetSingleValue<float>(DicomTag.WindowWidth);
            WindowCenter = file.Dataset.GetSingleValue<float>(DicomTag.WindowCenter);

            RescaleSlope = file.Dataset.GetSingleValue<float>(DicomTag.RescaleSlope);
            RescaleIntercept = file.Dataset.GetSingleValue<float>(DicomTag.RescaleIntercept);

            LoadVolume();
        }

        private void LoadVolume()
        {
            // Pixel Data
            DicomPixelData pixelData = DicomPixelData.Create(this.files.First().Dataset);
            IPixelData dataInferface = PixelDataFactory.Create(pixelData, 0);

            var width = pixelData.Width;
            var height = pixelData.Height;
            var length = Files.Length;

            this.volume = GetComponent<DicomVolume>();
            this.volume.Init(width, height, length);

            if(dataInferface is GrayscalePixelDataS16)
                LoadGrayscalePixelData();
        }

        private void LoadGrayscalePixelData()
        {
            int x = this.volume.VoxelWidth;
            int y = this.volume.VoxelHeight;
            int z = this.volume.VoxelLength;

            this.volume.Texture = new Texture3D(x, y, z, TextureFormat.RGB24, true);
            Color[] colors = new Color[x * y * z];

            // // Window range
            float minRange = WindowCenter - 0.5f * WindowWidth;
            float maxRange = WindowCenter + 0.5f * WindowWidth;
            
            z = 0;
            foreach(DicomFile file in Files)
            {
                DicomPixelData pixelData = DicomPixelData.Create(file.Dataset);
                IPixelData dataInferface = PixelDataFactory.Create(pixelData, 0);

                Color[] pixelColor = new Color[x * y];
                short[] data = (dataInferface as GrayscalePixelDataS16).Data;

                for(int i = 0; i < data.Length; i++)
                {
                    // Convert data to Hounsfield Units (HU)
                    float hounsfield = Mathf.Clamp((RescaleSlope * data[i]) + RescaleIntercept, minRange, maxRange);

                    // Adjust range to grayscale
                    float scale = AdjustRange(hounsfield, minRange, maxRange);
         
                    // Grayscale value
                    pixelColor[i] = new Color(scale, scale, scale);
                }

                Array.Copy(pixelColor, 0, colors, x * y * z, x * y);
                z++;   
            }

            this.volume.Texture.SetPixels(colors);
            this.volume.Texture.Apply();
        }

        private float AdjustRange(float x, float minRange, float maxRange)
        {
            float min = Mathf.Abs(minRange);
            float max = Mathf.Abs(maxRange);
            float y = x * (1.0f / (min + max)) + (min / (min + max));

            return Mathf.Clamp(y, 0, 1);
        }
    }
}