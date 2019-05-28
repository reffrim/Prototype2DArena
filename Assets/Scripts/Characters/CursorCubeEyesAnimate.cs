using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCubeEyesAnimate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        LookAtCursor();
    }

    void LookAtCursor()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookPos = cursorWorldPosition - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 145f;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
