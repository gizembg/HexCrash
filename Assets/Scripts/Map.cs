﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject hexPrefab;
    public int width = 9;
    public int height = 8;
    float xOffset = 0.879f;
    float yOffset = 0.764f;
    public static List<Hex> hexagonList = new List<Hex>();
    public int colorCount = 5;

    public Color color1 = Color.red;
    public Color color2 = Color.yellow;
    public Color color3 = Color.magenta;
    public Color color4 = Color.blue;
    public Color color5 = Color.green;


    //private List<List<Hex>> hexGrid;

    void Start()
    {
        List<Color> colorList = new List<Color>();
        colorList.Add(color1);
        colorList.Add(color2);
        colorList.Add(color3);
        colorList.Add(color4);
        colorList.Add(color5);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x;
                if (y % 2 == 0)
                { //even row
                    xPos += xOffset / 2f;
                }
                //hexGO.transform.Find("hex").GetComponent<MeshRenderer>().material.color =  Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                //Instantiate(hexPrefab, new Vector3(xPos,y * yOffset, 90), Quaternion.identity);
                GameObject hexGO = (GameObject)Instantiate(hexPrefab, new Vector2(y * yOffset, xPos), Quaternion.Euler(0, 0, 90)) as GameObject;

                hexGO.name = "Hex_" + x + "_" + y;
                hexGO.GetComponent<Hex>().x = x;
                hexGO.GetComponent<Hex>().y = y;
                hexGO.transform.SetParent(this.transform);
                hexGO.isStatic = true;
                Color color = colorList[Random.Range(1, colorList.Count)];
                //Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

                hexGO.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;  //hex
                hexGO.GetComponent<Hex>().color = color;  //Hex_x_y
                hexagonList.Add(hexGO.GetComponent<Hex>());

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}

