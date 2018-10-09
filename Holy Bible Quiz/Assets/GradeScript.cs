using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Този клас определя крайната оценка за дадено изкарано ниво: Слабо, Добро, Отлично
public class GradeScript : MonoBehaviour {

    public GameObject gradeExcellent;
    public GameObject gradeGood;
    public GameObject gradeLow;

    // Use this for initialization
    void Start () {
        gradeLow.SetActive(false);
        gradeGood.SetActive(false);
        gradeExcellent.SetActive(false);
        if (startScript.counter == 1)
        {
            ScoreManager.score += 10;
        }
    }
    // Update is called once per frame
    void Update () {
     
		if (ScoreManager.score < 70)
        {
            gradeLow.SetActive(true);
        }
        else if(70 <= ScoreManager.score && ScoreManager.score < 100)
        {
            gradeGood.SetActive(true);
        }
        else if(ScoreManager.score >= 100)
        {
            gradeExcellent.SetActive(true);
        }
	}
    
}
