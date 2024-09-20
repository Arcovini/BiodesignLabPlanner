using UnityEngine;

namespace BiodesignLab
{
    public class DicomVolume : MonoBehaviour
    {
        [Header("Volume Texture")]
        public Texture3D Texture = null;

        [Header("Voxel")]
        [ReadOnly] public int VoxelWidth;
        [ReadOnly] public int VoxelHeight;
        [ReadOnly] public int VoxelLength;

        [Header("Physical")]
        [ReadOnly] public float PhysicalWidth;
        [ReadOnly] public float PhysicalHeight;
        [ReadOnly] public float PhysicalLength;

        public Transform Transform => transform;
        public Vector3 Position  => transform.position;
        
        public Collider Collider => GetComponent<Collider>();
        public Vector3 BoundsMin => GetComponent<Collider>().bounds.min;
        public Vector3 BoundsMax => GetComponent<Collider>().bounds.max;

        private Dicom dicom;

        public void Init(int width, int height, int length)
        {
            this.dicom = GetComponent<Dicom>();

            VoxelWidth = width;
            VoxelHeight = height;
            VoxelLength = length;

            float pixelRowSpacing = dicom.PixelSpacing[0];
            float pixelColumnSpacing = dicom.PixelSpacing[1];
            float sliceThickness = dicom.SliceThickness;

            PhysicalHeight = pixelRowSpacing * height + pixelRowSpacing;
            PhysicalWidth = pixelColumnSpacing * width + pixelColumnSpacing;
            PhysicalLength = sliceThickness * length;
            
            PhysicalHeight = PhysicalHeight.Millimeters();
            PhysicalWidth  = PhysicalWidth.Millimeters();
            PhysicalLength = PhysicalLength.Millimeters();

            SetBoundingBox();
        }

        public void SetBoundingBox()
        {
            // Scale
            transform.localScale = new Vector3(PhysicalWidth, PhysicalHeight, PhysicalLength);

            // TODO: refactor
            // Rotation
            var orientation = this.dicom.ImageOrientationPatient;

            Vector3 rowOrientation = new Vector3(orientation[0], orientation[1], orientation[2]);
            Vector3 columnOrientation = new Vector3(orientation[3], orientation[4], orientation[5]);
            Vector3 depthOrientation = Vector3.Cross(rowOrientation, columnOrientation);

            Matrix4x4 localToWorld = new Matrix4x4();
            localToWorld.SetColumn(0, new Vector4(rowOrientation.x, rowOrientation.y, rowOrientation.z, 0.0f));
            localToWorld.SetColumn(1, new Vector4(columnOrientation.x, columnOrientation.y, columnOrientation.z, 0.0f));
            localToWorld.SetColumn(2, new Vector4(depthOrientation.x, columnOrientation.y, columnOrientation.z, 0.0f));
            localToWorld.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));

            Vector3 forward;
            forward.x = localToWorld.m02;
            forward.y = localToWorld.m12;
            forward.z = localToWorld.m22;

            Vector3 upwards;
            upwards.x = localToWorld.m01;
            upwards.y = localToWorld.m11;
            upwards.z = localToWorld.m21;

            transform.rotation = Quaternion.LookRotation(forward, upwards);

            Physics.SyncTransforms();
        }
    }
}