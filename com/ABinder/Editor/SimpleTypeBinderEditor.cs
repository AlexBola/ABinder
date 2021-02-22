using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.ABinder;
using UnityEditor;
using UnityEngine;

namespace com.ABinder.Editor
{
    [CustomEditor(typeof(SimpleTypeBinder), true)]
    public class SimpleTypeBinderEditor : ABinderEditor
    {
        SerializedProperty targetObject;
        SerializedProperty targetProperty;
        SerializedProperty targetIndex;
        SerializedProperty targetHash;

        protected new void OnEnable()
        {
            base.OnEnable();
            // Setup the SerializedProperties.
            targetObject = serializedObject.FindProperty("_target");
            targetProperty = serializedObject.FindProperty("TargetPropertyName");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
             if (GUI.changed)
                return;
            serializedObject.Update();
            targetIndex = serializedObject.FindProperty("TargetCurrentIndex");
            targetHash = serializedObject.FindProperty("TargetCurrentHash");

            var type = serializedObject.targetObject.GetType();
            var attrs = type.GetCustomAttributes(typeof (BindToAttribute), false);
            var bindToAtr = attrs.Length > 0 ? attrs.First() as BindToAttribute : null;

            if (bindToAtr == null)
                throw new Exception(String.Format("Type {0} has no Target property type which to can bind!",
                    GetType().Name));

            Component presetComponent = targetObject.objectReferenceValue as Component;
            if (presetComponent == null)
                return;
            var components = presetComponent.GetComponents<Component>().Where(c =>
            {
                var t = c.GetType();
                return t.GetCustomAttributes(typeof(BindSourceAttribute), false).Length <= 0 &&
                       t.GetCustomAttributes(typeof(BindToAttribute), false).Length <= 0;
            });
            List<KeyValuePair<Component, PropertyInfo>> propertyInfos = components.SelectMany(c => c.GetType().GetProperties().Where(p => p.CanWrite && CurrentSourcePropType == p.PropertyType), (c, info) => new KeyValuePair<Component, PropertyInfo>(c, info)).ToList();

            var propList = propertyInfos.Select(pair => pair.Key.GetType().Name + "-" + pair.Value.Name).ToArray();
            
            var propIndex = propList.IndexOf(n => n.GetHashCode().Equals(targetHash.intValue));
            PropertyInfo selectedTProperty = propIndex < 0 ? null : propertyInfos[propIndex].Value;

            if (selectedTProperty != null)
            {
                targetObject.objectReferenceValue = propertyInfos[propIndex].Key;
                targetProperty.stringValue = selectedTProperty.Name;
            }
            targetIndex.intValue = EditorGUILayout.Popup("Target Property", propIndex, propList);
            if (propList.Length > 0 && targetIndex.intValue > 0)
                targetHash.intValue = propList[targetIndex.intValue].GetHashCode();
            serializedObject.ApplyModifiedProperties();
            if (targetIndex.intValue < 0)
                throw new Exception(String.Format("Type {0} of {1} Target property isn't selected value !", GetType().Name, targetGameObject.GetStringHierarchiPath()));
        }
    }
}
