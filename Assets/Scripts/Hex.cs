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
    public IDictionary<string, Hex> neighbors;
    public IDictionary<string, Hex> GetNeighbors() //    public Hex[] GetNeighbors()
    {
        //to reach non-static object drom another script
        GameObject obj = GameObject.Find("Map"); //go Map script is attached to
        Map mapInstance = obj.GetComponent<Map>();
        int mapHeight = mapInstance.height;
        int mapWidth = mapInstance.width;
        neighbors = new Dictionary<string, Hex>();

        Hex downNeighbor = x == 0 ? null : GameObject.Find("Hex_" + (x - 1) + "_" + y).GetComponent<Hex>();
        neighbors.Add("downNeighbor", downNeighbor);

        Hex upNeighbor = x >= mapHeight - 1 ? null : GameObject.Find("Hex_" + (x + 1) + "_" + y).GetComponent<Hex>();
        neighbors.Add("upNeighbor", upNeighbor);

        if (y % 2 == 1)//if y odd number
        {
            Hex downLeftNeighbor = x == 0 ? null : GameObject.Find("Hex_" + (x - 1) + "_" + (y - 1)).GetComponent<Hex>();
            neighbors.Add("downLeftNeighbor", downLeftNeighbor);
            Hex downRightNeighbor = x == 0 || y >= mapWidth - 1? null : GameObject.Find("Hex_" + (x - 1) + "_" + (y + 1)).GetComponent<Hex>();
            neighbors.Add("downRightNeighbor", downRightNeighbor);
            Hex upLeftNeighbor = y >= mapHeight - 1? null : GameObject.Find("Hex_" + (x) + "_" + (y - 1)).GetComponent<Hex>();
            neighbors.Add("upLeftNeighbor", upLeftNeighbor);
            Hex upRightNeighbor = x >= mapHeight - 1 || y >= mapWidth - 1? null : GameObject.Find("Hex_" + (x) + "_" + (y + 1)).GetComponent<Hex>();
            neighbors.Add("upRightNeighbor", upRightNeighbor);

        }
        else
        {
            Hex downLeftNeighbor = y == 0 || x == 0 ? null : GameObject.Find("Hex_" + (x) + "_" + (y - 1)).GetComponent<Hex>();
            //Debug.Log("downLeftNeighbor: " + downLeftNeighbor);
            neighbors.Add("downLeftNeighbor", downLeftNeighbor);

            Hex downRightNeighbor = GameObject.Find("Hex_" + (x) + "_" + (y + 1)).GetComponent<Hex>();
            //Debug.Log("downRightNeighbor: " + downRightNeighbor);
            neighbors.Add("downRightNeighbor", downRightNeighbor);

            Hex upLeftNeighbor = x >= mapHeight - 1 || y == 0 ? null : GameObject.Find("Hex_" + (x + 1) + "_" + (y - 1)).GetComponent<Hex>();
            neighbors.Add("upLeftNeighbor", upLeftNeighbor);

            Hex upRightNeighbor = x >= mapHeight - 1 || GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1)).GetComponent<Hex>() == null ? null : GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1)).GetComponent<Hex>();
            neighbors.Add("upRightNeighbor", upRightNeighbor);
        }

        return neighbors;
    }

}
