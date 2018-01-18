using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OpenYandere.Items;
using UnityEngine.EventSystems;

namespace OpenYandere.UI.Inventory
{
    internal class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private int _slotId;
        private Item _item;
        
        [Header("References:")]
        [SerializeField] private InventoryUserInterface _inventoryUI;
        [SerializeField] private Image _slotIcon;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _amount;
        
        public void Set(int slotId, Item item)
        {
            _slotId = slotId;
            _item = item;
			
            _amount.text = item.Amount > 1 ? item.Amount.ToString() : "";
            _amount.enabled = true;
			
            _slotIcon.sprite = item.Icon;
            _slotIcon.enabled = true;

            _button.interactable = true;
        }
        
        public void OnSlotClicked()
        {
            if (_item == null) return;
            
            Debug.Log($"Clicked on {_item.Name}!");

            // TODO: Show options.
        }
        
        public void Empty()
        {
            if (_item == null) return;
            
            _item = null;
            _slotIcon.enabled = false;
            _amount.enabled = false;
            _button.interactable = false;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _inventoryUI.SetItemDetails(_item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventoryUI.SetItemDetails(null);
        }
    }
}