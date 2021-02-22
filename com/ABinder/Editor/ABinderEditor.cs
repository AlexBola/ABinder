using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.ABinder;
using UnityEngine;
using UnityEditor;

namespace com.ABinder.Editor
{
    [CustomEditor(typeof(ABinder), true)]
    public class ABinderEditor : UnityEditor.Editor
    {
        SerializedProperty sourceObject;
        SerializedProperty sourceProperty;
        SerializedProperty index;
        SerializedProperty hash;

        protected Type currentPropType;
        protected GameObject targetGameObject;

        protected void OnEnable()
        {
            // Setup the SerializedProperties.
            sourceObject = serializedObject.FindProperty("_source");
            sourceProperty = serializedObject.FindProperty("PropertyName");
            targetGameObject = ((ABinder) target).gameObject;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUI.changed)
                return;
            
            serializedObject.Update();
            index = serializedObject.FindProperty("CurrentIndex");
            hash = serializedObject.FindProperty("CurrentHash");
            var type = serializedObject.targetObject.GetType();
            var attrs = type.GetCustomAttributes(typeof (BindToAttribute), false);
            var bindToAtr = attrs.First() as BindToAttribute;
            if (bindToAtr == null)
                throw new Exception(String.Format("Type {0} has no Target property type which to can bind!", GetType().Name));
            MonoBehaviour sourceComponent = sourceObject.objectReferenceValue as MonoBehaviour;
            if (sourceComponent == null)
                return;
            //Debug.Log("ABinderEditor.OnInspectorGUI sourceProp: " + sourceObject + " _selected: " + index.intValue);
            Component[] components = sourceComponent.GetComponents<Component>();
            string[] resultProps = new string[]{};
            var propIndex = -1;
            //PropertyInfo selectedPropInfo = null;
            foreach (Component c in components)
            {
                Type t = c.GetType();
                if (t.GetCustomAttributes(typeof(BindSourceAttribute), false).Length < 1)
                    continue;
                PropertyInfo[] bindableProps = t.GetProperties().Where(p => p.GetCustomAttributes(typeof(BindableAttribute), false).Length > 0 &&
                                                                                       bindToAtr.BindToTypes.Contains(p.PropertyType)).ToArray();
                resultProps = bindableProps.Select(p => c.GetType().Name + "-" + p.Name).ToArray();
                // Attempt to get selected property by Hash.
                propIndex = resultProps.IndexOf(n => n.GetHashCode().Equals(hash.intValue));
                PropertyInfo selectedProperty = propIndex < 0 ? null : bindableProps[propIndex];
                if (propIndex < 0 && index.intValue > -1 && bindableProps.Count() > index.intValue)
                {
                    Debug.LogWarningFormat(string.Format("[{0}/{1}.{2}] Source property of {3} is not found!\n Next attempt to get it by index.",
                        sourceComponent.gameObject.GetStringHierarchiPath(), serializedObject.targetObject.name,
                        type.Name, sourceObject.objectReferenceValue.name));
                    // Get selected property by index.
                    selectedProperty = bindableProps[index.intValue];
                    propIndex = index.intValue;
                }
                if (selectedProperty != null)
                {
                    sourceObject.objectReferenceValue = c;
                    sourceProperty.stringValue = selectedProperty.Name;
                    currentPropType = selectedProperty.PropertyType;
                }
            }
            index.intValue = EditorGUILayout.Popup("Property", propIndex, resultProps);
            if (resultProps.Length > 0 && index.intValue >= 0)
                hash.intValue = resultProps[index.intValue].GetHashCode();
            serializedObject.ApplyModifiedProperties();
            if (index.intValue < 0)
                throw new Exception(String.Format("Type {0} of {1} Source property isn't selected!", GetType().Name, targetGameObject.GetStringHierarchiPath()));
        }

        protected Type CurrentSourcePropType
        {
            get { return currentPropType; }
        }
    }
}
