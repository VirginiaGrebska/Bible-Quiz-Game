using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Този клас съдържа методи, чрез които се започва нова игра,
//нулира се резултата, продължава се нивото и зареждане на дадена тема
public class startScript : MonoBehaviour
{
    public static int counter;
    public static int level;
    //public GameObject TextHelp;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        /* if(counter==1)
         {
             TextHelp.SetActive(true);
         }
         else if(counter==0)
         {
             TextHelp.SetActive(false);
         }*/

    }
    public void SubThemes()
    {
        SceneManager.LoadScene("SubThemes");
    }
	public void SubThemesNewLife()
	{
		SceneManager.LoadScene ("SubThemesNewLife");
	}
	public void NewLife1()
	{
		level = 4;
		ScoreManager.score = 0;
		counter = 1;
		SceneManager.LoadScene ("Levels/Level"+level.ToString()+"/"+ buttonsScript.count.ToString());
	}
	public void NewLife2()
	{
		level = 5;
		ScoreManager.score = 0;
		counter = 1;
		SceneManager.LoadScene ("Levels/Level"+level.ToString()+"/"+ buttonsScript.count.ToString());
	}
    public void StartNewQuiz()
    {
    
        ScoreManager.score = 0;
        counter = 1;
		SceneManager.LoadScene("Levels/Level"+level.ToString()+"/"+ buttonsScript.count.ToString());
        
    }
    public void ContinueQuiz()
    {
       
        SceneManager.LoadScene("Levels/Level" + level.ToString() + "/" + buttonsScript.count.ToString());
    }
    int RandomGenerator(int num)
    {
        Random.Range(0,num);
        return num;
    }
    public void ThemeStart()
    {
        SceneManager.LoadScene("Themes");
    }
    public void TheSinStart()
    {
        level = 2;
        SceneManager.LoadScene("thesin");
    }
    public void CreationStart()
    {
        level = 1;
        SceneManager.LoadScene("creation");
    }
    public void SalvationStart()
    {
        level = 3;
        SceneManager.LoadScene("salvation");
    }
    public void Menu()
	{
		SceneManager.LoadScene("menu");
	}
    public void Creation1Start()
    {
        if (counter == 1)
        {
            counter -= 1;
            SceneManager.LoadScene("creation1");
        }
    }
    public void TheSin1Start()
    {
        if (counter == 1)
        {
            counter -= 1;
            SceneManager.LoadScene("thesin1");
        }
    }
}
