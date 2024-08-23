using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Demo.UI
{
    public static class UGUIResolveNameTable
    {
        public static Dictionary<Type, string> pairs = new Dictionary<Type, string>
        {
            { typeof(Button), "Button - " },
            { typeof(Image), "Image - " },
            { typeof(TextMeshProUGUI), "Text (TMP) - " },
            { typeof(TMP_Text), "Text (TMP) - " },
            { typeof(TMP_InputField), "InputField (TMP) - " },
        };
    }
}
