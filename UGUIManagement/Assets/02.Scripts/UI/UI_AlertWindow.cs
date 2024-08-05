using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Demo.UI
{
    public class UI_AlertWindow : UI_PopupBase
    {
        [SerializeField] TMP_Text _message;
        [SerializeField] Button _confirm;

        public void Show(string message, UnityAction onConfirmed = null)
        {
            base.Show();

            _message.text = message;
            _confirm.onClick.RemoveAllListeners();

            _confirm.onClick.AddListener(Hide);

            if (onConfirmed != null) 
                _confirm.onClick.AddListener(onConfirmed);
        }
    }
}