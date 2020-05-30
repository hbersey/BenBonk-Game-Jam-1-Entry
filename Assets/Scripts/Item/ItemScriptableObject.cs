using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemScriptableObject", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        public string id;
        public new string name;
        [FormerlySerializedAs("itemPrefab")] public GameObject pickupPrefab;
        public Sprite toFindSprite;
    }
}