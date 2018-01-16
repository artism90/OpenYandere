using UnityEngine;

namespace OpenYandere.Items.Types
{
    [CreateAssetMenu(fileName = "Attack Item", menuName = "Items/Attack Item")]
    internal class AttackItem : Item
    {
        public override void OnEquipped()
        {
            // TODO: Activate the "Attack" interactable.
        }
        
        public override void OnUnequipped()
        {
            // TODO: Deactivate the "Attack" interactable.
        }
    }
}