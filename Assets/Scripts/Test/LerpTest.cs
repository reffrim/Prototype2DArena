using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    float Accumulator = 0.001f;
    float Output = 0f;
    float RateSlowingDown = 0.08f;

    private void Start()
    {
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Images/Tabors");
        renderer.sortingLayerName = "Player1";
        renderer.color = new Color(0, 1, 0.3f, 1); //0.3f = 30% from the byte number that assigns to the color.
    } 

    private void Update()
    {
        Vector3 temp = new Vector3(LerpTypes(), 1, 1); //
        transform.position = temp;
    }

    float LerpTypes()
    {
        //Accumulator Type1: Linear, fixed accumulator. 
        //Accumulator += 0.004f;

        //Accumulator Type2: Exponential, doubling accumulator. 
        Accumulator += Accumulator * RateSlowingDown;

        Output = Mathf.Lerp(-5, 5f, Accumulator);

        return Output;
    }

    private void OnGUI()
    {
        GUI.contentColor = Color.magenta;

        GUI.Label(
            new Rect(105, 205, Screen.width, Screen.height),
            string.Format("<b>LERP Accumulator: {0}</b>", Accumulator.ToString())
            );

        GUI.Label(
            new Rect(105, 270, Screen.width, Screen.height),
            string.Format("<b>LERP Output: {0}</b>", Output.ToString())
            );
    }
}
