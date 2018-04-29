using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

     public GameObject player; // The player
     private Vector3 offset; // Space between camera and player

     // Use this for initialization
     void Start() {
          offset = transform.position - player.transform.position;
     }

     // LateUpdate is called once per frame, after Update has completed.
     void LateUpdate() {
          transform.position = player.transform.position + offset;
     }
}