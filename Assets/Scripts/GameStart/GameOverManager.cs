using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static bool IsGameOVer;

    public float leftBorderAmendment;
    public float rightBorderAmendment;
    public float downBorderAmendment;
    public float upBorderAmendment;

    SpriteRenderer GameOverRenderer;
    Vector2 RandomDirection;

    void Start()
    {
        GameOverRenderer = GetComponent<SpriteRenderer>();
        IsGameOVer = false;

        RandomDirection.x = Random.Range(CubeController.CamWithX * -1, CubeController.CamWithX);
        RandomDirection.y = Random.Range(CubeController.CamHeightY * -1, CubeController.CamHeightY);
        RandomDirection.Normalize();

        leftBorderAmendment = 1.55f;
        rightBorderAmendment = 1.55f;
        downBorderAmendment = 0.95f;
        upBorderAmendment = 0.95f;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGameOVer)
        {
            GameOverRenderer.color = Color.Lerp(GameOverRenderer.color, new Color(1, 1, 1, 0.8f), 0.06f * Timer.DeltaTimeMod);

            GameStartManager restart = GameObject.Find("Restart").GetComponent<GameStartManager>();
            restart.enabled = true;

            if (GameOverRenderer.color.a > 0.799999f)
                GameOverRenderer.transform.Translate(RandomDirection * 0.05f);
            BoundaryCheck();
        }
    }

    void BoundaryCheck()
    {
        if (transform.position.y > CubeController.CamHeightY - upBorderAmendment || transform.position.y < CubeController.CamHeightY * -1 + downBorderAmendment)
            RandomDirection.y *= -1;
        if (transform.position.x > CubeController.CamWithX - rightBorderAmendment || transform.position.x < CubeController.CamWithX * -1 + leftBorderAmendment)
            RandomDirection.x *= -1;
    }
}
