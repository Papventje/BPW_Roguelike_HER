using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    [SerializeField]
    bool entered = false;

    GameObject doors;

    public List<GameObject> currentEnemies;

    EnemyEvent enemyEvent;

    private void Start()
    {
        enemyEvent = GameObject.FindGameObjectWithTag("Events").GetComponent<EnemyEvent>();

        doors = transform.GetChild(0).gameObject;
        doors.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Transform currentRoom = transform;
            if (!entered)
            {
                enemyEvent.EnemyEncounter(currentRoom);
                doors.SetActive(true);
                entered = true;
            }
            
            Camera.main.transform.position = new Vector3(currentRoom.position.x, currentRoom.position.y, Camera.main.transform.position.z);
        }
    }

    private void Update()
    {
        if(entered && currentEnemies.Count == 0)
        {
            doors.SetActive(false);
        }
    }
}
