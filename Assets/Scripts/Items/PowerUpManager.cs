using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static Timer PowerUpMeter;

    public GameObject PowerUpPrefab;
    public bool IsPowered;

    Timer SpawnTimer;

    void Start()
    {
        IsPowered = false;
        SpawnTimer = new Timer(0);
        PowerUpMeter = new Timer(50);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Cube") != null)
        {
            if (SpawnTimer.Counter > 300)
            {
                Vector3 randomSpawn = new Vector3(
                Random.Range(-6.2f, 6.2f),
                Random.Range(-3.4f, 3.4f),
                transform.position.z
                );

                transform.position = randomSpawn;

                GameObject gO = GameObject.Instantiate(PowerUpPrefab, randomSpawn, transform.rotation);
                gO.transform.parent = gameObject.transform;
            } 
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("PowerUp")) && !IsPowered && PowerUpMeter.Counter > 0) // || Input.GetButtonDown("PowerUp")
            TimeChange(0.5f);
        else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("PowerUp")) && IsPowered) // || Input.GetButtonDown("PowerUp")
            TimeChange(2f);

        if (IsPowered)
        {
            PowerUpMeter.RunReverse();
            if (PowerUpMeter.Counter == 0)
                TimeChange(2f); 
        }
        SpawnTimer.RunForwardTo(300);
    }
     
    private void TimeChange(float speedFactor)
    {
        CubeController.Speed = CubeController.Speed * speedFactor;
        SphereController.Speed = SphereController.Speed * speedFactor;
        IsPowered = !IsPowered;
    }
}
