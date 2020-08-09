using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    public GameObject[] enemies;

    public void EnemyEncounter(Transform currentRoom)
    {
        int spawnNumber = Random.Range(2, 8);
        

        //Debug.Log("SpawnNumber: " + spawnNumber + "Index: " + index);

        for (int i = 0; i < spawnNumber; i++)
        {
            int index = Random.Range(0, enemies.Length);
            GameObject enemy = Instantiate(enemies[index], currentRoom.position + new Vector3(Random.Range(-6, 6), Random.Range(-3, 3), 1), Quaternion.identity);
            enemy.transform.parent = currentRoom;

            currentRoom.GetComponent<RoomEnter>().currentEnemies.Add(enemy);
        }
    }
}
