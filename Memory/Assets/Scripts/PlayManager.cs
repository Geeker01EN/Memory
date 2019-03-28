using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{

    private int numberofCards;
    private int[] cards;

    [SerializeField] private int rows;
    [SerializeField] private int cols;

    private float ratio = 62 / 49;
    private float thickness = .1f;

    void Start()
    {

        rows = PlayerPrefs.GetInt("rows");
        cols = PlayerPrefs.GetInt("cols");

        numberofCards = rows * cols;

        cards = new int[numberofCards];

        for (int i = 0; i < numberofCards/2; i++)
        {
            cards[i] = i;
            cards[i + numberofCards / 2] = i;
        }

        //Knuth shuffle algorithm
        for (int i = 0; i < numberofCards; i++)
        {
            int tmp = cards[i];
            int rnd = Random.Range(i,numberofCards);
            cards[i] = cards[rnd];
            cards[rnd] = tmp;
        }

        //Generate cards model
        float width = (6 * Camera.main.orthographicSize * Camera.main.aspect) / (4 * cols + 1);
        float height = width * ratio;
        float deltaHeight = (2 * Camera.main.orthographicSize * 9/10 - height * rows) / (rows + 1);

        //Creating mesh
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                //Creating mesh object
                Mesh mesh = new Mesh();

                //Arrays of mesh properties
                Vector3[] vertices =
                {
                    //Vertices position
                    new Vector3(-width/2, -thickness/2, -height/2),                         //0
                    new Vector3(width/2, -thickness/2, -height/2),                          //1
                    new Vector3(width/2, -thickness/2, height/2),                           //2
                    new Vector3(-width/2, -thickness/2, height/2),                          //3

                    new Vector3(width/2, -thickness/2, -height/2),                          //4
                    new Vector3(width/2, thickness/2, -height/2),                           //5
                    new Vector3(width/2, thickness/2, height/2),                            //6
                    new Vector3(width/2, -thickness/2, height/2),                           //7

                    new Vector3(-width/2, -thickness/2, height/2),                          //8
                    new Vector3(width/2, -thickness/2, height/2),                           //9
                    new Vector3(width/2, thickness/2, height/2),                            //10
                    new Vector3(-width/2, thickness/2, height/2),                           //11

                    new Vector3(-width/2, thickness/2, -height/2),                          //12
                    new Vector3(-width/2, -thickness/2, -height/2),                         //13
                    new Vector3(-width/2, -thickness/2, height/2),                          //14
                    new Vector3(-width/2, thickness/2, height/2),                           //15

                    new Vector3(-width/2, thickness/2, -height/2),                          //16
                    new Vector3(width/2, thickness/2, -height/2),                           //17
                    new Vector3(width/2, -thickness/2, -height/2),                          //18
                    new Vector3(-width/2, -thickness/2, -height/2),                         //19

                    new Vector3(width/2, thickness/2, -height/2),                           //20
                    new Vector3(-width/2, thickness/2, -height/2),                          //21
                    new Vector3(-width/2, thickness/2, height/2),                           //22
                    new Vector3(width/2, thickness/2, height/2)                             //23
                };
                int[] tris =
                {
                    //Bottom
                    0, 1, 2,
                    2, 3, 0,
                    //Right side
                    4, 5, 6,
                    6, 7, 4,
                    //Up side
                    8, 9, 10,
                    10, 11, 8,
                    //Left side
                    12, 13, 14,
                    14, 15, 12,
                    //Down side
                    16, 17, 18,
                    18, 19, 16,
                    //Top
                    20, 21, 22,
                    22, 23, 20
                };
                Vector2[] uvs =
                {
                    //Bottom
                    new Vector2(.99f, 0),
                    new Vector2(.5f, 0),
                    new Vector2(.5f, .62f),
                    new Vector2(.99f, .62f),
                    //Right
                    new Vector2(.8f, 0),
                    new Vector2(.7f, 0),
                    new Vector2(.7f, .49f),
                    new Vector2(.8f, .49f),
                    //Up
                    new Vector2(.8f, 0),
                    new Vector2(.7f, 0),
                    new Vector2(.7f, .49f),
                    new Vector2(.8f, .49f),
                    //Left
                    new Vector2(.8f, 0),
                    new Vector2(.7f, 0),
                    new Vector2(.7f, .49f),
                    new Vector2(.8f, .49f),
                    //Down
                    new Vector2(.8f, 0),
                    new Vector2(.7f, 0),
                    new Vector2(.7f, .49f),
                    new Vector2(.8f, .49f),
                    //Top
                    new Vector2(.49f, 0),
                    new Vector2(0, 0),
                    new Vector2(0, .62f),
                    new Vector2(.49f, .62f),
                };

                //Applying mesh property
                mesh.vertices = vertices;
                mesh.uv = uvs;
                mesh.triangles = tris;
                mesh.RecalculateNormals();

                //Creation of the gameObject
                GameObject gameObject = new GameObject("Cards " + (cols * x + y), typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider), typeof(Animator), typeof(CardScript), typeof(AudioSource));

                //Apply mesh to gameObject
                gameObject.GetComponent<MeshFilter>().mesh = mesh;

                //Position of card
                gameObject.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect, 0, Camera.main.orthographicSize * 9/10 - height);
                gameObject.transform.position += new Vector3(width * y * 4 / 3 + width / 3 + width/2, thickness/2, -deltaHeight - x * (height + deltaHeight) + height/2);

                //Material Creation
                Material material = new Material(Shader.Find("Mobile/Diffuse"));

                material.mainTexture = (Texture2D)Resources.Load("Textures/Cards/" + cards[(cols * x + y)]);

                gameObject.GetComponent<MeshRenderer>().material = material;

                gameObject.GetComponent<BoxCollider>().size = new Vector3(width,thickness,height);
            }
        }
    }

    int alreadyActiveIndex = -1;
    public bool alreadyActiveCard = false;
    public bool animationRunning = false;
    int points = 0;

    IEnumerator FacingDown(int firstCard, int secondCard)
    {
        yield return new WaitForSecondsRealtime(1.2f);

        GameObject.Find("Cards " + firstCard).GetComponent<CardScript>().FaceDown();
        GameObject.Find("Cards " + secondCard).GetComponent<CardScript>().FaceDown();

        animationRunning = false;
    }

    public void UpdateState(int cardIndex)
    {
        animationRunning = true;

        if (alreadyActiveCard)
        {
            if (alreadyActiveIndex != cardIndex)
            {
                if (cards[alreadyActiveIndex] == cards[cardIndex])
                {
                    Destroy(GameObject.Find("Cards " + cardIndex).GetComponent<BoxCollider>());
                    Destroy(GameObject.Find("Cards " + alreadyActiveIndex).GetComponent<BoxCollider>());

                    points += 2;

                    animationRunning = false;
                }
                else
                {
                    GameObject.Find("Cards " + cardIndex).GetComponent<CardScript>().selected = false;
                    GameObject.Find("Cards " + alreadyActiveIndex).GetComponent<CardScript>().selected = false;

                    StartCoroutine(FacingDown(cardIndex, alreadyActiveIndex));
                }
            }

            alreadyActiveCard = false;
        }
        else
        {
            alreadyActiveCard = true;
            alreadyActiveIndex = cardIndex;

            animationRunning = false;
        }

        if (points >= numberofCards)
        {
            SceneManager.LoadScene(2);
        }
    }
}
