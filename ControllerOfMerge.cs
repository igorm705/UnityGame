using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOfMerge : MonoBehaviour
{
    const int ARRAY_SIZE = 7;

    //central array of GameObjects
    public GameObject[] numbers = new GameObject[ARRAY_SIZE];
    public GameObject[] tempRight = new GameObject[];//right subarray
    public GameObject[] tempLeft = new GameObject[];//left subarray

    //two-dimensional array of gameObject's locations
    Vector3[][] gameObjects_locations;

    int currentSubArrayLocation = 0;

    public bool start_sort = false;//start sorting
    public bool rightSubarray = false; //boolean variable sighned part of subArray
    //--------------------------------------------------
    void Start()
    {
        //initial locations of GameObjects
        var locationOfGameObjects = new List<Vector3>();

        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            //place cubes with numbers
            float temp_float = UnityEngine.Random.Range(-100.0f, 100.0f);
            int temp_int = (int)temp_float;
            textMesh1 = numbers[i].GetComponent<TextMesh>();
            textMesh1.text = temp_int.ToString();

            locationOfGameObjects.Add(numbers[i].transform.position);
        }

        //copy initial locations of gameObject's to array
        gameObjects_locations[0] = listOfObjects.ToArray();
    }

    //--------------------------------------------------
    void Update()
    {
        if (start_sort)
        {
            StartCoroutine(start_sorting());
        }

    }
    //--------------------------------------------------
    //the purpose of this IEnumerator - running of break_array only once in "void Update"!! and
    //not every frame
    private IEnumerator start_sorting()
    {
        break_array(numbers);
    }
    //---------------------------------------------------
    //main function of sorting 
    private void break_array(GameObject[] numbers)
    {
      

        if (numbers.Length == 1)
        {
            //now we are taking care for case when we start compare values of array 
        }
        else
        {
         //choosing left or right subarray
          rightSubarray = chooseRightOrLeftSubarray();

          tempRight = giveBackRightPart();
          tempLeft = giveBackLeftpart();


            //divide array to subArrays: we must know:
            //1) is this subarray left or right and 
            //2) current position on gameObjects_locations 
           if (rightSubarray)
           {
                StartCoroutine(moveUp(tempRight, rightSubarray,  currentSubArrayLocation));
                break_array(tempRight);
           }
           else
           {
                StartCoroutine(moveUp(tempLeft, rightSubarray, currentSubArrayLocation));
                break_array(tempLeft);
           }
        }
    }
    //---------------------------------------------------
    //choosing left or right subarray 
    private bool chooseRightOrLeftSubarray()
    {
        StartCoroutine(chooseRightOrLeftSubarray());

        return rightSubarray;
    }
    //--------------------------------------------------
    private IEnumerator chooseRightOrLeftSubarray()
    {
        bool done = false;
        //Debug.Log("IEnumerator waitForEnterPress");

        while (!done) // essentially a "while true", but with a bool to break out naturally
        {


             if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rightSubarray = false;
                done = true; // breaks the loop     
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rightSubarray = true;
                done = true; // breaks the loop     
            }

            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

    }

    //---------------------------------------------------
    //rising choosing part of GameObject's array
    private IEnumerator moveUp (GameObject[] temp, bool rightSubarray,
                               int currentSubArrayLocation)
    {

        yield return null;
    }
    //---------------------------------------------------
}
