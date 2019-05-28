using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Mock.GameObject<CubeController> gameObject1 = new Mock.GameObject<CubeController>(new CubeController());
        //gameObject1.GetComponent();

        Mock.GameObject gameObject2 = new Mock.GameObject();
        gameObject2.AddComponent<Mock.SpriteRenderer>();

        Mock.SpriteRenderer renderer = gameObject2.GetComponent<Mock.SpriteRenderer>();
        renderer.Name = "Tabors";

        Debug.Log("Component Name: " + gameObject2.component.Name);
    }

}

namespace Mock
{
    class GameObject<T>
    {
        T Component;
        public GameObject(T init)
        {
            Component = init;
        }

        public void GetComponent()
        {
            Debug.Log("Generic GameObject's Component: " + Component.GetType());
        }
    }

    class GameObject
    {
        public Component component;

        public void AddComponent<T>() where T : Component, new()
        {
            component = new T();
            component = (T)component;

            Debug.Log("Added Component of type: " + typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)component;
        }
    }

    class Component { public string Name; }
    class SpriteRenderer : Component { }
}