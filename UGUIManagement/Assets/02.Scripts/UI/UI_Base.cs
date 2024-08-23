using Demo.Extensions;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Demo.UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class UI_Base : MonoBehaviour, IUI
    {
        public int sortingOrder 
        {
            get => _canvas.sortingOrder;
            set => _canvas.sortingOrder = value;
        }


        private Canvas _canvas;
        private GraphicRaycaster _raycastModule;
        private EventSystem _eventSystem;
        private List<RaycastResult> _raycastBuffer = new List<RaycastResult>(2);


        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _raycastModule = GetComponent<GraphicRaycaster>();
            _eventSystem = EventSystem.current;
            Resolve();
            UIManager.instance.Register(this);
        }

        private void Resolve()
        {
            FieldInfo[] fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            StringBuilder stringBuilder = new StringBuilder(40);

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                ResolveAttribute resolveAttribute = fieldInfos[i].GetCustomAttribute<ResolveAttribute>();

                if (resolveAttribute != null)
                {
                    stringBuilder.Clear();
                    stringBuilder.Append(UGUIResolveNameTable.pairs[fieldInfos[i].FieldType]);
                    string fieldName = fieldInfos[i].Name;
                    bool isFirst = true;

                    for (int j = 0; j < fieldName.Length; j++)
                    {
                        if (isFirst)
                        {
                            if (fieldName[j].Equals('_'))
                                continue;

                            stringBuilder.Append(char.ToUpper(fieldName[j]));
                            isFirst = false;
                        }
                        else
                        {
                            stringBuilder.Append(fieldName[j]);
                        }
                    }

                    Transform result = transform.FindChildWithName(stringBuilder.ToString());

                    if (result != null)
                    {
                        fieldInfos[i].SetValue(this, result.GetComponent(fieldInfos[i].FieldType));
                    }
                    else
                    {
                        Debug.LogError($"[{name}] : Cannot resolve field {fieldInfos[i].Name}.");
                    }
                }
            }
        }

        public virtual void Show()
        {
            _canvas.enabled = true;
        }

        public virtual void Hide()
        {
            _canvas.enabled = false;
        }

        public bool TryCast<T>(Vector2 pointerPosition, out T result)
        {
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = pointerPosition;
            _raycastBuffer.Clear();
            _raycastModule.Raycast(pointerEventData, _raycastBuffer);

            if (_raycastBuffer.Count > 0)
            {
                if (_raycastBuffer[0].gameObject.TryGetComponent(out result))
                {
                    return true;
                }
            }

            result = default(T);
            return false;
        }

        #region InputActions
        public virtual void OnNavigate(InputAction.CallbackContext context)
        {
        }

        public virtual void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public virtual void OnCancel(InputAction.CallbackContext context)
        {
        }

        public virtual void OnPoint(InputAction.CallbackContext context)
        {
        }

        public virtual void OnClick(InputAction.CallbackContext context)
        {
        }

        public virtual void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public virtual void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public virtual void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public virtual void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public virtual void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }
        #endregion
    }
}