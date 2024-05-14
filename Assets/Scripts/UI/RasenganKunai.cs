using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenganKunai : MonoBehaviour
{
    private GameObject[] children;

    private void Start()
    {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
    }

    public void Show(int number)
    {
        number = Mathf.Clamp(number, 0, transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i].SetActive(false);
        }

        for (int i = 0; i < number; i++)
        {
            children[i].SetActive(true);
        }
    }
}
