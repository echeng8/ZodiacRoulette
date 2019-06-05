using UnityEngine;
using UnityEngine.UI; 

static public class UnityEngineExtensions
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
}