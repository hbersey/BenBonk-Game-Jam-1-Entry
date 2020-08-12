using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "Pandemic Shopper/Item", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        public new string name;
        public GameObject prefab;
    }
}