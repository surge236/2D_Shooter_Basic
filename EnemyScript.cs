using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject target; // Who the player is attacking.
    public float ROTATION_SPEED;
    public float MOVE_SPEED;
    public float ATTACK_SPEED;
    public int scoreWorth; // How much score the enemy is worth
    //public int health; // The amount of damage the enemy can take///////////////////////////////////////////

    private Rigidbody2D rb;
     private float lookAngle;

     // Use this for initialization
     void Start () {
          rb = this.GetComponent<Rigidbody2D>();
     }
	
	// Update is called once per frame
	void Update () {
          Vector2 targetLocation = target.GetComponent<Transform>().position;
          Vector2 currentLocation = this.GetComponent<Transform>().position;
          Vector2 moveDirection = targetLocation - currentLocation;

          // Rotate the enemy towards the target
          float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
          angle -= 90;
          lookAngle = angle;
          Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
          transform.rotation = Quaternion.Slerp(transform.rotation, rotation, ROTATION_SPEED * Time.deltaTime);

          // Move the enemy towards the target
          int xMotion = 0; if (moveDirection.x > 0) xMotion = 1; else if (moveDirection.x < 0) xMotion = -1;
          int yMotion = 0; if (moveDirection.y > 0) yMotion = 1; else if (moveDirection.y < 0) yMotion = -1;
          rb.MovePosition(new Vector2(rb.position.x + (xMotion * MOVE_SPEED), rb.position.y + (yMotion * MOVE_SPEED)));

          //rb.MovePosition(rb.position);
          //rb.AddRelativeForce(moveDirection * MOVE_SPEED);
     }

    // Called when the game is meant to end
    void GameOver() {
        Destroy(this.gameObject);
    }

     // When the enemy collides with something else
     void OnCollisionEnter2D(Collision2D col) {
          // Destroy the enemy if it collides with a bullet, then add the enemy's scoreGiven to the shooter's score.
          if (col.gameObject.CompareTag("Bullet")) {
               // increment the bullet shooter's score
               GameObject shot = col.gameObject.GetComponent<BulletScript>().shooter;
               if (shot.GetComponent<PlayerScript>() != null) { // Confirm the shooter was a player
                    shot.GetComponent<PlayerScript>().incrPlayerScore(scoreWorth); // Increment the shooter's score
               }

               Destroy(this.gameObject); // Desroy the enemy
          }

          // If the enemy collides with a wall, ignore the collision.
          if (col.gameObject.CompareTag("Wall")) {
               Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
          }
     }
}
