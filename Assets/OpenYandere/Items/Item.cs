using UnityEngine;

namespace OpenYandere.Items
{
    public abstract class Item : ScriptableObject
    {
        [Header("Settings:")]
        public int Id;
        public string Name;
        public Sprite Icon;
        public ItemType Type;
        [Multiline(3)] public string Description;
        public MeshRenderer Mesh;
        
        [Header("Bone Settings:")]
        public Vector3 BonePosition;
        public Vector3 BoneScale;
        
        [HideInInspector] public bool IsEquipped;
        [HideInInspector] public int Amount = 1;
        
        public abstract void OnEquipped();
        public abstract void OnUnequipped();
    }
}