using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WriteJSON : MonoBehaviour {

    /*  public static Question quest = new Question("Заради кой дойде Христос на този свят за да спаси?"
          ,new Answer("Фарисеите",false)
          ,new Answer("Евреите",false)
          ,new Answer("Грешниците",true)
          ,new Answer("Апостолите",false));
          */
    //string json = JsonUtility.ToJson(quest);
    // Use this for initialization

    string path;
    string jsonString;
    public Text text;


	void Start () {
        path = Application.streamingAssetsPath + "/json1.json";
        jsonString = File.ReadAllText(path);
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
[System.Serializable]
public class Question
{
   public string quest;
   public string answers;

    public Question(string quest,string answers)
    {
        this.quest = quest;
        this.answers = answers;
       
    }

}
