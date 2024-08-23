using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class UI_PlayerStatusInBattleField : UI_Base
    {
        [Resolve] Button _shop;


        protected override void Awake()
        {
            base.Awake();

            _shop.onClick.AddListener(() =>
            {
                UIManager.instance.Get<UI_Shop>()
                                  .Show();
            });
        }
    }
}