using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

     private Rigidbody2D rb;
     private float lookAngle;
     private float lastShotTime;

     public int score;
     public float ROTATION_SPEED;
     public float MOVE_SPEED;
     public float BULLET_SPEED;
     public float ATTACK_SPEED;
     public GameObject bulletType;
     public Text scoreDisplay;
     public int shotStyle; // 0: standard shot, 1: spread shot //////////////////////////////////////////////////
     public int health; // The amount of damage the player can take /////////////////////////////////////////////

    private float rbPosX;
    private float rbPosY;

     // Use this for initialization
     void Start () {
        //GetComponent<Rigidbody2D>().position = Vector3.zero;
          rb = GetComponent<Rigidbody2D>();
        rbPosX = rb.position.x;
        rbPosY = rb.position.y;
        lastShotTime = Time.realtimeSinceStartup;
          ATTACK_SPEED *= .1f;
          score = 0;
     }

    // FixedUpdate is called every fixed framerate frame (use for rigidbody or other physics based calculations).
    private void FixedUpdate() {
        
        // Get the current rigidbody position
        rbPosX = rb.position.x;
        rbPosY = rb.position.y;

        // Control Player Movement
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)) moveUpLeft();
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)) moveUpRight();
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)) moveDownLeft();
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)) moveDownRight();
        else if (Input.GetKey(KeyCode.DownArrow)) moveDown();
        else if (Input.GetKey(KeyCode.UpArrow)) moveUp();
        else if (Input.GetKey(KeyCode.LeftArrow)) moveLeft();
        else if (Input.GetKey(KeyCode.RightArrow)) moveRight();

    }

    // Update is called once per frame
    void Update () {

          // Shoot when the left mouse button is pressed (but space it out)
          if (Input.GetMouseButton(0) && Time.realtimeSinceStartup - lastShotTime > ATTACK_SPEED) shoot(shotStyle);

          // Rotate the player towards the mouse
          Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
          angle -= 90;
          lookAngle = angle;
          Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
          transform.rotation = Quaternion.Slerp(transform.rotation, rotation, ROTATION_SPEED * Time.deltaTime);
     }

     // Make the player move down
     void moveDown() {
        // Movement based on transform
        //this.transform.position += Vector3.down * MOVE_SPEED;

        // Movement based on rigidbody (faster)
        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += Vector2.down * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // make the player move up
     void moveUp() {
        //this.transform.position += Vector3.up * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += Vector2.up * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // make the player move left
     void moveLeft() {
        //this.transform.position += Vector3.left * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += Vector2.left * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // make the player move right
     void moveRight() {
        //this.transform.position += Vector3.right * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += Vector2.right * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // make the player move diagonally to the upper left
     void moveUpLeft() {
        //this.transform.position += Vector3.up * MOVE_SPEED;
        //this.transform.position += Vector3.left * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += new Vector2(-1, 1) * MOVE_SPEED;
        rb.MovePosition(rbPosition);

    }

     // make the player move diagonally to the upper right
     void moveUpRight() {
        //this.transform.position += Vector3.up * MOVE_SPEED;
        //this.transform.position += Vector3.right * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += new Vector2(1, 1) * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // make the player move diagonally to the lower left
     void moveDownLeft() {
        //this.transform.position += Vector3.down * MOVE_SPEED;
        //this.transform.position += Vector3.left * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += new Vector2(-1, -1) * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // make the player move diagonally to the lower right
     void moveDownRight() {
        //this.transform.position += Vector3.down * MOVE_SPEED;
        //this.transform.position += Vector3.right * MOVE_SPEED;

        Vector2 rbPosition = new Vector2(rbPosX, rbPosY);
        rbPosition += new Vector2(1, -1) * MOVE_SPEED;
        rb.MovePosition(rbPosition);
    }

     // have the player shoot
     void shoot(int shotType) {
               lastShotTime = Time.realtimeSinceStartup;
               GameObject newBullet = Instantiate(bulletType, transform.position, transform.rotation, null);
               newBullet.SetActive(true);
               Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
               Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
               bulletRB.AddForce(direction.normalized * BULLET_SPEED);
     }

     // Increments the player's score by the parameter passed value
     public void incrPlayerScore(int addScore) {
          score += addScore;
          scoreDisplay.text = "Score: " + score.ToString();
     }

    // Called when the game is meant to end
    void GameOver() {
        Destroy(this.gameObject);
    }

    // Called when all objects in the scene need to recieve a broadcast
    void BroadcastAll(string message) {
        GameObject[] all = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < all.Length; i++) {
            if (all[i] && all[i].transform.parent == null) all[i].gameObject.BroadcastMessage(message);
        }
    }

    // When the player collides with something else
    void OnCollisionEnter2D(Collision2D col) {
        // Set the player state to off if an enemy touches them, then alert all enemies and spawners to destroy
        // themselves.
        if (col.gameObject.CompareTag("Enemy")) {
            //this.gameObject.SetActive(false);
            BroadcastAll("GameOver");
        }
     }

}
