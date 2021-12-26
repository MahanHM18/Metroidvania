using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAttackCollider : MonoBehaviour
{
    private float timer;
    [SerializeField] private float AttackTimer;
    

    private void OnEnable()
    {
        timer = AttackTimer;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
        else 
            gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.parent.GetComponent<PenitentAttack>().HitEnemy = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.parent.GetComponent<PenitentAttack>().HitEnemy = false;
        }
    }

    
}
