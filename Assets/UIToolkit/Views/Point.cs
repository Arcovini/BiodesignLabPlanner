using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BiodesignLab.CustomVisualElements
{
    public class Point : VisualElement, IDisposable
    {
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public Color OutlineColor
        {
            get { return this.outlineColor; }
            set { this.outlineColor = value; }
        }

        public float Radius
        {
            get { return this.radius; }
            set { this.radius = value; }
        }

        public float OutlineThickness
        {
            get { return this.outlineThickness; }
            set { this.outlineThickness = value; }
        }

        private Color color = Color.red;
        private Color outlineColor = Color.red;
        private float radius = 5.0f;
        private float outlineThickness = 3.0f;

        public Point(Color color, Color outlineColor, float radius, float outlineThickness)
        {
            this.color = color;
            this.outlineColor = outlineColor;
            this.radius = radius;
            this.outlineThickness = outlineThickness;

            SetVisualElements();
            RegisterCallbacks();            
        }

        private void SetVisualElements()
        {
            name = "Point";
            generateVisualContent += OnGenerateVisualContent;
        }

        private void RegisterCallbacks()
        {

        }

        public void Dispose()
        {
            
        }

        private void OnGenerateVisualContent(MeshGenerationContext context)
        {
            style.width = 2 * this.radius;
            style.height = 2 * this.radius;  

            Painter2D painter = context.painter2D;

            painter.fillColor = this.outlineColor;
            painter.BeginPath();
            painter.Arc(new Vector2(this.radius, this.radius), this.radius + this.outlineThickness, 0.0f, 360.0f);
            painter.Fill();

            painter.fillColor = this.color;
            painter.BeginPath();
            painter.Arc(new Vector2(this.radius, this.radius), this.radius, 0.0f, 360.0f);
            painter.Fill();
        }
    }
}