using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static int GrandTotalScore;
    public static bool IsScoring;
    public static bool PauseSwitch;

    bool BonusSwitch;
    SphereController SphereScript;
    Timer HornTimer;
    AudioSource[] Audio;
    List<GameObject> AllSpheres = new List<GameObject>();


    private void Start()
    {
        // Set tht target  framerate.
        //Application.targetFrameRate = 1;
        //QualitySettings.vSyncCount = 0;

        SphereScript = GameObject.Find("Sphere").GetComponent<SphereController>();
        Audio = GetComponents<AudioSource>();
        BonusSwitch = true;

        MusicStart();

        HornTimer = new Timer(0);
    }

    private void Update()
    {
        GetCloseCallScore();
        PreSpawnAirHorn();
        MusicModifier();
        BonusAnnounce();

        GamePause();

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("ProtoStart");
    }

    private void GetCloseCallScore()
    {
        if (SphereController.IsCubeEaten)
            return;
        int previousGransTotalScore = GrandTotalScore;
        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (!AllSpheres.Contains(sphere))
                AllSpheres.Add(sphere);
        }

        float tempTotal = 0;
        foreach (GameObject sphere in AllSpheres)
        {
            SphereController eachSphereScript = sphere.GetComponent<SphereController>();
            tempTotal += eachSphereScript.CloseCall;
        }
        GrandTotalScore = (int)tempTotal;

        IsScoring = ScoringCheck(previousGransTotalScore, GrandTotalScore);
    }

    private void PreSpawnAirHorn()
    {
        float hornInterval = EnemySpawner.SpawnInterval - 150;
        if (HornTimer.Counter > hornInterval)
        {
            if(!GameOverManager.IsGameOVer)
                Audio[1].Play();
        }

        if(EnemySpawner.SpawnTimer.Counter == 0)
        {
            HornTimer.Counter = 0;
        }
        HornTimer.RunForwardTo(hornInterval);
    }

    private void MusicStart()
    {
        Audio[2].Play();
        Audio[3].Play();
    }

    private void MusicModifier()
    {
        if (EnemySpawner.WaveCount < 4)
        {
            Audio[2].mute = false;
            Audio[3].mute = true;
        }
        else if (EnemySpawner.WaveCount == 4)
        {
            Audio[2].mute = true;
            Audio[3].mute = false;
        }

        // CodeBlock below have to increase the temp of music according to the wave count by its author ider, but it failed. 
        else if (EnemySpawner.WaveCount == 6)
        {
            Audio[3].pitch = Mathf.Lerp(Audio[3].pitch, 1.1f, 0.003f * Timer.DeltaTimeMod);
            Debug.Log(Audio[3].pitch);
        }
        else if (EnemySpawner.WaveCount > 6)
        {
            Audio[3].pitch = Mathf.Lerp(Audio[3].pitch, 1.25f, 0.003f * Timer.DeltaTimeMod);
            Debug.Log(Audio[3].pitch);
        }

    }

    private void BonusAnnounce()
    {
        if (GrandTotalScore > 1000 && GrandTotalScore < 3000 && BonusSwitch)
        {
            Audio[4].Play();
            PowerUpManager.PowerUpMeter.Counter *= 1.5f;
            BonusSwitch = !BonusSwitch;
        }
        else if (GrandTotalScore > 3000 && !BonusSwitch)
        {
            Audio[5].Play();
            PowerUpManager.PowerUpMeter.Counter *= 1.25f;
            BonusSwitch = !BonusSwitch;
        }
    }

    private void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PauseSwitch = !PauseSwitch;

        if(PauseSwitch && !GameOverManager.IsGameOVer)
        {
            Time.timeScale = 0;
            GameObject.Find("Cube").GetComponent<CubeController>().enabled = false;
        }
        else if (!PauseSwitch && !GameOverManager.IsGameOVer)
        {
            Time.timeScale = 1;
            GameObject.Find("Cube").GetComponent<CubeController>().enabled = true;
        }
    }

    private bool ScoringCheck(int previousFramesScore, int currentFramesScore)
    {
        if (previousFramesScore < currentFramesScore)
            return true;
        else
            return false;
    }

}
