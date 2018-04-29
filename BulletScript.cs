using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

     public float LIFE_TIME; // How long the bullet lives
     public GameObject shooter; // Who shot the bullet
     public int damage; // The amount of damage the bullet deals //////////////////////////////////////////////////

	// Use this for initialization
	void Start () {
          GameObject.Destroy(this.gameObject, LIFE_TIME);
	}

     // Update is called once per frame
     void Update() {

     }

    // Called when the game is meant to end
    void GameOver() {
        Destroy(this.gameObject);
    }

    // When the bullet collides with something
    void OnCollisionEnter2D(Collision2D col) {
          // Bullets should ignore collisions with each other
          if (col.gameObject.CompareTag("Bullet") == true) {
               Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
          }
          // The bullet is destroyed when it hits anything but the player
          else if (col.gameObject.CompareTag("Player") == false) {
               Debug.Log("Collision with: " + col.gameObject.tag);
               Destroy(this.gameObject, 0);
          }
          // Ignore bullet collisions with the player
          else Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
     }
}
