﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float deltaHeight = (2 * Camera.main.orthographicSize - height * rows) / (rows + 1);

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
                    new Vector3(0, 0, 0),                       //0
                    new Vector3(width, 0, 0),                   //1
                    new Vector3(width, 0, height),              //2
                    new Vector3(0, 0, height),                  //3

                    new Vector3(width, 0, 0),                   //4
                    new Vector3(width, thickness, 0),           //5
                    new Vector3(width, thickness, height),      //6
                    new Vector3(width, 0, height),              //7

                    new Vector3(0, 0, height),                  //8
                    new Vector3(width, 0, height),              //9
                    new Vector3(width, thickness, height),      //10
                    new Vector3(0, thickness, height),          //11

                    new Vector3(0, thickness, 0),               //12
                    new Vector3(0, 0, 0),                       //13
                    new Vector3(0, 0, height),                  //14
                    new Vector3(0, thickness, height),          //15

                    new Vector3(0, thickness, 0),               //16
                    new Vector3(width, thickness, 0),           //17
                    new Vector3(width, 0, 0),                   //18
                    new Vector3(0, 0, 0),                       //19

                    new Vector3(width, thickness, 0),           //20
                    new Vector3(0, thickness, 0),               //21
                    new Vector3(0, thickness, height),          //22
                    new Vector3(width, thickness, height)       //23
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
                GameObject gameObject = new GameObject("Cards " + (cols * x + y), typeof(MeshFilter), typeof(MeshRenderer));

                //Apply mesh to gameObject
                gameObject.GetComponent<MeshFilter>().mesh = mesh;

                //Position of card
                gameObject.transform.position = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect, 0, Camera.main.orthographicSize - height);
                gameObject.transform.position += new Vector3(width * y * 4 / 3 + width / 3, 0, -deltaHeight - x * (height + deltaHeight));

                //Material Creation
                Material material = new Material(Shader.Find("Mobile/Diffuse"));

                material.mainTexture = (Texture2D)Resources.Load("Textures/Cards/" + cards[(cols * x + y)]);

                gameObject.GetComponent<MeshRenderer>().material = material;
            }
        }
    }

}
