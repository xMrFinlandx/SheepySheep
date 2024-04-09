using _Scripts.Utilities.Classes;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace _Scripts.Editor
{
    [CustomPropertyDrawer(typeof(ShaderProperty<>))]
    public class ShaderPropertyDrawer : PropertyDrawer
    {
        private ReorderableList list;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3 + (list?.GetHeight() ?? 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var name = property.FindPropertyRelative("_propertyName");
            var values = property.FindPropertyRelative("_values");

            position.y += EditorGUIUtility.singleLineHeight;
            
            var nameRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var listRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, position.height - EditorGUIUtility.singleLineHeight);
            var boxRect = new Rect(position.x - 5, position.y - EditorGUIUtility.singleLineHeight / 2, position.width + 10, position.height - EditorGUIUtility.singleLineHeight);
            
            EditorGUI.HelpBox(boxRect, GUIContent.none);
            
            EditorGUI.PropertyField(nameRect, name);
            
            list ??= new ReorderableList(property.serializedObject, values, true, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, new GUIContent($"Values of {name.stringValue}")),
                drawElementCallback = (rect, index, _, _) =>
                {
                    var element = values.GetArrayElementAtIndex(index);

                    EditorGUI.PropertyField(rect, element, GUIContent.none);
                }
            };
            
            list.DoList(listRect);

            EditorGUI.EndProperty();
        }
    }
}