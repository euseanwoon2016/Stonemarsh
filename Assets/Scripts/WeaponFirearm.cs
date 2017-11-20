﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFirearm : MonoBehaviour {
    //====================================//
    //              Guns etc.             //
    //====================================//
    public float rateOfFire = 0.5f;
    public GameObject player;
    public PlayerController playerScript;
    public bool isAttacking = false;
    public GameObject projectile;
    public float bulletSpeed = 100f;
    public int energyConsumption = 5;
    // Use this for initialization
    void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        playerScript = player.GetComponent<PlayerController>();
        rateOfFire = playerScript.localPlayerData.attackSpeed;

    }
    Vector3 prjPos;
    Quaternion prjRotation;
    Vector3 prjForce;

    // Update is called once per frame
    void Update () {
        if (playerScript == null)
        {
            playerScript = player.GetComponent<PlayerController>();
        }
        prjPos = transform.position + new Vector3(0f, -0.0065f, 0.004f);
        prjRotation = player.transform.rotation;
        rateOfFire -= 1 * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        prjForce = player.transform.forward * bulletSpeed;
        if (isAttacking && rateOfFire <= 0 && playerScript.localPlayerData.currentEnergy >= energyConsumption)
        {
            playerScript.localPlayerData.currentEnergy -= (energyConsumption - (playerScript.localPlayerData.playerInt / 10));
            rateOfFire = playerScript.localPlayerData.attackSpeed;
            Attack();
        }


    }
    float Crit(float percent, float critMultiplier)
    {
        if (Random.value <= (percent / 100f))
        {
            return critMultiplier;
        }
        return 1f;
    }
    void Attack()
    {
        var clone = Instantiate(projectile,prjPos,prjRotation);
        var cloneScript = clone.GetComponent<WeaponProjectile>();
        Debug.Log(cloneScript.damage + "Dmg");
        cloneScript.damage *= Mathf.RoundToInt(Crit(playerScript.localPlayerData.critChance, playerScript.localPlayerData.critMultiplier));
        Debug.Log(cloneScript.damage + "Dmg");
        clone.GetComponent<Rigidbody>().AddForce(prjForce,ForceMode.VelocityChange);
        //Test for bool, if yes instantiate bullet with speed, control bool with animator.
    }
}
