using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

     // NOTE: Keep the times in chronological order or everything before the lowest will spawn at once.

     public int[] spawnTimes; // The times the enemies in the enemyTypes array will be spawned
     public int[] numberOfEnemies; // The number of enemies of the specified type to be spawned at the specified time.
     public GameObject[] enemyTypes; // The enemy type to be spawned at the specified time
     public int spawnInterval; // interval between which new enemies are spawned
     public int doesLoop; // True if the array index should loop, false if not
     public int spawnRadius; // Distance from the spawner enemies can spawn at

     private int arraySize; // The size of the arrays (array sizes should be equal)
     private float spawnerStartTime; // The time the spawner was created at
     private float spawnerCurrentTime; // The current system time
     private int spawnIndex; // The index at which information is pulled from the arrays

	// Use this for initialization
	void Start () {
          spawnerStartTime = Time.realtimeSinceStartup;
          spawnIndex = 0;
          arraySize = spawnTimes.Length;
	}
	
	// Update is called once per frame
	void Update () {
          // Loops the spawner if it should do that
          if (doesLoop == 1) if (spawnIndex == arraySize) {
                    spawnIndex = 0; // Loop the index if necessary
                    spawnerStartTime = Time.realtimeSinceStartup; // Change the start time so the spawn times work right
          }

          // If the spawner shouldn't loop, and it has run through the arrays, just destroy it
          if (doesLoop == 0) {
               if (spawnIndex >= arraySize) {
                    spawnIndex = 0;
                    Destroy(this.gameObject); Destroy(this);
               }
          }

          spawnerCurrentTime = Time.realtimeSinceStartup; // update the current time

          if (spawnerCurrentTime - spawnerStartTime > spawnTimes[spawnIndex]) spawnNextEnemy(); // spawn creatures when necessary
	}

     // Called when it is time for a new enemy to spawn
     void spawnNextEnemy() {
          int num = numberOfEnemies[spawnIndex]; // get the number of iterations
          GameObject enemyType = enemyTypes[spawnIndex]; // get the enemy type to spawn

          // spawn that many enemies (spawn one extra and destroy it to force them to spread out)
          float loopStart = Time.realtimeSinceStartup;
          for (int i = 0; i < num; i++) {
               // Randomly place the new enemy within spawn radius
               int xCo = Random.Range(-spawnRadius, spawnRadius);
               int yCo = Random.Range(-spawnRadius, spawnRadius);
               Vector3 newPosition = new Vector3(xCo, yCo, 0);
               transform.position += newPosition;

               GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation, null);
               //GameObject newEnemy = Instantiate(enemyType, transform.position, transform.rotation, null);
               newEnemy.SetActive(true);
          } 

          spawnIndex++; // Increment the index for future execution
     }

    // Called when the game is meant to end
    void GameOver() {
        Destroy(this.gameObject);
    }
}
