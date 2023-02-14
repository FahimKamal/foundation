using System.Collections;

namespace Pancake
{
    using System;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;
#if UNITY_EDITOR
    using System.Reflection;

#endif


    public static partial class C
    {
        public static T GetOrAddComponent<T>(GameObject gameObject, bool recordUndo = true) where T : Component
        {
            if (gameObject != null)
            {
                var component = gameObject.GetComponent<T>();
                if (component == null) component = AddComponent<T>(gameObject, recordUndo);
                return component;
            }

            return null;
        }

        public static T AddComponent<T>(GameObject gameObject, bool recordUndo = true) where T : Component
        {
            if (gameObject != null)
            {
#if UNITY_EDITOR
                if (Application.isPlaying) return gameObject.AddComponent<T>();
                if (recordUndo) return UnityEditor.Undo.AddComponent<T>(gameObject);
                return gameObject.AddComponent<T>();
#else
                return gameObject.AddComponent<T>();
#endif
            }

            return null;
        }

        /// <summary>
        /// add blank button
        /// </summary>
        /// <param name="target"></param>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static T AddBlankComponent<T>(this GameObject target, Action<T> setup) where T : MonoBehaviour
        {
            var button = target.AddComponent<T>();
            setup?.Invoke(button);
            return button;
        }

        /// <summary>
        /// Get the RectTransform component.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static RectTransform rectTransform(this Component target) { return target.transform as RectTransform; }

        /// <summary>
        /// Delay invoking (scaled time).
        /// </summary>
        public static void Delay(this MonoBehaviour behaviour, float delay, Action action)
        {
            behaviour.StartCoroutine(DelayedCoroutine());

            IEnumerator DelayedCoroutine()
            {
                yield return new WaitForSeconds(delay);
                action();
            }
        }


        /// <summary>
        /// Delay invoking (unscaled time).
        /// </summary>
        public static void DelayRealtime(this MonoBehaviour behaviour, float delay, Action action)
        {
            behaviour.StartCoroutine(DelayedCoroutine());

            IEnumerator DelayedCoroutine()
            {
                yield return new WaitForSecondsRealtime(delay);
                action();
            }
        }

        public static void IfNotNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component != null) action?.Invoke(component);
        }

        public static void IfNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component == null) action?.Invoke(component);
        }

        public static void Enable(this Component component) { Enable(component.gameObject); }

        public static void Enable(this GameObject gameObject) { gameObject.SetActive(true); }

        public static void Disable(this Component component) { Disable(component.gameObject); }

        public static void Disable(this GameObject gameObject) { gameObject.SetActive(false); }

        public static void EnableParent(this Component component)
        {
            var parent = component.transform.parent;

            if (parent != null) parent.Enable();
        }

        public static void DisableParent(this Component component)
        {
            var parent = component.transform.parent;

            if (parent != null) parent.Disable();
        }

        public static bool TryGetParent(this Component component, out Transform parent)
        {
            var transform = component.transform;
            parent = transform.parent;
            return parent != null;
        }

        public static bool TryGetChild(this Component component, out Transform child)
        {
            var transform = component.transform;
            var childCount = transform.childCount;

            child = childCount > 0 ? transform.GetChild(0) : null;

            return childCount > 0;
        }

        public static T GetNearby<T>(this Component component) where T : Component
        {
            T instance = null;

            if (component.transform.parent != null) instance = component.GetComponentInParent<T>();

            if (instance == null) instance = component.GetComponentInChildren<T>();

            if (instance == null) throw new NullReferenceException(typeof(T).Name);

            return instance;
        }

        // Check is component is active and enable
        public static bool IsEnabled(this Behaviour behaviour)
        {
            return behaviour != null && behaviour.isActiveAndEnabled;
        }
        
        /// <summary>
        /// add blank button
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        internal static UnityEngine.UI.Button AddBlankButtonComponent(this GameObject target)
        {
            var button = target.AddComponent<UnityEngine.UI.Button>();
            button.transition = UnityEngine.UI.Selectable.Transition.None;
            return button;
        }

        #region field info

#if UNITY_EDITOR
        public static readonly Dictionary<int, List<FieldInfo>> FieldInfoList = new Dictionary<int, List<FieldInfo>>();

        public static int GetFieldInfo(UnityEngine.Object target, out List<FieldInfo> fieldInfoList)
        {
            Type targetType = target.GetType();
            int targetTypeHashCode = targetType.GetHashCode();

            if (!FieldInfoList.TryGetValue(targetTypeHashCode, out fieldInfoList))
            {
                IList<Type> typeTree = targetType.GetBaseTypes();
                fieldInfoList = target.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.NonPublic)
                    .OrderByDescending(x => typeTree.IndexOf(x.DeclaringType))
                    .ToList();
                FieldInfoList.Add(targetTypeHashCode, fieldInfoList);
            }

            return fieldInfoList.Count;
        }
#endif
        public static IList<Type> GetBaseTypes(this Type t)
        {
            var types = new List<Type>();
            while (t.BaseType != null)
            {
                types.Add(t);
                t = t.BaseType;
            }

            return types;
        }

        #endregion

        #region interface

        public struct ComponentOfInterface<T>
        {
            public readonly Component component;
            public readonly T @interface;

            public ComponentOfInterface(Component component, T @interface)
            {
                this.component = component;
                this.@interface = @interface;
            }
        }

        /// <summary>
        /// Find all Components of specified interface
        /// </summary>
        public static T[] FindObjectsOfInterface<T>() where T : class
        {
            var monoBehaviours = UnityEngine.Object.FindObjectsOfType<Transform>();

            return monoBehaviours.Select(behaviour => behaviour.GetComponent(typeof(T))).OfType<T>().ToArray();
        }

        /// <summary>
        /// Find all Components of specified interface along with Component itself
        /// </summary>
        public static ComponentOfInterface<T>[] FindObjectsOfInterfaceAsComponents<T>() where T : class
        {
            return UnityEngine.Object.FindObjectsOfType<Component>().Where(c => c is T).Select(c => new ComponentOfInterface<T>(c, c as T)).ToArray();
        }

        #endregion
    }
}