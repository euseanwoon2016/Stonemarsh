﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosChange : MonoBehaviour {
    public Transform camPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SmoothCameraAdvanced cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothCameraAdvanced>();
            switch (cam.followTarget)
            {
                case true:
                    cam.followTarget = false;
                    cam.transform.position = camPos.transform.position;
                    break;
                case false:
                    cam.followTarget = true;
                    break;
            }
        }
    }
}
