using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimate : MonoBehaviour
{
    SpriteRenderer[] cubeRenderers; //Get all the child's sprite renderers. 

    CubeController controller;

    private void Start()
    {
        cubeRenderers = cubeRenderers = GetComponentsInChildren<SpriteRenderer>();
        controller = GetComponent<CubeController>();
    }

    private void Update()
    {
        TeleportFade();
    }

    void TeleportFade()
    {
        //SpriteRenderer[] cubeRenderers = GetComponentsInChildren<SpriteRenderer>(); //Get all the child's sprite renderers. 

        //CubeController controller = GetComponent<CubeController>();

        for (int i = 0; i < cubeRenderers.Length; i++)
        {
            Color color = cubeRenderers[i].color;

            if (controller.IsDisappear)  //Get the script attached to this parcticular instance. 
                color.a = 0;
            else
                color.a = Mathf.Lerp(color.a, 1f, 0.029f * Timer.DeltaTimeMod);

            cubeRenderers[i].color = color;
        }
    }

    private void CubeFade()
    {
        SpriteRenderer cubeRenderer = GetComponent<SpriteRenderer>();
        Color color = cubeRenderer.color;


        if (GetComponent<CubeController>().IsDisappear)  //Get the script attached to this parcticular instance. 
            color.a = 0;
        else
            color.a = Mathf.Lerp(color.a, 1f, 0.029f);

        cubeRenderer.color = color;
    }
}
