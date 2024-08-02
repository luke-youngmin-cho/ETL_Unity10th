using Demo.Singleton;
using System;
using System.Collections.Generic;

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
            _popups = new List<IUI>(16);
        }


        private const int SCREEN_SORT_ORDER_DEFAULT = -10;
        private Dictionary<Type, IUI> _uis;
        private IUI _screen;
        private List<IUI> _popups;


        /// <summary>
        /// ui 를 사전에 등록시도. 중복 허용하지않음.
        /// </summary>
        /// <param name="ui"> 등록할 ui </param>
        /// <exception cref="Exception"> 중복된 ui 등록 시도 예외 </exception>
        public void Register(IUI ui)
        {
            if (_uis.TryAdd(ui.GetType(), ui) == false)
                throw new Exception($"[{nameof(UIManager)}] : Failed to register {ui}. already exist.");
        }

        /// <summary>
        /// 원하는 타입의 UI 를 가져옴
        /// </summary>
        /// <typeparam name="T"> 가져오고싶은 UI 타입 </typeparam>
        /// <returns> 원하던 UI 객체 </returns>
        /// <exception cref="Exception"> 현재 Hierarchy에 원하는 UI 가 없음 </exception>
        public T Get<T>()
            where T : IUI
        {
            if (_uis.TryGetValue(typeof(T), out IUI ui))
                return (T)ui;

            // todo -> 사전에 없는경우, Resources 등의 폴더에서 해당 UI 프리팹을 로드해서라도 반환해주는 로직 작성
            throw new Exception($"[{nameof(UIManager)}] : Failed to get {typeof(T)}. not exist.");
        }

        /// <summary>
        /// 전체 스크린용 UI를 설정 및 재정렬
        /// </summary>
        /// <param name="ui"> 새로 보여줄 스크린 UI </param>
        public void SetScreen(IUI ui)
        {
            if (_screen != null)
            {
                _screen.Hide();
            }

            ui.sortingOrder = SCREEN_SORT_ORDER_DEFAULT;
            ui.Show();
        }

        /// <summary>
        /// Popup 스택의 가장 마지막에 추가하고 정렬 순서 재설정
        /// </summary>
        /// <param name="ui"> 새로 띄울 팝업 </param>
        public void Push(IUI ui)
        {
            ui.sortingOrder = _popups.Count;
            _popups.Add(ui);
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

            for (int i = index; i < _popups.Count - 1; i++)
            {
                _popups[i] = _popups[i + 1];
                _popups[i].sortingOrder = i;
            }

            _popups.RemoveAt(_popups.Count - 1);
        }
    }
}
