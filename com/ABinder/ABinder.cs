using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Bgm.Utils;
using UnityEngine;
using Come2Play.UnityLogger;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace com.ABinder
{
    [ExecuteInEditMode]
    public abstract class ABinder : MonoBehaviour
    {
        [SerializeField] protected MonoBehaviour _source;
        [HideInInspector] public int CurrentIndex = -1;
        [HideInInspector] public int CurrentHash = 0;
        [HideInInspector] public string PropertyName = "";

        private static Logger<ABinder> _logger = new Logger<ABinder>();

        private bool _hasBeenInited;
        private IBindSource _currentSource;

        private void Start()
        {
            Init();
            OnValidate();
            OnSourcePropertyChange(null, BindEventArgs.Empty);
        }

        protected Type[] SourcePropertyType
        {
            get
            {
                var atr = GetType().GetCustomAttributes(typeof(BindToAttribute), true).FirstOrDefault() as BindToAttribute;
                if (atr == null)
                {
                    throw new Exception(string.Format("Type {0} has no Target property type which to can bind!",
                        GetType().Name));
                }
                return atr.BindToTypes;
            }
        }

        protected virtual void Init()
        {
            _hasBeenInited = true;
        }

        protected virtual void Bind()
        {
            CheckSettings();
        }

        void OnValidate()
        {
            CheckSettings();
            if (ReferenceEquals(_currentSource, _source))
            {
                return;
            }
            if (_currentSource != null)
            {
                _currentSource.PropertyChanged -= OnSourcePropertyChange;
            }

            _currentSource = _source as IBindSource;

            if (_currentSource != null)
            {
                _currentSource.PropertyChanged += OnSourcePropertyChange;
            }
        }

        void OnEnable()
        {
            CheckSettings();
        }

        protected virtual void CheckSettings()
        {
            if (_source == null)
            {
                throw new Exception(string.Format("{0}.{1} Source reference is not specified.", gameObject.GetStringHierarchiPath(), this.GetType().Name));
            }
            if (string.IsNullOrEmpty(PropertyName))
            {
                throw new Exception(string.Format("{0}.{1} Source property is not specified.", gameObject.GetStringHierarchiPath(), this.GetType().Name));
            }
        }

        private void OnSourcePropertyChange(object sender, BindEventArgs e)
        {
            if (!_hasBeenInited || !IsSourceReady)
            {
                return;
            }
            if (string.IsNullOrEmpty(e.PropertyName))
            {
                Bind();
                return;
            }
            if (e.PropertyName != PropertyName)
            {
                return;
            }
            Bind();
        }

        protected object GetSourceValue()
        {
            if (_source == null)
            {
                throw new Exception(string.Format("Source reference shouldn't be null! GO: '{0}'. Property: '{1}'", 
                    gameObject.name,
                    PropertyName));
            }
            if (!_currentSource.ReadyToBind)
            {
                throw new Exception(string.Format("Source isn't ready to bind! GO: '{0}'; Source: '{1}'; Property: '{2}'", 
                    gameObject.name, 
                    _source.name,
                    PropertyName));
            }

            var propertyInfo = _source.GetType().GetProperty(PropertyName);
            if (propertyInfo == null)
            {
                throw new Exception(string.Format("Source property is missing: '{0}'. GO: '{1}'",
                    PropertyName,
                    gameObject.name));
            }
            
            return propertyInfo.GetValue(_source, null);
        }

        protected bool IsSourceReady
        {
            get
            {
                return _currentSource != null && _currentSource.ReadyToBind;
            }
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/ABinder/Rebind")]
        private static void NewOpenForRigidBody(MenuCommand command)
        {
            ABinder binder = (ABinder) command.context;
            _logger.Debug("binder: {0}", binder);
            MethodInfo dynMethod = binder.GetType().GetMethod("Bind", BindingFlags.NonPublic | BindingFlags.Instance);
            dynMethod.Invoke(binder, new object[0]);
        }
#endif
    }
}
