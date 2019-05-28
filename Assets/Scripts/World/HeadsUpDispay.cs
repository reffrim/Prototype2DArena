using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsUpDispay : MonoBehaviour
{
    private int FontSize;

    public GUIStyle FontStyleResources = new GUIStyle();
    public GUIStyle FontStyleScore = new GUIStyle();
    public GUIStyle FontStylePause = new GUIStyle();
    public int Offset;

    private void Start()
    {
        //FontStyleResources.font = FontStyleScore.font = Resources.Load<Font>("Fonts/Dr.Nerve");
    }

    private void OnGUI()
    {
        ScaleFontSize();
        FontBloom(FontStyleScore);
        //GUI.contentColor = Color.blue; // Worked til the style hasn't been set;

        //GUIStyle style = new GUIStyle();
        //style.fontSize = 30;
        //style.normal.textColor = Color.blue;

        if (WorldManager.GrandTotalScore > 0)
        {
            GUI.Label(
               new Rect(Screen.width / 85f + Offset, Screen.height / 1.15f + Offset, Screen.width, Screen.height),
               WorldManager.GrandTotalScore.ToString(),
               FontStyleScore
               ); 
        }

        GUI.Label(
            new Rect(Screen.width / 85f + Offset, Screen.height / 50 + Offset, Screen.width, Screen.height), 
            string.Format("PowerUP: {0}", Mathf.RoundToInt(PowerUpManager.PowerUpMeter.Counter).ToString()),
            FontStyleResources
            );

        GUI.Label(
           new Rect(Screen.width / 85f + Offset, Screen.height / 20 + Offset, Screen.width, Screen.height),
           string.Format("CoolDown: {0}", Mathf.RoundToInt(CubeController.TeleportCool.Counter).ToString()),
           FontStyleResources
           );
        
        if(WorldManager.PauseSwitch && !GameOverManager.IsGameOVer)
            GUI.Label(
          new Rect(Screen.width / 2.6f + Offset, Screen.height / 2.4f + Offset, Screen.width, Screen.height),
          "PAUSE",
          FontStylePause
          );


    }

    private void ScaleFontSize()
    {
        FontSize = Screen.width / 48;
        FontStyleResources.fontSize = FontSize;
    }

    private void FontBloom(GUIStyle fontToScale)
    {
        int sizeFactor = (WorldManager.IsScoring) ? FontSize * 4 : FontSize * 2;
        fontToScale.fontSize = (int)Mathf.Lerp(fontToScale.fontSize, sizeFactor, 0.09f * Timer.DeltaTimeMod);
    }
}
