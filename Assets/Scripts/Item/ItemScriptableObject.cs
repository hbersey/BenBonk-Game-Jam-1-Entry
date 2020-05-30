using System;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemScriptableObject", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        public string id;
        public new string name;
        public GameObject itemPrefab;
    }
}