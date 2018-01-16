using System;
using UnityEngine;
using System.Linq;
using OpenYandere.Items;
using System.Collections.Generic;

namespace OpenYandere.Characters.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private readonly Item[] _equippedItems = new Item[1];
        private readonly MeshRenderer[] _equippedItemMeshes = new MeshRenderer[1];
        
        private readonly Dictionary<int, Item> _inventoryDictionary = new Dictionary<int, Item>();
        
        public Action OnInventoryChanged;
        
        [Header("References:")]
        [SerializeField] private Transform _weaponBone;
        
        [Header("Settings:")]
        [SerializeField] private int _inventorySize = 5; // TODO: Determine what types of items will exist other than weapons.
        
        public Item this[int index] => _inventoryDictionary.ElementAt(index).Value;

        public int Count
        {
            get
            {
                int count = 0;
                for (int i = 0; i < _inventoryDictionary.Count; i++) count += this[i].Amount;
                return count;
            }
        }
        
        public bool Contains(Item item)
        {
            return _inventoryDictionary.ContainsKey(item.Id);
        }
        
        public bool Add(Item item)
        {
            // Check the limit has not been reached.
            if (Count == _inventorySize) return false;
            
            // Add the item.
            if (!_inventoryDictionary.ContainsKey(item.Id))
            {
                _inventoryDictionary.Add(item.Id, item);
            }
            else
            {
                _inventoryDictionary[item.Id].Amount++;
            }
            
            // Call the callback method.
            OnInventoryChanged?.Invoke();

            return true;
        }
        
        public void Remove(Item item)
        {
            // Check that item exists.
            if (!_inventoryDictionary.ContainsKey(item.Id)) return;
            
            // If we have more than one of this item.
            if (item.Amount > 1)
            {
                // Decrease the amount.
                item.Amount--;
            }
            // Otherwise, remove the item from the inventory.
            else
            {
                _inventoryDictionary.Remove(item.Id);
            }
            
            // Call the callback method.
            OnInventoryChanged?.Invoke();
        }
        
        public void Equip(Item item)
        {
            int itemType = (int)item.Type;
            
            // Unequip the item of the same type.
            if (_equippedItems[itemType] != null)
            {
                Unequip(_equippedItems[itemType]);
            }
            
            // Instantiate the item mesh.
            MeshRenderer itemMesh = Instantiate(item.Mesh);
            
            // Set the position and scale of the weapon bone.
            _weaponBone.transform.localPosition = item.BonePosition;
            _weaponBone.transform.localScale = item.BoneScale;
            
            // Parent the item mesh to the weapon bone.
            itemMesh.transform.parent = _weaponBone;
            
            // Reset the item mesh's transforms as they are now relative to the parent.
            itemMesh.transform.localPosition = Vector3.zero;
            itemMesh.transform.localRotation = Quaternion.identity;
            
            // Update the equipped item array.
            _equippedItems[itemType] = item;
            _equippedItemMeshes[itemType] = itemMesh;
            
            // Mark the item as equipped.
            item.IsEquipped = true;
            
            // Call the callback methods.
            item.OnEquipped();
            OnInventoryChanged?.Invoke();
        }
        
        public void Unequip(Item item)
        {
            int itemType = (int)item.Type;
            
            // Remove the mesh from the player.
            if (_equippedItemMeshes[itemType] != null)
            {
                Destroy(_equippedItemMeshes[itemType]);
            }
            
            // Update the equipped item array.
            _equippedItems[itemType] = null;
            
            // Unmark the item as equipped.
            item.IsEquipped = false;
            
            // Call the callback methods
            item.OnUnequipped();

            OnInventoryChanged?.Invoke();
        }

        public void Drop(Item item)
        {
            // TODO
        }
    }
}