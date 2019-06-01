using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSubArraysPlaces : MonoBehaviour
{

    //arrays of GameObject`s numbers
    public GameObject[] numbers2;
    public GameObject[] tempArray;
    public float speed = 1.0f;    //speed   
    //choose side of subarray, start of mooving and reaching targets
    public bool right, start = false, start_changing = false, finished = false;

    List<Vector3> targets = new List<Vector3>();//target destinations of gameObjects
    List<Vector3> second_targets = new List<Vector3>();//target destinations of gameObjects from second array



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start_changing)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            int i = 0; //initialization of counters
            while (i < numbers2.Length && i < tempArray.Length)
            {
                tempArray[i].transform.position = Vector3.MoveTowards(tempArray[i].transform.position, targets[i], step);
                numbers2[i].transform.position = Vector3.MoveTowards(numbers2[i].transform.position, second_targets[i], step);
            }

            //if (temparray.length > numbers2.length)
            //{

            //    for (int j = temparray.length; j < temparray.length; j++)
            //    {
            //        numbers[j].transform.position = vector3.movetowards(numbers[j].transform.position, second_numbers[j].transform.position, step);
            //    }
            //}
        }

    }
    //-----------------------------------------------------
    //set target destinations of GameObjects
    public IEnumerator set_targets_for_changing()
    {

        List<Vector3> targets1 = new List<Vector3>();//target destinations of gameObjects
        targets = targets1;

        List<Vector3> targets2 = new List<Vector3>();//target destinations of gameObjects
        second_targets = targets2;

        int num = 0;//initialization of counter
        while (num < numbers2.Length)
        {
            targets.Add(numbers2[num].transform.position);
            num++;
        }

        num = 0;
        while (num < tempArray.Length)
        {
            second_targets.Add(tempArray[num].transform.position);
            num++;
        }
        start_changing = true;
        yield return null;
    }
}
