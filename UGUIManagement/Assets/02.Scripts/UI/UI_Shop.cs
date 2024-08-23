using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Demo.UI
{
    public class UI_Shop : UI_PopupBase
    {
        [Resolve] Button _close;

        protected override void Awake()
        {
            base.Awake();

            _close.onClick.AddListener(Hide);
        }

        public override void OnRightClick(InputAction.CallbackContext context)
        {
            base.OnRightClick(context);

            // Down
            if (context.ReadValueAsButton())
            {
                if (TryCast(Mouse.current.position.ReadValue(), out ShopSlot shopSlot))
                {
                    Debug.Log($"[{name}] : Casted {shopSlot}.");
                    UIManager.instance.Get<UI_ConfirmWindow>()
                                      .Show(message: $"The price is {shopSlot.itemPrice}.\nReally want to purchase?",
                                            onConfirmed: () =>
                                            {
                                                Destroy(shopSlot.gameObject);
                                                UIManager.instance.Get<UI_AlertWindow>()
                                                                  .Show(message: "Purchase Success !");
                                            });
                }
            }
        }
    }
}