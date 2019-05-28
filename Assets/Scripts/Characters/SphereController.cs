using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public static float Speed = 0.06f;
    public static bool IsCubeEaten;

    public float CloseCall;
    public GameObject CubeReference;
     
    int TimeToStand;
    Vector3 CubeReferencePosition;
    Vector3 RandomPoint;
    Vector2 Distance;
    AudioSource Audio;
    Timer CoolCrowd;
    Timer StandingTimer;

    // Start is called before the first frame update
    void Start()
    {
        CubeReference = GameObject.Find("Cube");
        StandingTimer = new Timer(0);
        TimeToStand = Random.Range(100, 350);
        float x = Random.Range(-6.2f, 6.2f);
        float y = Random.Range(-3.4f, 3.4f);
        RandomPoint = new Vector3(x, y, 0);
        IsCubeEaten = false;

        Audio = GetComponent<AudioSource>();
        CoolCrowd = new Timer(220) ;

        CloseCall = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeReference.transform.localScale.y < 0.011f)
        {
            //GetComponent<SphereController>().enabled = false;
            PointlessStranging();
        }
        else
        {
            Movement();
            EnemyProximity();
            //SpawnEnemy(); // Legacy
            CrowdCheer();
        }
        
    }

    private void Movement()
    {
        if (CubeReference != null) // Unnecesary, just to keep in mind
        {
            // Sphere Tracks position of player1 and follows him
            CubeReferencePosition = CubeReference.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, CubeReferencePosition, Speed * Timer.DeltaTimeMod);
        }
    }

    //private void SpawnEnemy()
    //{
    //    //Spawn a new sphere at random position every 500 frames. 
    //    if (SpawnTimer.Counter > SpawnIndterval)
    //    {
    //        float x;
    //        float y;
    //        float z;

    //       Vector2 distance;
    //        do
    //        {
    //            x = Random.Range(-6.2f, 6.2f);
    //            y = Random.Range(-3.4f, 3.4f);
    //            z = transform.position.z;
    //            distance.x = x - CubeReferencePosition.x;
    //            distance.y = y - CubeReferencePosition.y;
    //        }
    //        while (((distance.x < 3) && (distance.x > -3)) && ((distance.y < 3) && (distance.y > -3)));
    //        GameObject.Instantiate(this.gameObject, new Vector3(x, y, z), transform.rotation);

    //        WaveCount++;
    //    }
    //    SpawnTimer.RunForwardTo(SpawnInterval);
    //}

    private void EnemyProximity()
    {
        //Check Distance between player1 & Enemy
        Distance.x = transform.position.x - CubeReferencePosition.x;
        Distance.y = transform.position.y - CubeReferencePosition.y;

        //Destroy the Cube if the Distance is short. 
        if (((Distance.x < 0.1f) && (Distance.x > -0.1f)) && ((Distance.y < 0.1f) && (Distance.y > -0.1f)))
        {
            ////Removed to fix null reference in final build
            //GameObject.Destroy(CubeReference);

            //Disable CubeController Script, can't move.
            CubeReference.GetComponent<CubeController>().enabled = false;

            //Cube gets "swallowed up" by the sphere
            CubeReference.transform.localScale = Vector3.Lerp(CubeReference.transform.localScale, new Vector3(0.01f, 0.01f, 0.01f), 0.05f * Timer.DeltaTimeMod);

            //Cube Spins as it's getting swallowed
            CubeReference.transform.rotation = new Quaternion(0, 0, transform.rotation.z * 0.5f, transform.rotation.w);

            IsCubeEaten = true;
            GameOverManager.IsGameOVer = true;

            if (CubeReference.transform.localScale.y < 0.012f)
                CubeReference.GetComponent<SpriteRenderer>().enabled = false;

            PowerUpManager powerUpManager = GameObject.Find("PowerUpSpawn").GetComponent<PowerUpManager>();
            powerUpManager.IsPowered = false;
            powerUpManager.enabled = false;
        }

    }

    public void PointlessStranging()
    {
        if (transform.position != RandomPoint)
            transform.position = Vector3.MoveTowards(transform.position, RandomPoint, Speed * 0.25f * Timer.DeltaTimeMod);
        else
        {
            if (StandingTimer.Counter >= TimeToStand)
            {
                RandomPoint = new Vector3(
                Random.Range(-6.2f, 6.2f),
                Random.Range(-3.4f, 3.4f),
                0
                );
                TimeToStand = Random.Range(250, 750);
            }
            else
                StandingTimer.RunForwardTo(TimeToStand);
        }
      
    }

    public void CrowdCheer()
    {
        int maxCoolTime = 220;
        //If close to cude, crows goes wild!
        if (((Distance.x < 0.9f) && (Distance.x > -0.9f)) && ((Distance.y < 0.9f) && (Distance.y > -0.9f)))
        {
            if (CoolCrowd.Counter == maxCoolTime)
            {
                Audio.panStereo = Random.Range(0f, 0.6f) * (Random.Range(0, 2) * 2 - 1);
                Audio.PlayDelayed(Random.Range(0.01f, 0.3f));
                CoolCrowd.Counter = maxCoolTime - 1;
            }
            CloseCall += 2 * EnemySpawner.WaveCount * Timer.DeltaTimeMod;
        }

        if (CoolCrowd.Counter < maxCoolTime)
        {
            CoolCrowd.RunReverseFrom(maxCoolTime);
        }
    }
}
