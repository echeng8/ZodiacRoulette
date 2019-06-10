using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Ever-growing list of useful extensions
/// - Evan Cheng
/// </summary>
static public class MethodExtensions
{
    /// <summary>
    /// Returns the component of Type type. If one doesn't already exist on the GameObject it will be added.
    /// </summary>
    /// <typeparam name="T">The type of Component to return.</typeparam>
    /// <param name="gameObject">The GameObject this Component is attached to.</param>
    /// <returns>Component</returns>
    static public T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Enables the child at the given index while disabling the others.  
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="childIndex"></param>
    static public void ToggleChildren(this Transform transform, int childIndex)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(childIndex).gameObject.SetActive(true);
    }

    /// <summary>
    /// Clears the text in the input field and makes it interactable.
    /// </summary>
    /// <param name="inputField"></param>
    static public void ResetField(this InputField inputField)
    {
        inputField.text = "";
        inputField.interactable = true;
    }

    /// <summary>
    /// Returns a random point in the given bounds. *Ignores rotation  
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    static public Vector3 RandomPointInBounds(this Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }


    /// <summary>
    /// Randomizes an IEnumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <returns></returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var r = new System.Random();
        return enumerable.OrderBy(x => r.Next()).ToList();
    }

    /// <summary>
    /// Allows an array of size 2 to be deconstructed like a tuple. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="t0"></param>
    /// <param name="t1"></param>
    public static void Destructure<T>(this T[] items, out T t0, out T t1)
    {
        t0 = items.Length > 0 ? items[0] : default(T);
        t1 = items.Length > 1 ? items[1] : default(T);
    }
}