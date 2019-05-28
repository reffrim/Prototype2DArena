using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public Timer KillTimer;

    GameObject CubeReference;
    
    // Start is called before the first frame update
    void Start()
    {
        CubeReference = GameObject.Find("Cube");
        KillTimer = new Timer(0);
    }

    // Update is called once per frame
    void Update()
    {
        AnimatePowerUp();
        if (CubeReference != null)
        {
            DestroyPowerUp(); 
        }
    }

    private bool AtePowerUp()
    {
        // Eat or Destroy AtePowerUp
         Vector2 distance = new Vector2(
             transform.position.x - CubeReference.transform.position.x,
             transform.position.y - CubeReference.transform.position.y
         );

        if (((distance.x < 0.5f) && (distance.x > -0.5f)) && ((distance.y < 0.5f) && (distance.y > -0.5f)))
            return true;
        else
            return false;
    }

    private void DestroyPowerUp()
    {
        if (AtePowerUp())
        {
            GameObject.Destroy(gameObject);
            PowerUpManager.PowerUpMeter.Counter += 100;

            AudioSource[] audios = GetComponentsInParent<AudioSource>();
            int rand = Random.Range(0, 3);
            audios[rand].Play();
        }

        if(KillTimer.Counter > 140)
        {
            GameObject.Destroy(gameObject);
        }
        KillTimer.RunForwardTo(140);
    }

    private void AnimatePowerUp()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 0.05f * Timer.DeltaTimeMod);
    }
}
