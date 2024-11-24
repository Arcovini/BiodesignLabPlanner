using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BiodesignLab.CustomVisualElements
{
    [UxmlElement]
    public partial class Point : VisualElement, IDisposable
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

        public float OutlineThickness
        {
            get { return this.outlineThickness; }
            set { this.outlineThickness = value; }
        }

        public float Radius
        {
            get { return this.radius; }
            set { this.radius = value; }
        }

        public float Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(transform.position.x + resolvedStyle.left + Size / 2.0f, transform.position.y + resolvedStyle.top + Size / 2.0f); }
            set {}
        }

        private Color color = Color.red;
        private Color outlineColor = Color.white;
        private float outlineThickness = 3.0f;
        private float radius = 5.0f;
        private float size = 10.0f;

        private Vector3 startPosition;
        private Vector3 pointerStartPosition;

        public Point()
        {
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
            RegisterCallback<PointerDownEvent>(OnPointerDown);
            RegisterCallback<PointerMoveEvent>(OnPointerMove);
            RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        public void Dispose()
        {
            UnregisterCallback<PointerDownEvent>(OnPointerDown);
            UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnPointerDown(PointerDownEvent e)
        {
            this.startPosition = transform.position;
            this.pointerStartPosition = e.position;

            e.target.CapturePointer(e.pointerId);
        }

        private void OnPointerMove(PointerMoveEvent e)
        {
            if(e.target.HasPointerCapture(e.pointerId))
            {
                Vector3 previousPosition = transform.position;

                Vector3 pointerDelta = e.position - this.pointerStartPosition;
                transform.position = new Vector2(this.startPosition.x + pointerDelta.x, this.startPosition.y + pointerDelta.y);

                CheckForCollision(previousPosition);
                parent.MarkDirtyRepaint();
            }
        }

        private void OnPointerUp(PointerUpEvent e)
        {
            e.target.ReleasePointer(e.pointerId);
        }

        private void OnGenerateVisualContent(MeshGenerationContext context)
        {
            style.width = this.size;
            style.height = this.size;

            Painter2D painter = context.painter2D;

            painter.fillColor = this.outlineColor;
            painter.BeginPath();
            painter.Arc(new Vector2(this.size / 2.0f, this.size / 2.0f), this.radius + this.outlineThickness, 0.0f, 360.0f);
            painter.Fill();

            painter.fillColor = this.color;
            painter.BeginPath();
            painter.Arc(new Vector2(this.size / 2.0f, this.size / 2.0f), this.radius, 0.0f, 360.0f);
            painter.Fill();
        }

        private void CheckForCollision(Vector3 previousPosition)
        {
            List<Vector2> offsets = new List<Vector2>() 
            {
                Position + new Vector2( this.radius, this.radius),
                Position + new Vector2(-this.radius, this.radius),
                Position + new Vector2( this.radius,-this.radius),
                Position + new Vector2(-this.radius,-this.radius)
            };

            foreach(Vector3 p in offsets)
            {
                if(p.x < 0.0f || p.y < 0.0f || p.x > parent.resolvedStyle.width || p.y > parent.resolvedStyle.height)
                {
                    transform.position = previousPosition;
                    break;
                }
            }
        }
    }
}