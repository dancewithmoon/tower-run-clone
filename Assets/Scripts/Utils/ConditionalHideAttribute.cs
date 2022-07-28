using UnityEngine;
using System;
using UnityEditor;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string SourceField;
    public bool HideInInspector;
    public bool Inverse;
    public object CompareValue;

    public ConditionalHideAttribute(string sourceField, object compareObject, bool inverse = false, bool hideInInspector = true)
    {
        this.SourceField = sourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
        this.CompareValue = compareObject == null ? true : compareObject;
    }

    public ConditionalHideAttribute(string sourceField, bool compareValue = true, bool inverse = false, bool hideInInspector = true)
    {
        this.SourceField = sourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
        this.CompareValue = compareValue;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (!condHAtt.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (!condHAtt.HideInInspector || enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }

    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty propertyA)
    {
        bool enabled = true;


        SerializedProperty sourcePropertyValue = FindSerializableProperty(condHAtt, propertyA);

        if (sourcePropertyValue != null)
        {
            var fieldValue = GetPropertyValue(sourcePropertyValue);

            var comparingValue = condHAtt.CompareValue.ToString();
            var fieldValueString = fieldValue.ToString();

            enabled = comparingValue == fieldValueString;
        }
        else
        {
            Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.SourceField);
        }

        if (condHAtt.Inverse) enabled = !enabled;

        return enabled;
    }

    private SerializedProperty FindSerializableProperty(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        string propertyPath = property.propertyPath;
        int idx = propertyPath.LastIndexOf('.');
        if (idx == -1)
        {
            return property.serializedObject.FindProperty(condHAtt.SourceField);
        }
        else
        {
            propertyPath = propertyPath.Substring(0, idx);
            return property.serializedObject.FindProperty(propertyPath).FindPropertyRelative(condHAtt.SourceField);
        }
    }

    public static object GetPropertyValue(SerializedProperty prop)
    {
        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                return prop.intValue;
            case SerializedPropertyType.Boolean:
                return prop.boolValue;
            case SerializedPropertyType.Float:
                return prop.floatValue;
            case SerializedPropertyType.String:
                return prop.stringValue;
            case SerializedPropertyType.Color:
                return prop.colorValue;
            case SerializedPropertyType.ObjectReference:
                return prop.objectReferenceValue;
            case SerializedPropertyType.LayerMask:
                return (LayerMask)prop.intValue;
            case SerializedPropertyType.Enum:
                if (prop.enumNames.Length == 0) { return "undefined"; }
                return prop.enumNames[prop.enumValueIndex];
            case SerializedPropertyType.Vector2:
                return prop.vector2Value;
            case SerializedPropertyType.Vector3:
                return prop.vector3Value;
            case SerializedPropertyType.Vector4:
                return prop.vector4Value;
            case SerializedPropertyType.Rect:
                return prop.rectValue;
            case SerializedPropertyType.ArraySize:
                return prop.arraySize;
            case SerializedPropertyType.Character:
                return (char)prop.intValue;
            case SerializedPropertyType.AnimationCurve:
                return prop.animationCurveValue;
            case SerializedPropertyType.Bounds:
                return prop.boundsValue;
            case SerializedPropertyType.Gradient:
                return null;
        }

        return null;
    }
}
#endif