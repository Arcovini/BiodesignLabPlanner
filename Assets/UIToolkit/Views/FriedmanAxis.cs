using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System;

namespace BiodesignLab.CustomVisualElements
{
    [UxmlElement]
    public partial class FriedmanAxis : VisualElement, IDisposable
    {
        private Color trigonumColor;
        private Color glenoidColor;
        private Color outlineColor;
        private float outlineThickness;
        private float radius;
        private float size;

        private Point trigonumPoint;
        private Point glenoidPoint;

        private const string styleSheetPath = "Assets/UIToolkit/USS/FriedmanAxis.uss";

        public FriedmanAxis()
        {
            SetVisualElements();
            RegisterCallbacks();
        }

        private void SetVisualElements()
        {
            name = "FriedmanAxis";

            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheetPath));
            AddToClassList("friedman-axis");

            this.trigonumPoint = new Point();
            this.glenoidPoint = new Point();

            this.trigonumPoint.AddToClassList("point");
            this.trigonumPoint.AddToClassList("trigonum-point");

            this.glenoidPoint.AddToClassList("point");
            this.glenoidPoint.AddToClassList("glenoid-point");

            Add(this.trigonumPoint);
            Add(this.glenoidPoint);

            generateVisualContent += OnGenerateVisualContent;
        }

        private void RegisterCallbacks()
        {
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        public void Dispose()
        {
            UnregisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }

        // TODO: improve
        private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
        {
            if(e.customStyle.TryGetValue(new CustomStyleProperty<Color>("--trigonum-color"), out var trigonumColorValue))
                this.trigonumColor = trigonumColorValue;
            if(e.customStyle.TryGetValue(new CustomStyleProperty<Color>("--glenoid-color"), out var glenoidColorValue))
                this.glenoidColor = glenoidColorValue;
            if(e.customStyle.TryGetValue(new CustomStyleProperty<Color>("--outline-color"), out var outlineColorValue))
                this.outlineColor = outlineColorValue;
            if(e.customStyle.TryGetValue(new CustomStyleProperty<float>("--outline-thickness"), out var outlineThicknessValue))
                this.outlineThickness = outlineThicknessValue;
            if(e.customStyle.TryGetValue(new CustomStyleProperty<float>("--radius"), out var radiusValue))
                this.radius = radiusValue;
            if(e.customStyle.TryGetValue(new CustomStyleProperty<float>("--size"), out var sizeValue))
                this.size = sizeValue;

            this.trigonumPoint.Color = this.trigonumColor;
            this.trigonumPoint.OutlineColor = this.outlineColor;
            this.trigonumPoint.OutlineThickness = this.outlineThickness;
            this.trigonumPoint.Radius = this.radius;
            this.trigonumPoint.Size = this.size;

            this.glenoidPoint.Color = this.glenoidColor;
            this.glenoidPoint.OutlineColor = this.outlineColor;
            this.glenoidPoint.OutlineThickness = this.outlineThickness;
            this.glenoidPoint.Radius = this.radius;
            this.glenoidPoint.Size = this.size;

            this.trigonumPoint.MarkDirtyRepaint();
            this.glenoidPoint.MarkDirtyRepaint();
        }

        private void OnGenerateVisualContent(MeshGenerationContext context)
        {
            Painter2D painter = context.painter2D;

            painter.strokeColor = this.outlineColor;
            painter.lineWidth = this.outlineThickness;
            painter.lineJoin = LineJoin.Miter;
            painter.lineCap = LineCap.Butt;

            painter.BeginPath();
            painter.MoveTo(this.trigonumPoint.Position);
            painter.LineTo(this.glenoidPoint.Position);
            painter.Stroke();
            painter.ClosePath();
        }
    }
}