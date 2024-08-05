using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class ShopSlot : MonoBehaviour
    {
        public int itemPrice { get; private set; }
        [SerializeField] Image _itemIcon;
        [SerializeField] TMP_Text _itemPrice;


        private void Awake()
        {
            itemPrice = Random.Range(100, 50000);
            _itemPrice.text = itemPrice.ToString();
        }
    }
}