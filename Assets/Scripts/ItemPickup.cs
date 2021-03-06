﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public Item item;
    PlayerController player;
    public float magnetSpeed = 10f;
    public float rotateSpeed = 600f;
    bool inRange = false;

    // Use this for initialization
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Vector3.Distance(transform.position, player.transform.position) <= .5f))
        {
            if (inRange)
            {
                if (Inventory.instance.HasSpace()) { 
                    Attract();
                }
                else
                {
                    inRange = false;
                }
            }
        }
    }
    void PickUp()
    {
        Inventory.instance.Add(item);
        Destroy(gameObject);

    }
    void Attract()
    {
        transform.RotateAround(player.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, magnetSpeed * (Vector3.Distance(transform.position, player.transform.position)) * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, player.transform.position, (multiplier * (Vector3.Distance(transform.position,player.transform.position))) * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag ("Player"))
        {
            PickUp();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }

    }
}
