using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersControllersScript : MonoBehaviour
{
    const int ARRAY_SIZE = 7, ROWS = 4;
    const int CAMERAS = 2;

    // This integers variables store the numbers
    private int[] append = new int[ARRAY_SIZE];
    public GameObject[] numbers = new GameObject[ARRAY_SIZE];


    //textMesh
    private TextMesh textMesh1;

    //cameras of the scene
    public GameObject[] camera1 = new GameObject[CAMERAS];

    //start sorting
    public bool start_sort = false;

    //sign: right or left side of array; given array is splitted
    public bool right, arraySplitUp = false;

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
        }
    }

    //------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {

        if (start_sort)
        {
          
            //deactivate control of Ellen, because we need control array`s sort
            var ellen = Ellen.GetComponent<Gamekit3D.PlayerController>();
            var ellenAnimator = Ellen.GetComponent<Animator>();
            ellen.enabled = false;
            ellenAnimator.enabled = false;

            StartCoroutine(GameOverWin(numbers));

            Debug.Log("End of Update");

            start_sort = false;
           
        }
    }

    //function for moving choosen subarray
    //---------------------------------------------
    void move_subArray(GameObject[] numbers, bool right)
    {
        //create variable of script "MoveSubarrey" and switch it on
        var enable_move = this.GetComponent<MoveSubarrey>();
       

        Vector3 target;//definition of new vector of target place
       
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move

        for (int i=0; i < numbers.Length; i++)
        {
            //set taret's coordinates
            if (right)
            {
             target.x = numbers[i].transform.position.x - 7.5f;
            }
            else
            {
             target.x = numbers[i].transform.position.x + 7.5f;
            }

            target.y = numbers[i].transform.position.y + 7.5f;
            target.z = numbers[i].transform.position.z;

            
            enable_move.number = numbers[i];
            enable_move.enabled = true;

        }
    }

    //------------------------------------------
    private IEnumerator GameOverWin(GameObject[] numbers) {

        Debug.Log("numbers.Length is " + numbers.Length);

        if (numbers.Length == 1)
        {
            Debug.Log("numbers.Lengthe = 1");

            camera1[1].SetActive(true);
            camera1[0].SetActive(false);

            //activate control of Ellen
            var ellen = Ellen.GetComponent<Gamekit3D.PlayerController>();
            var ellenAnimator = Ellen.GetComponent<Animator>();
            ellen.enabled = true;
            ellenAnimator.enabled = true;

            this.enabled = false;

        }
        else
        {
            // wait for player to press Enter
            yield return waitForEnterPress(KeyCode.Return);

            if (right)
            {
                //in case if length of array odd
                if (numbers.Length % 2 != 0)
                {
                    GameObject[] temp = new GameObject[(numbers.Length / 2) + 1];
                    Array.Copy(numbers, ((numbers.Length / 2) + 1), temp, 0, (numbers.Length / 2 + 1));
                    move_subArray(temp, right);
                    StartCoroutine(GameOverWin(temp));
                }
                //in case if lenght of array is even
                else
                {
                    GameObject[] temp = new GameObject[(numbers.Length / 2)];
                    Array.Copy(numbers, (numbers.Length/2), temp, 0, (numbers.Length/2));
                    move_subArray(temp, right);
                    StartCoroutine(GameOverWin(temp));
                }
            }
            else
            {
                GameObject[] temp = new GameObject[numbers.Length - ((numbers.Length / 2)+1)];
                //Array.Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
                Array.Copy(numbers, 0, temp, 0, (numbers.Length - ((numbers.Length/2)+1)));
                move_subArray(temp, right);
                StartCoroutine(GameOverWin(temp));
            }

        }
    }

    //-------------------------------------
    private IEnumerator waitForEnterPress(KeyCode key)
    {
        bool done = false;


        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
                Debug.Log("RightSubarray");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                right = false;
                Debug.Log("LeftSubarray");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right = true;
                Debug.Log("RightSubarray");
            }

            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
    }
 

 
}
