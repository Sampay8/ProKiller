using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WindowManager
    {
        private const string PrefabsFilePath = "Dialogs/";

        // ѕри создании новых окон добавл€ть их сюда
        private static readonly Dictionary<Type, string> PrefabsDictionary = new Dictionary<Type, string>()
        {
            {typeof(PausaDialog),"PausaDialog"},
            {typeof(LevelInfoDialog),"LevelInfoDialog"},
            {typeof(BrifingDialog),"BrifingDialog"},
            {typeof(ShopDialog),"ShopDialog"},


        };

        private static Transform _GUIHolder =>  GameRoot.GUI; 

        public static void ShowWindow<T>() where T : Window
        {
            CreateWindow<T>();
        }

        public static T GetWindow<T>() where T : Window
        {
            var obj = CreateWindow<T>();
            var component = obj.GetComponent<T>();

            return component;
        }

        private static GameObject CreateWindow<T>() where T : Window
        {
            var go = GetPrefabByType<T>();
            if (go == null)
            {
                Debug.LogError("Show window - object not found");
                return null;
            }

            return GameObject.Instantiate(go, _GUIHolder);
        }

        private static GameObject GetPrefabByType<T>() where T : Window
        {
            var prefabName = PrefabsDictionary[typeof(T)];
            if (string.IsNullOrEmpty(prefabName))
            {
                Debug.LogError("cant find prefab type of " + typeof(T) + "Do you added it in PrefabsDictionary?");
            }

            var path = PrefabsFilePath + PrefabsDictionary[typeof(T)];
            var windowGO = Resources.Load<GameObject>(path);
            if (windowGO == null)
            {
                Debug.LogError("Cant find prefab at path " + path);
            }

            return windowGO;
        }

    }
}