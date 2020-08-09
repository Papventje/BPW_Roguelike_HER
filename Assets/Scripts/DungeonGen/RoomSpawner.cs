using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public enum Direction {Top, Bottom, Left, Right};

    public Direction direction;

    private RoomTemplates templates;

    int rand;
    int numb;
    public bool spawned = false;


    private void Start()
    {
        //Destroy(gameObject, 4f);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", .1f);
    }

    void Spawn()
    {
        if (!spawned)
        {
            //Debug.Log(templates.rooms.Count);
            switch (direction)
            {
                case Direction.Top:
                    //Spawn room with BOTTOM door;
                    //Make sure the first rooms isn't a dead end

                    if (templates.rooms.Count == 0)
                    {
                        numb = Random.Range(1, templates.botRooms.Length);
                        Instantiate(templates.botRooms[1], transform.position, templates.botRooms[1].transform.rotation);
                    }
                    else
                    {
                        rand = Random.Range(0, templates.botRooms.Length);
                        Instantiate(templates.botRooms[rand], transform.position, templates.botRooms[rand].transform.rotation);
                    }

                    break;
                case Direction.Bottom:
                    //Spawn room with TOP door;
                    //Make sure the first rooms isn't a dead end

                    if (templates.rooms.Count == 0)
                    {
                        numb = Random.Range(1, templates.topRooms.Length);
                        Instantiate(templates.topRooms[1], transform.position, templates.topRooms[1].transform.rotation);
                    }
                    else
                    {
                        rand = Random.Range(0, templates.topRooms.Length);
                        Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    }

                    break;
                case Direction.Left:
                    //Spawn room with RIGHT door;
                    //Make sure the first rooms isn't a dead end

                    if (templates.rooms.Count == 0)
                    {
                        numb = Random.Range(1, templates.rightRooms.Length);
                        Instantiate(templates.rightRooms[1], transform.position, templates.rightRooms[1].transform.rotation);
                    }
                    else
                    {
                        rand = Random.Range(0, templates.rightRooms.Length);
                        Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    }

                    break;
                case Direction.Right:
                    //Spawn room with LEFT door;
                    //Make sure the first rooms isn't a dead end

                    if (templates.rooms.Count == 0)
                    {
                        numb = Random.Range(1, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[1], transform.position, templates.leftRooms[1].transform.rotation);
                    }
                    else
                    {
                        rand = Random.Range(0, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    }

                    break;
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
