using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLvl2 : MonoBehaviour
{
    public PlayerCombat player;
    public GameObject healthBars;
    public GameObject playerHealthBar;
    public GameObject enemyHealthBar;
    public GameObject rasenganUI;
    public GameObject kunaiUI;

    private void Start()
    {
        healthBars.SetActive(true);
        enemyHealthBar.SetActive(false);

        Invoke("SetupPlayerParams", 0.1f);
    }

    void SetupPlayerParams()
    {
        player.currentHelath = StaticData.playerHealth;
        player.rasenganCharge = StaticData.playerRasenganCharge;
        player.kunaiCharge = StaticData.playerKunaiCharge;

        playerHealthBar.GetComponent<HealthSlider>().SetHealth(StaticData.playerHealth);

        rasenganUI.GetComponent<RasenganKunai>().Show(StaticData.playerRasenganCharge);
        kunaiUI.GetComponent<RasenganKunai>().Show(StaticData.playerKunaiCharge); 
    }
}
