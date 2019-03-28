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

    [SerializeField] AudioSource menu;
    [SerializeField] float delay = 1f;

    void Start()
    {
        menu = GetComponent<AudioSource>();
    }

    public void BackToMenu()
    {
        menu.pitch = Random.Range(.8f, 1.2f);
        menu.Play();

        StartCoroutine(loadScene(0));
    }

    public void Play()
    {
        GameObject.Find("Start").SetActive(false);
        GameObject.Find("MEMORY").SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Easy").gameObject.SetActive(true);
        GameObject.Find("Difficoult").transform.Find("Medium").gameObject.SetActive(true);
        GameObject.Find("Difficoult").transform.Find("Hard").gameObject.SetActive(true);

        menu.pitch = Random.Range(.8f, 1.2f);
        menu.Play();
    }

    public void Easy()
    {
        PlayerPrefs.SetInt("rows", rowsEasy);
        PlayerPrefs.SetInt("cols", colsEasy);

        menu.pitch = Random.Range(.8f, 1.2f);
        menu.Play();

        GameObject.Find("Difficoult").transform.Find("Easy").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Medium").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Hard").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Loading").gameObject.SetActive(true);

        StartCoroutine(loadScene(1));
    }
    public void Medium()
    {
        PlayerPrefs.SetInt("rows", rowsMedium);
        PlayerPrefs.SetInt("cols", colsMedium);

        menu.pitch = Random.Range(.8f, 1.2f);
        menu.Play();

        GameObject.Find("Difficoult").transform.Find("Easy").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Medium").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Hard").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Loading").gameObject.SetActive(true);

        StartCoroutine(loadScene(1));
    }
    public void Hard()
    {
        PlayerPrefs.SetInt("rows", rowsHard);
        PlayerPrefs.SetInt("cols", colsHard);

        menu.pitch = Random.Range(.8f, 1.2f);
        menu.Play();

        GameObject.Find("Difficoult").transform.Find("Easy").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Medium").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Hard").gameObject.SetActive(false);
        GameObject.Find("Difficoult").transform.Find("Loading").gameObject.SetActive(true);

        StartCoroutine(loadScene(1));
    }

    IEnumerator loadScene(int index)
    {
        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(index);
    }
}
