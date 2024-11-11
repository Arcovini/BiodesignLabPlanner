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
        private float radius;
        private float outlineThickness;

        // Solve custom style

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

            this.trigonumPoint = new Point(Color.green, new Color(1.0f, 0.0f, 1.0f), 3.0f, 2.0f);
            this.glenoidPoint = new Point(new Color(1.0f, 0.5f, 0.1f), new Color(1.0f, 0.0f, 1.0f), 3.0f, 2.0f);

            this.trigonumPoint.AddToClassList("trigonum-point");
            this.glenoidPoint.AddToClassList("glenoid-point");

            Add(this.trigonumPoint);
            Add(this.glenoidPoint);
        }

        private void RegisterCallbacks()
        {

        }

        public void Dispose()
        {

        }
    }
}

    // public CustomElement()
    // {
    //     // Register the CustomStyleResolvedEvent
    //     RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        
    //     // Add a class to apply the style
    //     this.AddToClassList("custom-element");
    // }

    // // Callback method when the custom styles are resolved
    // private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
    // {
    //     // Try to get the custom color variable from the resolved style
    //     if (resolvedStyle.TryGetCustomProperty("my-custom-color", out var colorValue))
    //     {
    //         // Custom property is found, and we convert it to a Color
    //         customColor = new Color(
    //             colorValue.value[0] / 255f, 
    //             colorValue.value[1] / 255f, 
    //             colorValue.value[2] / 255f
    //         );
    //         Debug.Log("Custom color resolved: " + customColor);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Custom style variable 'my-custom-color' not found.");
    //     }
    // }