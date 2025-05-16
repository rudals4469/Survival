using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampFire : MonoBehaviour
{

    public int damage;

    public int damageRate;

    private List<IDamagebIble> things = new List<IDamagebIble>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DealDamage",0 ,damageRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DealDamage()
    {
        for (int i=0; i < things.Count; i++)
        {
            things[i].TakePhsycialDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagebIble damagebIble))
        {
            things.Add(damagebIble);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagebIble damagebIble))
        {
            things.Remove(damagebIble);
        }
    }
}
