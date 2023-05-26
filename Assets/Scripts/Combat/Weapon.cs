using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    int dmg;

    private void Start()
    {
        dmg = transform.parent.gameObject.GetComponent<CharacterStats>().damage.GetValue();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterStats>() != null)
        {
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(dmg);
        }
    }
}
