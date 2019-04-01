using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MauseManager : MonoBehaviour
{
    public GameObject MSelection;
    private List<Hex> selectedGroup;
    private Hex selectedHexagon;
    void Start()
    {
        selectedGroup = new List<Hex>();
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo)) //To find which object is selected.
        {
            Hex hitObject = hitInfo.collider.transform.parent.gameObject.GetComponent<Hex>();


            if (hitObject.GetComponent<Hex>() != null || selectedHexagon == null)
            {
                MouseOver_Hex(hitObject);
                selectedHexagon = hitObject;
            }




        }
    }

    void MouseOver_Hex(Hex hitObject)
    {


        if (Input.GetMouseButtonDown(0))
        {

            selectedGroup.Clear();
            selectionRule(hitObject);
            markSelectedObjects();
        }
    }

    void markSelectedObjects()
    {
        foreach (Hex markedHex in selectedGroup)
        {
            if (markedHex != null)
            {
                MeshRenderer mr2 = markedHex.GetComponentInChildren<MeshRenderer>();
                Instantiate(MSelection, mr2.bounds.center, Quaternion.identity);
            }
        }

    }

    void selectionRule(Hex hitObject)
    {

        GameObject obj = GameObject.Find("Map"); //go Map script is attached to
        Map mapInstance = obj.GetComponent<Map>();
        int mapHeight = mapInstance.width;//9
        int mapWidth = mapInstance.height;//8

        Debug.Log("mapHeight: " + mapHeight);
        Debug.Log("mapWidth: " + mapWidth);
        Debug.Log("hitObject.x : " + hitObject.x);
        Debug.Log("hitObject.y:  " + hitObject.y);

        string pos = "";

        if (hitObject.x == mapHeight - 1 && hitObject.y == mapWidth - 1) //topright corner
        {
            pos = "pos5";
        }
        else if (hitObject.x == mapWidth - 1 && hitObject.y == 0) //topleft corner
        {
            pos = "pos3";
        }
        else if (hitObject.x == 0 && hitObject.y == mapWidth - 1) //downrightcorner
        {
            pos = "pos6";
        }
        else if (hitObject.x == mapHeight - 1)
        {
            pos = "pos3";

        }
        else if (hitObject.y == 0)
        {
            pos = "pos2";
        }
        else if (hitObject.y == mapWidth - 1)
        {
            pos = "pos5";
        }
        else if (hitObject.x == 0)
        {
            pos = "pos1";
        }
        else
        {
            pos = "pos2";

        }

        setSelectionPositions(pos, hitObject);
    }

    void setSelectionPositions(string pos, Hex hitObject)
    {
        if (pos == "pos1")
        {
            Debug.Log("pos1");
            selectedGroup.Add(hitObject);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upRightNeighbour"]);
        }
        else if (pos == "pos2")
        {
            selectedGroup.Add(hitObject);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upRightNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downRightNeighbour"]);
            Debug.Log("pos2");

        }
        else if (pos == "pos3")
        {
            selectedGroup.Add(hitObject);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downRightNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downNeighbour"]);
            Debug.Log("pos3");

        }
        else if (pos == "pos4")
        {

            selectedGroup.Add(hitObject);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downLeftNeighbour"]);
            Debug.Log("pos4");

        }
        else if (pos == "pos5")
        {
            selectedGroup.Add(hitObject);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downLeftNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upLeftNeighbour"]);
            Debug.Log("pos5");

        }
        else if (pos == "pos6")
        {
            selectedGroup.Add(hitObject);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upLeftNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upNeighbour"]);
            Debug.Log("pos6");

        }


    }

}

