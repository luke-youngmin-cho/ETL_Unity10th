using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Demo.UI
{
    public class UI_PopupBase : UI_Base
    {
        [SerializeField] bool _hideWhenPointerDownOutside;


        protected override void Awake()
        {
            base.Awake();

            if (_hideWhenPointerDownOutside)
                CreateOutsidePanel();
        }

        public override void Show()
        {
            base.Show();

            UIManager.instance.Push(this);
        }

        public override void Hide()
        {
            base.Hide();

            UIManager.instance.Pop(this);
        }

        private void CreateOutsidePanel()
        {
            GameObject panel = new GameObject("Outside"); // 빈 게임오브젝트 생성
            panel.transform.SetParent(transform); // 현재 캔버스 하위에 종속
            panel.transform.SetAsFirstSibling(); // 첫번째 자식으로 순서 변경 (다른 UI 컴포넌트 가리지 말라고)
            panel.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.6f);

            RectTransform rectTransform = (RectTransform)panel.transform;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.pivot = Vector2.one * .5f;
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = Vector2.zero;

            EventTrigger eventTrigger = panel.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener(eventData => Hide());
            eventTrigger.triggers.Add(entry);
        }
    }
}
