using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// Това е клас, който предоставя функции за различни бутони от играта, като финкция 
// за отчинате на верен и такава, която отчина грешен отговор, както и Update() функция за отмерване на време 
public class buttonsScript : MonoBehaviour {

    static List<int> randomNumbers = new List<int>();
    public static int count = 1;
     bool nextScene = false;
  //  float ansTimer = 50;
   // public static bool keepTiming = true;
   // public bool pressRight = false;

    // Use this for initialization
    void Start () {
        //for (int i = 1; i < 11; i++)
          //  randomNumbers.Add(i);
    }

	
	// Update is called once per frame
    void Update()
    {

      
        /* if(keepTiming=true)
         { TimerManager.Timer -= 1 * Time.smoothDeltaTime; }
         */


        /* if (TimerManager.Timer <= 0)
         {
             SceneManager.LoadScene("Levels/Level" + startScript.level.ToString() + "/" + count.ToString());
             //Application.LoadLevel("end");
         */
    }
    
    /*
    int PickNumber()
     {
		if (randomNumbers.Count <= 0)
			SceneManager.LoadScene ("end");
            //Application.LoadLevel("end"); 
        int index = Random.Range(1, randomNumbers.Count);
        int value = randomNumbers[index];
        randomNumbers.RemoveAt(index);
         return value;
     }
     */
public void CheckRight()
    {
		this.GetComponent<Image> ().color = Color.green;
       
        // isRightText[1].SetActive(true);

        nextScene = true;
       // keepTiming = false;
      //  pressRight = true;
        if (nextScene)
        {
            ScoreManager.score += 10;
            Debug.Log("Green");
            if (count < 14)
            {
                count++;
                //keepTiming = true;
                Debug.Log("Enter");
                Debug.Log(count.ToString());
                SceneManager.LoadScene("Levels/Level" + startScript.level.ToString() + "/" + count.ToString());
                //Application.LoadLevel(random);
            }
            else
            {
                count = 1;
                //startScript.counter = 1;
                SceneManager.LoadScene("end");
                //Application.LoadLevel("end");
            }
        }


        //int random = PickNumber();

    }

    public void CheckWrong()
    {

		this.GetComponent<Image> ().color = Color.red;
        if(ScoreManager.score <= 0)
        {
            ScoreManager.score = 0;
        }
        else
        {
            ScoreManager.score -= 10;
        }
        
    }
}

