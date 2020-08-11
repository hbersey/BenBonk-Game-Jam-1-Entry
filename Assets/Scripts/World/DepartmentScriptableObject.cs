using System;
using UnityEngine;

namespace World
{
    [CreateAssetMenu(fileName = "Department", menuName = "PandemicShopper/Department", order = 1)]
    public class DepartmentScriptableObject : ScriptableObject
    {
        public new string name;
        public ItemScriptableObject[] items;
        public Sprite[] floor;
    }
}