using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public void ZoomIn()
    {
        GameObject gameController = GameObject.Find("gameController");
        gameController.GetComponent<GameController>().ToggleUI(true);
        gameController.GetComponent<GameController>().timerStart = true;
    }
}
