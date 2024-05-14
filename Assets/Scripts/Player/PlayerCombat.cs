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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.tag == "SecretBox")
        {
            rasenganCharge += rasenganChargePerSecret + (int)RandomDropFromSecretBox().x;
            kunaiCharge += kunaiChargePerSecret + (int)RandomDropFromSecretBox().y;

            if (rasenganCharge > maxKunaiRasenganCharges)
                rasenganCharge = maxKunaiRasenganCharges;
            if (kunaiCharge > maxKunaiRasenganCharges)
                kunaiCharge = maxKunaiRasenganCharges;

            rasenganUI.GetComponent<RasenganKunai>().Show(rasenganCharge);
            kunaiUI.GetComponent<RasenganKunai>().Show(kunaiCharge);

            collision.gameObject.GetComponent<SecretBox>().DestroyMe();

            Debug.Log("Rasengan Charge: " + rasenganCharge);
            Debug.Log("Kunai Charge: " + kunaiCharge);
        }
    }

    Vector2 RandomDropFromSecretBox()
    {
        Vector2 ret = Vector2.zero;

        int rasenganRand = Random.Range(0, 100); 

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

        currentKunai = Instantiate(kunaiPrefab, rasPosR.position, Quaternion.identity);
    }

    public void StopRasengan()
    {
        animator.SetBool("Rasengan", false);
    }

    public void SpawnRasengan()
    {
        if (GetComponent<PlayerMovement>().movementDir == 1)
            Instantiate(rasenganPrefab, rasPosR.position, Quaternion.identity);
    
        if(GetComponent<PlayerMovement>().movementDir == -1)
            Instantiate(rasenganPrefab, rasPosL.position, Quaternion.identity);
        else
            Instantiate(rasenganPrefab, rasPosR.position, Quaternion.identity);

    }
}
