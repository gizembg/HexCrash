﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject hexPrefab;
    int width = 9;
    int height = 8;
    float xOffset = 0.879f;
    float yOffset = 0.764f;
    public static List<Hex> hexagonList = new List<Hex>();
    //private List<List<Hex>> hexGrid;

    void Start()
    {
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

                Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

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
