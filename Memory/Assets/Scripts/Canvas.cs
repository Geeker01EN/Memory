using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{

    public int rowsEasy;
    public int colsEasy;

    public int rowsMedium;
    public int colsMedium;

    public int rowsHard;
    public int colsHard;


    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        GameObject.Find("Start").SetActive(false);
        GameObject.Find("MEMORY").SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Easy").gameObject.SetActive(true);
        GameObject.Find("Difficoult").transform.Find("Medium").gameObject.SetActive(true);
        GameObject.Find("Difficoult").transform.Find("Hard").gameObject.SetActive(true);
    }

    public void Easy()
    {
        PlayerPrefs.SetInt("rows", rowsEasy);
        PlayerPrefs.SetInt("cols", colsEasy);

        SceneManager.LoadScene(1);
    }
    public void Medium()
    {
        PlayerPrefs.SetInt("rows", rowsMedium);
        PlayerPrefs.SetInt("cols", colsMedium);

        SceneManager.LoadScene(1);
    }
    public void Hard()
    {
        PlayerPrefs.SetInt("rows", rowsHard);
        PlayerPrefs.SetInt("cols", colsHard);

        SceneManager.LoadScene(1);
    }
}
