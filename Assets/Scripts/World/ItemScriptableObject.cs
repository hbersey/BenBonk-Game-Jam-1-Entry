using UnityEngine;

namespace World
{
    [CreateAssetMenu(fileName = "Item", menuName = "PandemicShopper/Item", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        public new string name;
        public Sprite sprite;
    }
}