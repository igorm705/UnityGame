using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSubArrayUp : MonoBehaviour
{
    const int ARRAY_SIZE = 7;

    //arrays of GameObject`s locations
    public Vector3[,] locations = new Vector3[ARRAY_SIZE, ARRAY_SIZE];

    //central array of GameObjects
    public GameObject[,] numbers = new GameObject[ARRAY_SIZE, ARRAY_SIZE];

    public bool start_moving = false;

    public float speed = 1.0f;    //speed   
    public int index = 0; //index of the row that we treating
    //---------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        var bc = this.GetComponent<Random_Numbers>();
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            numbers[0, i] = bc.numbers[i];
            locations[0, i] = bc.numbers[i].transform.position;
        }
        //assigning all possible locations of the GameObjects
        for (int row = 1; row < ARRAY_SIZE; row++)
        {
            for (int col = 0; col < ARRAY_SIZE; col++)
            {
                locations[row, col] = locations[row-1, col];
                locations[row, col].y += 1f;
            }
        }
    }
    //---------------------------------------------
    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move

        if (start_moving)
        {

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[0, i].transform.position = Vector3.MoveTowards(numbers[i].transform.position, locations[i], step);
            }
        }
    }
    //---------------------------------------------
}
