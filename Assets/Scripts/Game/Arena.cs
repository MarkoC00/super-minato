using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject arenaEnter;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject tipText;
    public GameObject healthBars;
    public GameObject shikamaru;

    public void StartArena()
    {
        Destroy(arenaEnter);

        leftWall.SetActive(true);
        rightWall.SetActive(true);

        tipText.SetActive(false);

        healthBars.SetActive(true);

        shikamaru.GetComponent<Shikamaru>().startCombat = true;
    }
}
