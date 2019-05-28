using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMainScreenController : MonoBehaviour
{
    public float Speed;

    const float CamHeightY = 3.4f;
    const float CamWithX = 6.2f;

    Vector2 Move;
    Vector2 CursorDirection;
    // Update is called once per frame
    private void Start()
    {
        Move = transform.position;
        Speed = 0;
    }

    void Update()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(transform.position, cursorWorldPosition) < 1f)
        {
            CursorDirection = transform.position - cursorWorldPosition;
            CursorDirection.Normalize();
            Speed = 0.05f * Timer.DeltaTimeMod;
        }

        if (Speed > 0)
            RunAwayFroyCursor(CursorDirection);
        BoundaryCheck();

    }

    void RunAwayFroyCursor(Vector3 CursorDirection)
    {
        transform.Translate(CursorDirection * Speed * Timer.DeltaTimeMod);
        Speed -= 0.0015f * Timer.DeltaTimeMod;
    }

    private void BoundaryCheck()
    {
        Move = transform.position;

        if (transform.position.x < CamWithX * -1)
            Move.x = CamWithX;
        if (transform.position.x > CamWithX * 1)
            Move.x = CamWithX * -1;
        if (transform.position.y < CamHeightY * -1)
            Move.y = CamHeightY;
        if (transform.position.y > CamHeightY * 1)
            Move.y = CamHeightY * -1;

        transform.position = Move;
    }
}
