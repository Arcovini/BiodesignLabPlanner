using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace BiodesignLab
{
    public enum Style { Light, Dark }

    public class DarkThemeTest : MonoBehaviour
    {
        [SerializeField] UIDocument document;

        // TODO: Iterate through the folder and load the files
        const string darkTheme = "Assets/UIToolkit/USS/Dark/DarkTheme.uss";
        const string darkButtons = "Assets/UIToolkit/USS/Dark/DarkButtons.uss";
        const string darkText = "Assets/UIToolkit/USS/Dark/DarkText.uss";
        const string darkSliceViewer = "Assets/UIToolkit/USS/Dark/DarkSliceViewer.uss";
        const string darkImage = "Assets/UIToolkit/USS/Dark/DarkImage.uss";

        const string lightTheme = "Assets/UIToolkit/USS/Light/LightTheme.uss";
        const string lightButtons = "Assets/UIToolkit/USS/Light/LightButtons.uss";
        const string lightText = "Assets/UIToolkit/USS/Light/LightText.uss";
        const string lightSliceViewer = "Assets/UIToolkit/USS/Light/LightSliceViewer.uss";
        const string lightImage = "Assets/UIToolkit/USS/Light/LightImage.uss";

        VisualElement root;
        Style currentStyle;

        private void Start()
        {
            root = document.rootVisualElement;
            currentStyle = Style.Light;
        }

        // TODO: save theme as PlayerPref so it can persist throughout the application
        // TODO: Iterate through the folder and load the files
        private void Update()
        {
            if(Input.GetKeyDown((KeyCode.Space)))
            {
                root.styleSheets.Clear();
                
                switch(currentStyle)
                {
                    case Style.Light:
                    {
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(darkTheme));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(darkButtons));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(darkText));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(darkSliceViewer));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(darkImage));
                        currentStyle = Style.Dark;
                        break;
                    }
                    case Style.Dark:
                    {
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(lightTheme));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(lightButtons));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(lightText));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(lightSliceViewer));
                        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(lightImage));
                        currentStyle = Style.Light;
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}