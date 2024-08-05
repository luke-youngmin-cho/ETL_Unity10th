using Demo.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.UI
{
    /// <summary>
    /// UI 의 활성/비활성 및 정렬 순서를 관리
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        public UIManager()
        {
            _uis = new Dictionary<Type, IUI>();
            _popups = new List<IUI>(16); // Reserve (용량 예약. 사용되는 아이템 갯수를 파악해서 미리 용량 확보)
            _inputActions = new InputActions();
            _inputActions.Enable();
        }


        private const int SCREEN_SORT_ORDER_DEFAULT = -10;
        private Dictionary<Type, IUI> _uis;
        private IUI _screen;
        private List<IUI> _popups;
        private InputActions _inputActions;

        /// <summary>
        /// ui 를 사전에 등록시도. 중복 허용하지않음.
        /// </summary>
        /// <param name="ui"> 등록할 ui </param>
        /// <exception cref="Exception"> 중복된 ui 등록 시도 예외 </exception>
        public void Register(IUI ui)
        {
            if (_uis.TryAdd(ui.GetType(), ui) == false)
                throw new Exception($"[{nameof(UIManager)}] : Failed to register {ui}. already exist.");

            Debug.Log($"[{nameof(UIManager)}] : registered {ui}.");
        }

        /// <summary>
        /// 원하는 타입의 UI 를 가져옴
        /// </summary>
        /// <typeparam name="T"> 가져오고싶은 UI 타입 </typeparam>
        /// <returns> 원하던 UI 객체 </returns>
        /// <exception cref="Exception"> 현재 Hierarchy에 원하는 UI 가 없음 </exception>
        public T Get<T>()
            where T : UnityEngine.Object, IUI
        {
            // ui 를 사전에서 찾아봄
            if (_uis.TryGetValue(typeof(T), out IUI ui))
                return (T)ui;

            // 사전에 없으면 Resources 에서 찾아봄
            string path = $"UI/Canvas - {typeof(T).Name.Substring(3)}"; 
            GameObject prefab = Resources.Load<GameObject>(path);

            // Resources 에서 없으면 예외던짐
            if (prefab == null)
                throw new Exception($"[{nameof(UIManager)}] : Failed to get {typeof(T)}. not exist.");

            return GameObject.Instantiate(prefab).GetComponent<T>();
        }

        /// <summary>
        /// 전체 스크린용 UI를 설정 및 재정렬
        /// </summary>
        /// <param name="ui"> 새로 보여줄 스크린 UI </param>
        public void SetScreen<T>()
            where T : UnityEngine.Object, IUI
        {
            // 기존 스크린 있으면 숨김
            if (_screen != null)
            {
                _inputActions.UI.RemoveCallbacks(_screen);
                _screen.Hide();
            }

            T ui = Get<T>(); // 새로 설정할 스크린 
            ui.sortingOrder = SCREEN_SORT_ORDER_DEFAULT; // 캔버스 정렬
            _inputActions.UI.AddCallbacks(_screen);
            ui.Show(); // 보여줌
        }

        /// <summary>
        /// Popup 스택의 가장 마지막에 추가하고 정렬 순서 재설정
        /// </summary>
        /// <param name="ui"> 새로 띄울 팝업 </param>
        public void Push(IUI ui)
        {
            if (_popups.Count > 0)
            {
                _inputActions.UI.RemoveCallbacks(_popups[_popups.Count - 1]);
            }

            ui.sortingOrder = _popups.Count;
            _popups.Add(ui);
            _inputActions.UI.AddCallbacks(ui);
        }

        /// <summary>
        /// Popup 스택의 마지막에서부터 제거하려는 팝업을 검색해서 제거 및 재정렬
        /// </summary>
        /// <param name="ui"> 제거하려는 팝업 UI </param>
        /// <exception cref="Exception"> 팝업 띄운적도 없는데 제거하려고 시도함 </exception>
        public void Pop(IUI ui)
        {
            int index = _popups.FindLastIndex(x => x == ui);

            if (index < 0)
                throw new Exception($"[{nameof(UIManager)}] : Failed to pop {ui}. Has not been pushed.");

            // 내가 제거하려던 팝업이 제일 최신팝업이었다면
            if (index == _popups.Count - 1)
            {
                _inputActions.UI.RemoveCallbacks(_popups[index]);

                // 내가 제거하려던 팝업 뒤에 다른 팝업이 있다면
                if (index >= 1)
                {
                    _inputActions.UI.AddCallbacks(_popups[index - 1]);
                }
            }

            for (int i = index; i < _popups.Count - 1; i++)
            {
                _popups[i] = _popups[i + 1];
                _popups[i].sortingOrder = i;
            }

            _popups.RemoveAt(_popups.Count - 1);
        }
    }
}
