using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersControllersScript : MonoBehaviour
{
    const int ARRAY_SIZE = 7, ROWS = 3;
    const int CAMERAS = 2;


    // This integers variables store the numbers
    private int[] append = new int[ARRAY_SIZE];
    private int row = 0; // row of GameObject`s array
    public GameObject[] numbers = new GameObject[ARRAY_SIZE];

    //secondary array for coping
    public GameObject[] tempArray;

    //array of GameObject`s numbers
    public GameObject[][] numbers2 = new GameObject[ARRAY_SIZE][];

    //for GIF Arrow that pointers to choosen subArray
    public GameObject ArrowPlane;

    //textMesh
    private TextMesh textMesh1;

    //cameras of the scene
    public GameObject[] camera1 = new GameObject[CAMERAS];

    //start sorting; order of values in subarray is right: from small to big
    public bool start_sort = false, rightOrder = false;

    //sign: right or left side of subarray
    public bool[] right;

    //in order to desactivate PlayerController and Animator of Ellen
    public GameObject Ellen;

    //speed
    public float speed = 1.0f;



    //------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            //place cubes with numbers
            float temp_float = UnityEngine.Random.Range(-100.0f, 100.0f);
            append[i] = (int)temp_float;
            textMesh1 = numbers[i].GetComponent<TextMesh>();
            textMesh1.text = append[i].ToString();
            right = new bool[ARRAY_SIZE];
        }
    }

    //------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {

        if (start_sort)
        {
            start_sort = false;
            //deactivate control of Ellen, because we need control array`s sort
            var ellen = Ellen.GetComponent<Gamekit3D.PlayerController>();
            var ellenAnimator = Ellen.GetComponent<Animator>();
            ellen.enabled = false;
            ellenAnimator.enabled = false;

            StartCoroutine(GameOverWin(numbers));

            //Debug.Log("End of Update");
        }
    }

    //------------------------------------------
    private IEnumerator GameOverWin(GameObject[] numbers)
    {

      
        yield return StartCoroutine(breakArray());
        yield return new WaitForSeconds(2);//wait before handling with second parts of subarrays
        yield return StartCoroutine(PlaceOnArray());

        if (numbers.Length == 1)
        {
            Debug.Log("numbers.Lengthe = 1");

       //     camera1[1].SetActive(true);
       //     camera1[0].SetActive(false);

            //activate control of Ellen
      //      var ellen = Ellen.GetComponent<Gamekit3D.PlayerController>();
     //       var ellenAnimator = Ellen.GetComponent<Animator>();
      //      ellen.enabled = true;
      //      ellenAnimator.enabled = true;

      //      this.enabled = false;
        }
        else
        {
          
            // StartCoroutine(Spawn(sac));
            yield return null;
        }
    }
    //-------------------------------------
    private IEnumerator breakArray()
    {   
        numbers2[row] = numbers;//initialization of array
    
        while (numbers2[row].Length != 1)
        {
            row++;
            yield return StartCoroutine(waitForEnterPress(KeyCode.Return, row));
            yield return StartCoroutine(rightOrLeftSubArray(row));
            yield return StartCoroutine(moveSubArray(row));
        }


        yield return null;
    }
    //-------------------------------------
    private IEnumerator PlaceOnArray()
    {
        Debug.Log("IEnumerator PlaceOnArray(), row is " + row);
        int counter = 0; //how much lift subarrays

        while (row != 0)
        {
            counter++;
            //up second part of subarray
            yield return StartCoroutine(secondPartUp(row));
            yield return StartCoroutine(rightOrLeftSubArrayUP(row));
            yield return StartCoroutine(moveSubArrayUP(counter));

            //now merge two subarrays to one
            //yield return StartCoroutine(mergeTwoArrays(row));

            yield return new WaitForSeconds(2);//wait before handling with second parts of subarrays

    
            //order two choosen subarrays from small to big
            yield return StartCoroutine(orderTwoSubArrays(row));
            row--;
        }

        yield return null;
    }
    //----------------------------------------------
    //up second part of subarray
    private IEnumerator secondPartUp(int row)
    {
        Debug.Log("secondPartUp");

        bool temp;//temporary variable for changing value of current row[i]
        temp = !right[row];
        right[row] = temp;

        yield return null;
    }
    //---------------------------------------------
    private IEnumerator rightOrLeftSubArrayUP(int row)
    {
        Debug.Log("rightOrLeftSubArrayUP");

        int prev_row;
        prev_row = row - 1; //previous row

        if (right[row])
        {
            //in case if length of array odd
            if (numbers2[prev_row].Length % 2 != 0)
            {

                tempArray = new GameObject[(numbers2[prev_row].Length / 2) + 1];
                Array.Copy(numbers2[prev_row], (numbers2[prev_row].Length / 2), tempArray, 0, ((numbers2[prev_row].Length / 2) + 1));

            }
            //in case if lenght of array is even
            else
            {
                tempArray = new GameObject[(numbers2[prev_row].Length / 2)];
                Array.Copy(numbers2[prev_row], (numbers2[prev_row].Length / 2), tempArray, 0, (numbers2[prev_row].Length / 2));
            }
        }
        else
        {
            tempArray = new GameObject[(numbers2[prev_row].Length / 2)];
            //Array.Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
            Array.Copy(numbers2[prev_row], 0, tempArray, 0, (numbers2[prev_row].Length / 2));
        }

        yield return null;
    }
    //----------------------------------------------------------
    private IEnumerator moveSubArrayUP(int counter)
    {
        Debug.Log("rightOrLeftSubArrayUP");

        var bc = this.GetComponent<MoveSubarrey>();

        bc.numbers = tempArray;

        StartCoroutine(bc.set_targets(counter));
        bc.start = true;

        yield return null;
    }

    //-------------------------------------
    //order two subarrays: small values on the left side, big on the right
    private IEnumerator orderTwoSubArrays(int row)
    {
        Debug.Log("orderTwoSubArrays");

        //yield return StartCoroutine(chooseSubArray(KeyCode.Return, row));
        yield return StartCoroutine(waitForEnterPressArrow(KeyCode.Return, row));
  //      ArrowPlane.SetActive(false); //and switch it ON
        //  yield return StartCoroutine(changePosition(row));

        yield return null;
    }
    //-------------------------------------
    //choose specific subarray that accented by arrow
    private IEnumerator waitForEnterPressArrow(KeyCode key, int row) 
    {
        bool done = false;
        Debug.Log("IEnumerator waitForEnterPressArrow");

        //put ArrowGIF above an average cell of tempArray
    //    ArrowPlane.SetActive(true); //and switch it ON
     //   ArrowPlane.transform.position = tempArray[tempArray.Length / 2].transform.position;

        while (!done) // essentially a "while true", but with a bool to break out naturally
        {

            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
                Debug.Log("Enter");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                right[row] = false;
                yield return StartCoroutine(moveSubArrayHor(row));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                yield return StartCoroutine(moveSubArrayHor(row));
                right[row] = true;
            }
           
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

    }
    //-----------------------------------------
    //change positions of subarrays
    private IEnumerator changePosition(int row)
    {


        yield return null;
    }
    //---------------------------------------------------
    //compare values of two choosen subarrays
    private IEnumerator compareTwoValues(int row)
    {
        //initialization ow two variables for comparison
        int num1, num2;
        rightOrder = false;

        int i = 0;
        int j = 0;

        while(i < tempArray.Length && i < numbers2[row].Length)
        {
            
            var vc = tempArray[i].GetComponent<TextMesh>().text;
            var bc = numbers2[row][i].GetComponent<TextMesh>().text;

            num1 = System.Int32.Parse(vc);
            num2 = System.Int32.Parse(bc);

            if (num1 > num2)
            {

            }
        }

        yield return null;
    }
    //---------------------------------------------------
    //choise is acceptable only if small subarray on tje left
    //private IEnumerator chooseSubArray(KeyCode key, int row)
    //{
    //    bool done = false;
    //    //Debug.Log("IEnumerator waitForEnterPress");

    //    while (!done) // essentially a "while true", but with a bool to break out naturally
    //    {

    //        if (Input.GetKeyDown(key))
    //        {
    //           // yield return StartCoroutine(compareTwoValues(row)); 

    //            if (rightOrder)
    //            {
    //                done = true;
    //                Debug.Log("done = true");
    //            }

    //            Debug.Log("Enter");
    //        }
    //        else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //        {

    //            right[row] = false;
    //            Debug.Log("LeftSubarray");
    //        }
    //        else if (Input.GetKeyDown(KeyCode.RightArrow))
    //        {
    //            right[row] = true;
    //            Debug.Log("RightSubarray");
    //        }

    //        yield return null; // wait until next frame, then continue execution from here (loop continues)
    //    }
    //}
    //-----------------------------------------
    //private IEnumerator mergeTwoArrays(int row)
    //{
    //    //secondary list for merging two subarrays
    //    var listOfObjects = new List<GameObject>();

    //    if (right[row])
    //    {
    //        listOfObjects.AddRange(tempArray);
    //        listOfObjects.AddRange(numbers2[row]);
    //    }
    //    else
    //    {
    //        listOfObjects.AddRange(numbers2[row]);
    //        listOfObjects.AddRange(tempArray);
    //    }

    //    numbers2[row] = listOfObjects.ToArray();

    //    yield return null;
    //}
    //------------------------------------------
    private IEnumerator waitForEnterPress(KeyCode key, int row)
    {
        bool done = false;
        //Debug.Log("IEnumerator waitForEnterPress");

        while (!done) // essentially a "while true", but with a bool to break out naturally
        {

            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
                Debug.Log("Enter");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                right[row] = false;
                Debug.Log("LeftSubarray");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right[row] = true;
                Debug.Log("RightSubarray");
            }

            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

    }

    //---------------------------------------------
   private IEnumerator rightOrLeftSubArray(int row)
    {
       
        int prev_row;
        prev_row = row-1; //previous row
        if (right[row])
        {
            //in case if length of array odd
            if (numbers2[prev_row].Length % 2 != 0)
            {

                numbers2[row] = new GameObject[(numbers2[prev_row].Length / 2) + 1];    
                Array.Copy(numbers2[prev_row], (numbers2[prev_row].Length / 2), numbers2[row], 0, ((numbers2[prev_row].Length / 2)+1));
               
            }
            //in case if lenght of array is even
            else
            {
                numbers2[row] = new GameObject[(numbers2[prev_row].Length / 2)];
                Array.Copy(numbers2[prev_row], (numbers2[prev_row].Length / 2), numbers2[row], 0, (numbers2[prev_row].Length / 2));
            }
        }
        else
        {
            numbers2[row] = new GameObject[(numbers2[prev_row].Length / 2) ];
            //Array.Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
            Array.Copy(numbers2[prev_row], 0, numbers2[row], 0, (numbers2[prev_row].Length / 2));        
        }

        yield return null;  
    }
    //----------------------------------------------------------
    private IEnumerator moveSubArrayHor(int row)
    {
        
        var bc = this.GetComponent<ChangeSubArraysPlaces>();
        bc.numbers2 = numbers2[row];
        bc.tempArray = tempArray;
        StartCoroutine(bc.set_targets_for_changing());
        

        yield return null;
    }
    //------------------------------------------------------------
    private IEnumerator moveSubArray(int row)
   {
      var bc = this.GetComponent<MoveSubarrey>();
        Debug.Log("IEnumerator moveSubArray(int row), row is " + row);
        bc.numbers = numbers2[row];
 
     StartCoroutine(bc.set_targets(1));
     bc.start = true;
       

     yield return null;

   }
    //----------------------------------------------------------
}
