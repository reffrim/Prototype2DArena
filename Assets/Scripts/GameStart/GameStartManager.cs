using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    Timer _loopTimer;
    Timer LoopTimer
    {
        get //LerpTimer will never be more that 3000 during reading. 
        {
            _loopTimer.RunForwardTo(30);
            return _loopTimer;
        }
        set
        {
            _loopTimer = value;
        }
    }


    void Start()
    {
        LoopTimer = new Timer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (LoopTimer.Counter == 0)
        {
            SpriteRenderer pressEnterRenderer = GetComponent<SpriteRenderer>();
            pressEnterRenderer.enabled = !pressEnterRenderer.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Teleport"))
        {
            SceneManager.LoadScene("ProtoScene");
            CubeController.Speed = 0.07f;
            SphereController.Speed = 0.06f;
        }

    }
}
