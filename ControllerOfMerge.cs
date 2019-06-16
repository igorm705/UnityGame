using System;
using System.Collections;
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

    //array of two-dimensional vector for starting and ending of subarrays
    int[] start_end = new int[4];

    public bool start_sort = false;//start sorting
    public bool rightSubarray = false; //boolean variable sighned part of subArray

    public int index = 0; //index of current row of  array "numbers"
    public int depth; //how much need to go down in order to collect the array

    int length_not_zero = ARRAY_SIZE;

    //move choosen subArray up
    bool start_moving = false;



    //--------------------------------------------------
    void Start()
    {


        var bc = this.GetComponent<Random_Numbers>();
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            numbers[0, i] = bc.numbers[i];
        }
        //assigning "null" in empty cells of "numbers"
        for (int row = 1; row < ARRAY_SIZE; row++)
        {
            for (int col = 0; col < ARRAY_SIZE; col++)
            {
                numbers[row, col] = null;
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

        yield return StartCoroutine(break_array());

        //after we came back from the split
        yield return new WaitForSeconds(2);//wait before handling with second parts of subarrays
        yield return StartCoroutine(collectArray(length_not_zero));


        yield return null;
    }
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //main function of sorting 
    private IEnumerator break_array()
    {
       
       while (length_not_zero != 2)
        {     
            //choosing left or right subarray
            yield return StartCoroutine(chooseRightOrLeftSubarrayIE(KeyCode.Return));


            yield return StartCoroutine(divideArray());

            index++;

            yield return StartCoroutine(moveUp());
            yield return StartCoroutine(break_array());

        }
        yield return null; // wait until next frame, then continue execution from here (loop continues)
    }

   //------------------------------------------------------------------
    private IEnumerator divideArray()
    {

        if (rightSubarray)
        {
            yield return StartCoroutine(next_row_RightPart());
        }
        else
        {
            yield return StartCoroutine(next_row_LeftPart());
        }

        yield return null;
    }
    //-------------------------------------------------------------
    private IEnumerator next_row_LeftPart()
    {
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

        for (int i = start_index; i < (start_index + amount/2); i++)
        {
            numbers[index + 1, i] = numbers[index, i];
            numbers[index, i] = null;        
        }

        length_not_zero = amount / 2;
        yield return null; // wait until next frame, then continue execution from here (loop continues)
    }
    //---------------------------------------------------
    private IEnumerator next_row_RightPart()
    {
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

        int n_amount = 0;
        for (int i = (start_index + amount / 2); i < (start_index + amount); i++)
        {

            numbers[index + 1, i] = numbers[index, i];
            numbers[index, i] = null;

            n_amount++;
        }

        length_not_zero = n_amount;

        yield return null; // wait until next frame, then continue execution from here (loop continues)
    }
    //--------------------------------------------------
    private IEnumerator chooseRightOrLeftSubarrayIE(KeyCode key)
    {
        bool done = false;

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
    private IEnumerator moveUp()
    {

        var bc = this.GetComponent<MoveSubArrayUp>();
        bc.index = index;

        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            bc.numbers[index, i] = numbers[index, i];
        }

        bc.start_moving = true;
        yield return null;
    }
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //collect back the array
    private IEnumerator collectArray(int length_not_zero)
    {
        depth = index - 1; //start from second highest subarray
        while (depth >= 0)
        {

            //memorize indexes of start and ending of subarray
            int amount = 0;
            for (int i = 0; i < ARRAY_SIZE; i++)
            {
                if (numbers[index, i] != null)
                {
                    amount++;
                    if (amount == 1)
                    {
                        start_end[2] = i;
                    }

                    if (amount > 0)
                    {
                        start_end[3] = i;
                    }
                }
            }
            //finish to memorize indexes of start and ending of subarray

            yield return StartCoroutine(SecondPartUp());
            yield return StartCoroutine(moveUp());

            //now compare values in the current subarray
            yield return StartCoroutine(compareValues());

            depth--;
            yield return null;
        }

    }
    ////---------------------------------------------------------------------------------------------------------
    ////lift second part of subArray
    private IEnumerator SecondPartUp()
    {

        //memorize indexes of start and ending of subarray
        int amount = 0;
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            if (numbers[depth, i] != null)
            {
                amount++;
                if (amount == 1)
                {
                    start_end[0] = i;
                }

                if (amount > 0)
                {
                    start_end[1] = i;
                }

                numbers[index, i] = numbers[depth, i];
                numbers[depth, i] = null;
            }
          
        }
        yield return null;
    }
    //---------------------------------------------------------------------------------------------------------
    //compare values of current subArray
    private IEnumerator compareValues()
    {

        yield return StartCoroutine(waitForConfirmation(KeyCode.Return));

        yield return null;
    }
    //---------------------------------------------------------------------------------------------------------
    //confirm the rearranging of choosen array
    private IEnumerator waitForConfirmation(KeyCode key)
    {

        bool done = false;

        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop     
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
               yield return StartCoroutine(reArrangeArray());
            }
            yield return null;
        }
           
    }
    //---------------------------------------------------------
    //rearrange the choosen array 
    private IEnumerator reArrangeArray()
    {
        Debug.Log("reArrangeArray");

        var bc = this.GetComponent<MoveSubArrayUp>();
        bc.index = index;


        int i = start_end[0];//start of first subarray
        int j = start_end[2];//start of second subarray

        //
        while (i <= start_end[1] && j <= start_end[3])
        {
            GameObject temp;
            temp = numbers[index, i];
            Debug.Log("numbers[" + index + "," + i + "] is " + numbers[index, i].GetComponent<TextMesh>().text +
                " and numbers[" + index + "," + j + " ] is " + numbers[index, j].GetComponent<TextMesh>().text);
            numbers[index, i] = numbers[index, j];
            numbers[index, j] = temp;
            i++;
            j++;
        }

        if ((start_end[1] - start_end[0]) > (start_end[3] - start_end[2]))
        {
            Debug.Log("case if (start_end[1] > start_end[3])");
            while (j <= start_end[3])
            {
                numbers[index, j] = numbers[index, i];
                Debug.Log("numbers[" + index + "," + j + "] is " + numbers[index, j].GetComponent<TextMesh>().text);
                i++;
                j++;
            }
        }
        else if((start_end[1] - start_end[0]) < (start_end[3] - start_end[2]))
        {
            while (i <= start_end[2])
            {
                numbers[index, i] = numbers[index, j];
                i++;
                j++;
            }
        }

        for (int num =0; num < ARRAY_SIZE; num++)
        {
            bc.numbers[index, num] = numbers[index, num];
        }

        bc.start_moving = true;

        yield return null;
    }
    //---------------------------------------------------------
}
