using UnityEditor;
using UnityEngine;
using OpenYandere.Items;

namespace OpenYandere.Items.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Item), true)]
    public class ItemEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Reset"))
            {
                Item targetItem = (Item)target;

                targetItem.IsEquipped = false;
                targetItem.Amount = 1;
            }
        }
    }
}