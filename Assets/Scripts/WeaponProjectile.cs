﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour {
    //====================================//
    //           Bullets etc.             //
    //====================================//
    public float knockback = 0.5f;
    public int damage = 15;
    public Enemy enemyObject;
    public float destroyTimer = 5f;
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * knockback, ForceMode.VelocityChange);
            other.gameObject.GetComponent<Enemy>().currentHealth -= damage;

            //make some kinda effect here
            Debug.Log("Hit " + other.name);
        }
        Destroy(gameObject, destroyTimer);
    }
}
