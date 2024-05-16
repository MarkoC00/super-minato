using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;

    private int rasenganCharge = 0;
    private int kunaiCharge = 0;

    GameObject currentKunai = null;

    public Transform rasPosR;
    public Transform rasPosL;
    public GameObject rasenganPrefab;
    public GameObject kunaiPrefab;

    public int rasenganChargePerSecret;
    public int kunaiChargePerSecret;

    public int maxKunaiRasenganCharges;

    public EnemyInRange enemyInRangeDetector;
    private GameObject enemyInRange
    {
        get
        {
            return enemyInRangeDetector.currentTarget;
        }
    }

    public GameObject rasenganUI;
    public GameObject kunaiUI;

    void Update()
    {
        CheckForAttack();
    }

    public void HitSecretBox()
    {
        rasenganCharge += rasenganChargePerSecret + (int)RandomDropFromSecretBox().x;
        kunaiCharge += kunaiChargePerSecret + (int)RandomDropFromSecretBox().y;

        if (rasenganCharge > maxKunaiRasenganCharges)
            rasenganCharge = maxKunaiRasenganCharges;
        if (kunaiCharge > maxKunaiRasenganCharges)
            kunaiCharge = maxKunaiRasenganCharges;

        rasenganUI.GetComponent<RasenganKunai>().Show(rasenganCharge);
        kunaiUI.GetComponent<RasenganKunai>().Show(kunaiCharge);


        Debug.Log("Rasengan Charge: " + rasenganCharge);
        Debug.Log("Kunai Charge: " + kunaiCharge);
    }

    Vector2 RandomDropFromSecretBox()
    {
        Vector2 ret = Vector2.zero;

        int rasenganRand = Random.Range(0, 100); // Generate a random number between 0 and 99

        if (rasenganRand < 50)
        {
            ret.x = 0;
        }
        else if (rasenganRand < 85)
        {
            ret.x = 1;
        }
        else
        {
            ret.x = 2;
        }
        
        int kunaiRand = Random.Range(0, 100);
        
        if (kunaiRand < 50)
        {
            ret.y = 0;
        }
        else if (kunaiRand < 85)
        {
            ret.y = 1;
        }
        else
        {
            ret.y = 2;
        }

        return ret;
    }

    void CheckForAttack()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (kunaiCharge > 0 && currentKunai == null)
            {
                KunaiThrow();
            }
            else if(currentKunai != null)
            {
                Vector3 transformPos = new Vector3(currentKunai.transform.position.x - 0.5f, currentKunai.transform.position.y, currentKunai.transform.position.z);
                this.transform.position = transformPos;
                Destroy(currentKunai);
            }
        }

        if (movement.isJumping)
            return; //Ukoliko ne skace izadji

        if (Input.GetKeyDown(KeyCode.J))
        {
            NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Rasengan
            if(rasenganCharge > 0)
            {
                Rasengan();
            }
        }
    }

    void NormalAttack()
    {
        animator.SetBool("Attack", true);
    }

    //Funkcije za animaciju
    public void SetAttackToFalse()
    {
        animator.SetBool("Attack", false);
    }
    public void DealDamage()
    { 
        if (enemyInRange != null)
            enemyInRange.GetComponent<EasyEnemyMovement>().UnistiMe();
    }

    public void Rasengan()
    {
        animator.SetBool("Rasengan", true);

        rasenganCharge -= 1;
        rasenganUI.GetComponent<RasenganKunai>().Show(rasenganCharge);
    }
    public void KunaiThrow()
    {
        currentKunai = null;
        kunaiCharge -= 1;
        kunaiUI.GetComponent<RasenganKunai>().Show(kunaiCharge);

        if (!movement.sp.flipX) //desno
        {
            var hit = Physics2D.Raycast(transform.position, rasPosR.position-transform.position, Vector2.Distance(transform.position,rasPosR.position)*1.5f, LayerMask.GetMask("Ground"));
            Debug.Log(hit.collider);
            Vector3 spawnPos = (hit.collider == null) ? rasPosR.position : hit.point;
            currentKunai = Instantiate(kunaiPrefab,spawnPos, Quaternion.identity);
            currentKunai.GetComponent<Kunai>().direction = 1;
        }
        if (movement.sp.flipX) //levo
        {
            currentKunai = Instantiate(kunaiPrefab, rasPosL.position, Quaternion.identity);
            currentKunai.GetComponent<Kunai>().direction = -1;

        }
    }

    public void StopRasengan()
    {
        animator.SetBool("Rasengan", false);
    }

    public void SpawnRasengan()
    { 
        if(!movement.sp.flipX)
            Instantiate(rasenganPrefab, rasPosR.position, Quaternion.identity);
        if(movement.sp.flipX)
            Instantiate(rasenganPrefab, rasPosL.position, Quaternion.identity);
    }
}
