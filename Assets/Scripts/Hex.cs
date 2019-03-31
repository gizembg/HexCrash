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
    public IDictionary<string, Hex> GetNeighbours()   //To save neighbours of a hex.
    {
        neighbours = new Dictionary<string, Hex>();

        Hex downNeighbour = GameObject.Find("Hex_" + (x - 1) + "_" + y).GetComponent<Hex>();
        Debug.Log("downNeighbour: " + downNeighbour);
        neighbours.Add("downNeighbour", downNeighbour);

        Hex upNeighbour = GameObject.Find("Hex_" + (x + 1) + "_" + y).GetComponent<Hex>();
        Debug.Log("upNeighbour: " + upNeighbour);
        neighbours.Add("upNeighbour", upNeighbour);

        if (y % 2 == 1)
        {  //if y odd number
            Hex downLeftNeighbour = GameObject.Find("Hex_" + (x - 1) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("downLeftNeighbour: " + downLeftNeighbour);
            neighbours.Add("downLeftNeighbour", downLeftNeighbour);

            Hex downRightNeighbour = GameObject.Find("Hex_" + (x - 1) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("downRightNeighbour: " + downRightNeighbour);
            neighbours.Add("downRightNeighbour", downRightNeighbour);

            Hex upLeftNeighbour = GameObject.Find("Hex_" + (x) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("upLeftNeighbour: " + upLeftNeighbour);
            neighbours.Add("upLeftNeighbour", upLeftNeighbour);

            Hex upRightNeighbour = GameObject.Find("Hex_" + (x) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("upRightNeighbour: " + upRightNeighbour);
            neighbours.Add("upRightNeighbour", upRightNeighbour);
        }
        else
        {
            Hex downLeftNeighbour = GameObject.Find("Hex_" + (x) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("downLeftNeighbour: " + downLeftNeighbour);
            neighbours.Add("downLeftNeighbour", downLeftNeighbour);

            Hex downRightNeighbour = GameObject.Find("Hex_" + (x) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("downRightNeighbour: " + downRightNeighbour);
            neighbours.Add("downRightNeighbour", downRightNeighbour);

            Hex upLeftNeighbour = GameObject.Find("Hex_" + (x + 1) + "_" + (y - 1)).GetComponent<Hex>();
            Debug.Log("upLeftNeighbour: " + upLeftNeighbour);
            neighbours.Add("upLeftNeighbour", upLeftNeighbour);

            Hex upRightNeighbour = GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1)).GetComponent<Hex>();
            Debug.Log("upRightNeighbour: " + upRightNeighbour);
            neighbours.Add("upRightNeighbour", upRightNeighbour);
        }

        return neighbours;
    }

}
