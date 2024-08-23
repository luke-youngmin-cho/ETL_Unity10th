using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Demo.UI
{
    public class UI_ConfirmWindow : UI_PopupBase
    {
        [Resolve] TMP_Text _message;
        [Resolve] Button _confirm;
        [Resolve] Button _cancel;

        public void Show(string message, UnityAction onConfirmed = null, UnityAction onCanceled = null)
        {
            base.Show();

            _message.text = message;
            _confirm.onClick.RemoveAllListeners();

            _confirm.onClick.AddListener(Hide);

            if (onConfirmed != null) 
                _confirm.onClick.AddListener(onConfirmed);

            _cancel.onClick.RemoveAllListeners();
            
            _cancel.onClick.AddListener(Hide);

            if (onCanceled != null)
                _cancel.onClick.AddListener(onCanceled);
        }
    }
}