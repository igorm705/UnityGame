using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOfMerge : MonoBehaviour
{
    const int ARRAY_SIZE = 7;

    //textMesh
    private TextMesh textMesh1;

    //central array of GameObjects
    public GameObject[,] numbers = new GameObject[ARRAY_SIZE, ARRAY_SIZE];
    public GameObject[] tempRight;//right subarray
    public GameObject[] tempLeft;//left subarray

    //in order to desactivate PlayerController and Animator of Ellen
    public GameObject Ellen;

    //two-dimensional array of gameObject's locations
    Vector3[,] gameObjects_locations = new Vector3[ARRAY_SIZE, ARRAY_SIZE];


    public bool start_sort = false;//start sorting
    public bool rightSubarray = false; //boolean variable sighned part of subArray

    public int index = 0; //index of current row of  array "numbers"
    //--------------------------------------------------
    void Start()
    {

        var bc = this.GetComponent<Random_Numbers>();
        for (int i = 0; i < ARRAY_SIZE; i++) {
            numbers[0,i] = bc.numbers[i];
        }
        //assigning "null" in empty cells of "numbers"
        for (int row = 1; row < ARRAY_SIZE; row++)
        {
            for (int col = 0; col < ARRAY_SIZE; col++)
            {
                numbers[row,col] = null;
            }
        }

        ////assign GameObject's initial locations
        //for (int i = 0; i < ARRAY_SIZE; i++)
        //{ 
        //    gameObjects_locations[0, i] = numbers[0, i].transform.position;

        //}
        //     //complete array locations of GameObjects
        //    for (int row = 0;  row < ARRAY_SIZE; row++)
        //    {
        //        for (int col = 0; col < ARRAY_SIZE; col++)
        //        {
        //            gameObjects_locations[row,col] = gameObjects_locations[row - 1,col];
        //            gameObjects_locations[row,col].y += 1f;
        //        }
        //    }
    }

    //--------------------------------------------------
    void Update()
    {
       
    }
    //--------------------------------------------------
    //the purpose of this IEnumerator - running of break_array only once in "void Update"!! and
    //not every frame
    public IEnumerator start_sorting()
    {
 
        //deactivate control of Ellen, because we need control array`s sort
        var ellen = Ellen.GetComponent<Gamekit3D.PlayerController>();
        var ellenAnimator = Ellen.GetComponent<Animator>();
        ellen.enabled = false;
        ellenAnimator.enabled = false;

        StartCoroutine(break_array(ARRAY_SIZE));

        yield return null;
    }
    //---------------------------------------------------
    //main function of sorting 
    private IEnumerator break_array(int length_not_zero)
    {
      
        if (length_not_zero == 1)
        {
            //now we are taking care for case when we start compare values of array 
        }
        else
        {
         //choosing left or right subarray
         yield return StartCoroutine(chooseRightOrLeftSubarrayIE(KeyCode.Return));


        if (rightSubarray)
        {
         length_not_zero = next_row_RightPart(index);
        }
        else
        {
         length_not_zero = next_row_LeftPart(index);
        }

          StartCoroutine(moveUp(index));
          break_array(index++);
        }
        yield return null; // wait until next frame, then continue execution from here (loop continues)
    }
    //---------------------------------------------------
    private int next_row_LeftPart(int index)
    {
        Debug.Log("giveBackLeftPart");
        //check an amount of not-null values  in the current row 
        int amount = 0, start_index = 0;
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            if (numbers[index, i] != null)
            {
                amount++;
            }//write down from which index not-null values start
            if (amount == 1)
            {
                start_index = i;
            }
        }

        for (int i = start_index; i < amount/2; i++)
        {
            numbers[index + 1, i] = numbers[index, i];
        }

        return amount;
    }
    //---------------------------------------------------
    private int next_row_RightPart(int index)
    {
        Debug.Log("giveBackRightPart");

        //check an amount of not-null values  in the current row 
        int amount = 0, start_index=0;
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
           if (numbers[index, i] != null)
            {
                amount++;
            }//write down from which index not-null values start
            if (amount == 1)
            {
                start_index = i;
            }
        }

        for (int i = (start_index + amount/2); i < (start_index + amount); i++)
         {
            numbers[index + 1, i] = numbers[index, i];
         }

        return amount;
    }
    //--------------------------------------------------
    private IEnumerator chooseRightOrLeftSubarrayIE(KeyCode key)
    {
        bool done = false;
        //Debug.Log("IEnumerator waitForEnterPress");

        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop             
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rightSubarray = false;
           
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rightSubarray = true;
            }

            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

    }

    //---------------------------------------------------
    //rising choosing part of GameObject's array
    private IEnumerator moveUp (int index)
    {
        var bc = this.GetComponent<MoveSubArrayUp>();
        bc.index = index;
      
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            bc.numbers[index,i] = numbers[index, i];
        }

        bc.start_moving = true;

        yield return null;
    }
    //---------------------------------------------------
}
