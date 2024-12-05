using UnityEngine;
using UnityEditor;
using System;

namespace BiodesignLab
{
    public class SlicingPlane : Plane, IDisposable
    {
        public CustomRenderTexture Texture
        {
            get { return this.customRenderTexture; }
            private set {}
        }

        private CustomRenderTexture customRenderTexture;
        private Material renderMaterial;

        private Vector3 startPoint;
        private Vector3 endPoint;

        public  SlicingPlane(float width, float height, Vector3 position, Vector3 normal) : base(width, height, position, normal)
        {
            CreateRenderMaterial();
            CreateRenderTexture();
        }

        public SlicingPlane(DicomVolume volume, float width, float height, Vector3 position, Vector3 normal) : base(width, height, position, normal)
        {
            CreateRenderMaterial();
            CreateRenderTexture();
            SetVolume(volume);
        }

        public void SetVolume(DicomVolume volume)
        {
            SetDistancePoints(volume);

            this.renderMaterial.SetTexture("_VolumeTex", volume.Texture);
            this.renderMaterial.SetMatrix("_LocalToWorld", LocalToWorldMatrix);
            this.renderMaterial.SetMatrix("_WorldToVolume", volume.Transform.worldToLocalMatrix);

            Update();
        }

        public void SetPosition(float value)
        {
            Position = Vector3.Lerp(this.startPoint, this.endPoint, value);
            this.renderMaterial.SetMatrix("_LocalToWorld", LocalToWorldMatrix);
            Update();
        }

        public void Dispose()
        {
            this.customRenderTexture.Release();
        }

        private void Update()
        {
            Graphics.Blit(null, this.customRenderTexture, this.renderMaterial);
        }

        private void CreateRenderMaterial()
        {
            this.renderMaterial = new Material(AssetDatabase.LoadAssetAtPath<Shader>("Assets/Scripts/Shaders/SliceView.shader"));
        }

        private void CreateRenderTexture()
        {
            this.customRenderTexture = new CustomRenderTexture(1024, 1024, RenderTextureFormat.ARGB32);
            this.customRenderTexture.enableRandomWrite = true;
            this.customRenderTexture.filterMode = FilterMode.Bilinear;
            this.customRenderTexture.Create();
        }

        private void SetDistancePoints(DicomVolume volume)
        {
            // Position the ray at the center of the volume and have it facing the plane's normal direction
            Ray r = new Ray();
            r.origin = volume.Position;
            r.direction = Normal;
            
            // Get the distance to the closest point inside the volume
            float t = 0.0f;
            volume.Collider.bounds.IntersectRay(r, out t);

            // This distance is the maximum travel distance we can go within the volume in that direction, starting from the center
            float distance = Mathf.Abs(t);

            this.startPoint = r.origin - r.direction * distance;
            this.endPoint = r.origin + r.direction * distance;
        }
    }
}