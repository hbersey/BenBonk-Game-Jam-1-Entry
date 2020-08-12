using System;
using Item;
using UnityEngine;

namespace World
{
    [CreateAssetMenu(fileName = "Department", menuName = "Pandemic Shopper/Department", order = 1)]
    public class DepartmentScriptableObject : ScriptableObject
    {
        public new string name;
        public ItemScriptableObject[] items;
        public Sprite[] customFloors;
        public bool HasCustomFloors() => customFloors.Length > 0;
    }
}