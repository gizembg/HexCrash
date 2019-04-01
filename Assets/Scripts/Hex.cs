using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    //coordinats in the map array
    public int x;
    public int y;
    public Color color;
    public float distance;
    public GameObject cube;
    public IDictionary<string, Hex> neighbours;
    public IDictionary<string, Hex> GetNeighbours() //    public Hex[] GetNeighbours()
    {
        //to reach non-static object drom another script
        GameObject obj = GameObject.Find("Map"); //go Map script is attached to
        Map mapInstance = obj.GetComponent<Map>();
        int mapHeight = mapInstance.width;
        int mapWidth = mapInstance.height;

        neighbours = new Dictionary<string, Hex>();

        Hex downNeighbour = x == 0 ? null : GameObject.Find("Hex_" + (x - 1) + "_" + y).GetComponent<Hex>();
        Debug.Log("downNeighbour: " + downNeighbour);
        neighbours.Add("downNeighbour", downNeighbour);

        Hex upNeighbour = x >= mapHeight - 1 ? null : GameObject.Find("Hex_" + (x + 1) + "_" + y).GetComponent<Hex>();
        Debug.Log("upNeighbour: " + upNeighbour);
        neighbours.Add("upNeighbour", upNeighbour);

        if (y % 2 == 1)//if y odd number
        {
            Hex downLeftNeighbour = x == 0 ? null : GameObject.Find("Hex_" + (x - 1) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("downLeftNeighbour: " + downLeftNeighbour);
            neighbours.Add("downLeftNeighbour", downLeftNeighbour);

            Hex downRightNeighbour = x == 0 || y >= mapWidth - 1 ? null : GameObject.Find("Hex_" + (x - 1) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("downRightNeighbour: " + downRightNeighbour);
            neighbours.Add("downRightNeighbour", downRightNeighbour);

            Hex upLeftNeighbour = y >= mapHeight - 1 ? null : GameObject.Find("Hex_" + (x) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("upLeftNeighbour: " + upLeftNeighbour);
            neighbours.Add("upLeftNeighbour", upLeftNeighbour);

            Hex upRightNeighbour = x >= mapHeight - 1 || y >= mapWidth - 1 ? null : GameObject.Find("Hex_" + (x) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("upRightNeighbour: " + upRightNeighbour);
            neighbours.Add("upRightNeighbour", upRightNeighbour);

        }
        else
        {
            Hex downLeftNeighbour = y == 0 || x == 0 ? null : GameObject.Find("Hex_" + (x) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("downLeftNeighbour: " + downLeftNeighbour);
            neighbours.Add("downLeftNeighbour", downLeftNeighbour);

            Hex downRightNeighbour = x >= mapWidth - 1 ? null : GameObject.Find("Hex_" + (x) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("downRightNeighbour: " + downRightNeighbour);
            neighbours.Add("downRightNeighbour", downRightNeighbour);

            Hex upLeftNeighbour = x >= mapHeight - 1 || y == 0 ? null : GameObject.Find("Hex_" + (x + 1) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("upLeftNeighbour: " + upLeftNeighbour);
            neighbours.Add("upLeftNeighbour", upLeftNeighbour);

            Hex upRightNeighbour = x >= mapHeight - 1 ? null : GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("upRightNeighbour: " + upRightNeighbour);
            neighbours.Add("upRightNeighbour", upRightNeighbour);
        }

        return neighbours;
    }
}
