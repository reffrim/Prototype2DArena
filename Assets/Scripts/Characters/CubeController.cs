using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public static float Speed = 0.07f;
    public static Timer TeleportCool;

    public bool IsDisappear; 

    public const float CamHeightY = 3.4f;
    public const float CamWithX = 6.2f;

    Vector3 Move;
    float Teleport;

    // Start is called before the first frame update
    void Start()
    {
        Move = transform.position;
        IsDisappear = false;

        TeleportCool = new Timer(0);
    }

    // Update is called once per frame
    void Update()
    {
        TeleportCheck();
        Movement();
        BoundaryCheck();
    }

    private void TeleportCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Teleport")) //|| Input.GetButtonDown("Teleport")
        {
            if (TeleportCool.Counter == 0)
            {
                Teleport = 2f;
                IsDisappear = true;
                TeleportCool.Counter = 500;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            Teleport = 0;
            IsDisappear = false;
        }

        TeleportCool.RunReverse();
    }

    private void Movement()
    {
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("DPAD_Horizontal") < 0) 
        {
            left = true;
            Move.x -= Speed * Timer.DeltaTimeMod + Teleport;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("DPAD_Horizontal") > 0)
        {
            right = true;
            Move.x += Speed * Timer.DeltaTimeMod + Teleport;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") < 0 || Input.GetAxis("DPAD_Vertical") > 0)
        {
            up = true;
            Move.y += Speed * Timer.DeltaTimeMod + Teleport;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") > 0 || Input.GetAxis("DPAD_Vertical") < 0)
        {
            down = true;
            Move.y -= Speed * Timer.DeltaTimeMod + Teleport;
        }

        if (left && up)
        {
            Move.x += Speed * Timer.DeltaTimeMod * 0.25f;
            Move.y -= Speed * Timer.DeltaTimeMod * 0.25f;
        }
        if (right && up)
        {
            Move.x -= Speed * Timer.DeltaTimeMod * 0.25f;
            Move.y -= Speed * Timer.DeltaTimeMod * 0.25f;
        }
        if (left && down)
        {
            Move.x += Speed * Timer.DeltaTimeMod* 0.25f;
            Move.y += Speed * Timer.DeltaTimeMod* 0.25f;
        }
        if (right && down)
        {
            Move.x -= Speed * Timer.DeltaTimeMod* 0.25f;
            Move.y += Speed * Timer.DeltaTimeMod* 0.25f;
        }

        transform.position = Move;
    }

    private void BoundaryCheck()
    {
        //Check at theedge of the screen and constrain movement if reached
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
