using _Scripts.Utilities.Classes;
using UnityEditor;
using UnityEngine.UIElements;

namespace _Scripts.Editor
{
    [CustomPropertyDrawer(typeof(ShaderProperty<>))]
    public class ShaderPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            
            return base.CreatePropertyGUI(property);
        }
    }
}