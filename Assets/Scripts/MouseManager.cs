using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{
    public GameObject MSelection;
    private List<Hex> selectedGroup;
    private Hex selectedHexagon;
    private GameObject cloneHex;
    private GameObject cloneMarks;
    private List<GameObject> cloneMarksList = new List<GameObject>();

    private static List<Hex> hexagonList;

    void Start()
    {
        selectedGroup = new List<Hex>();
        hexagonList = Map.hexagonList;

    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo)) //To find which object is selected.
        {
            Hex hitObject = hitInfo.collider.transform.parent.gameObject.GetComponent<Hex>();

            if (hitObject != null && hitObject.GetComponent<Hex>() != null || selectedHexagon == null)
            {
                MouseOver_Hex(hitObject);
                selectedHexagon = hitObject;
            }
        }
    }

    void MouseOver_Hex(Hex hitObject)
    {
        GameObject obj = GameObject.Find("Map"); //go Map script is attached to
        Map mapInstance = obj.GetComponent<Map>();
        int mapHeight = mapInstance.height;
        int mapWidth = mapInstance.width;

        if (Input.GetMouseButtonDown(0))
        {

            //TODO: deselction errors
            deseceltObjects();
            selectedGroup.Clear();
            selectionRule(hitObject);
            markSelectedObjects();

        }
        if (Input.GetMouseButtonUp(1))  //right click starts rotation
        {
            StartCoroutine(Rotation());

        }
    }
    void markSelectedObjects()
    {
        foreach (Hex markedHex in selectedGroup)
        {
            if (markedHex != null)
            {
                MeshRenderer mr2 = markedHex.GetComponentInChildren<MeshRenderer>();
                cloneMarks = Instantiate(MSelection, mr2.bounds.center, Quaternion.identity);
                cloneMarksList.Add(cloneMarks);


            }
        }
    }
    private void deseceltObjects()
    {
        if (cloneMarks != null && cloneMarks.transform.childCount > 0)
        {
            Debug.Log("cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc " + cloneMarks.transform.childCount);

            for (int i = 0; i < cloneMarksList.Count; ++i)
            {
                Destroy(cloneMarksList[i]);

            }

            // foreach (Transform child in cloneMarks.transform)
            // {
            //     Destroy(child.gameObject);
            // }
        }
    }

    void selectionRule(Hex hitObject)
    {
        GameObject obj = GameObject.Find("Map"); //go Map script is attached to
        Map mapInstance = obj.GetComponent<Map>();
        int mapHeight = mapInstance.width;//9
        int mapWidth = mapInstance.height;//8
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

    private IEnumerator Rotation() //Coroutine
    {
        List<GameObject> matchedHexagons = null;
        for (int i = 0; i < selectedGroup.Count; ++i)
        {
            swapHexagons();
            yield return new WaitForSeconds(0.1f);  //wait untill swapping completed.
            //matchedHexagons = checkExplosion(selectedGroup);  //now it checks only selected group. //TODO: Modify for all hexs.
            // if (matchedHexagons.Count > 0)
            // {
            //     break; //stop if there is explosion
            // }
        }
    }



    private void swapHexagons()
    {
        Hex firstHex, secondHex, thirdHex;
        Vector2 pos1, pos2, pos3;

        firstHex = selectedGroup[0];
        secondHex = selectedGroup[1];
        thirdHex = selectedGroup[2];

        pos1 = firstHex.transform.position;
        pos2 = secondHex.transform.position;
        pos3 = thirdHex.transform.position;

        Debug.Log("pos1: " + pos1);
        Debug.Log("pos2: " + pos2);
        Debug.Log("pos3: " + pos3);

        firstHex.transform.position = pos2;
        secondHex.transform.position = pos3;
        thirdHex.transform.position = pos1;

        Debug.Log("Cpos1: " + pos1);
        Debug.Log("Cpos2: " + pos2);
        Debug.Log("Cpos3: " + pos3);
    }


    public static List<GameObject> checkExplosion(List<Hex> hexagonList)
    {
        List<GameObject> explosiveList = new List<GameObject>();
        Color hexColor, upRightNeighbourColor, downRightNeighbourColor, downNeighbourColor, downLeftNeighbourColor, upLeftNeighbourColor, upNeighbourColor;

        for (int i = 0; i < hexagonList.Count; ++i)
        {
            Hex hex = hexagonList[i];
            hexColor = getColor(hexagonList[i]);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- hexColor" + hexColor);

            Hex upRightNeighbour = hex.GetNeighbours()["upRightNeighbour"];
            upRightNeighbourColor = getColor(upRightNeighbour);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- upRightNeighbourColor" + upRightNeighbourColor);

            Hex downRightNeighbour = hex.GetNeighbours()["downRightNeighbour"];
            downRightNeighbourColor = getColor(downRightNeighbour);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- downRightNeighbourColor" + downRightNeighbourColor);

            Hex downNeighbour = hex.GetNeighbours()["downNeighbour"];
            downNeighbourColor = getColor(downNeighbour);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- downNeighbourColor" + downNeighbourColor);

            Hex downLeftNeighbour = hex.GetNeighbours()["downLeftNeighbour"];
            downLeftNeighbourColor = getColor(downLeftNeighbour);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- downLeftNeighbourColor" + downLeftNeighbourColor);

            Hex upLeftNeighbour = hex.GetNeighbours()["upLeftNeighbour"];
            upLeftNeighbourColor = getColor(upLeftNeighbour);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- upLeftNeighbourColor" + upLeftNeighbourColor);

            Hex upNeighbour = hex.GetNeighbours()["upNeighbour"];
            upNeighbourColor = getColor(upNeighbour);
            Debug.Log("!!!!!!!!!!!!!!!!!COLOR- upNeighbourColor" + upNeighbourColor);




            if (hexColor == upNeighbourColor && hexColor == upRightNeighbourColor)//hexColor == upNeighbourColor && hexColor == upRightNeighbourColor
            {
                GameObject other = GameObject.Find("Hex_" + upNeighbour.x + "_" + upNeighbour.y);

                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + upNeighbour.x + "_" + upNeighbour.y));
                explosiveList.Add(GameObject.Find("Hex_" + upRightNeighbour.x + "_" + upRightNeighbour.y));
                Destroy(explosiveList[0]);
                Destroy(explosiveList[1]);
                Destroy(explosiveList[2]);

            }
            else if (hexColor == upRightNeighbourColor && hexColor == downRightNeighbourColor)
            {
                GameObject other = GameObject.Find("Hex_" + upNeighbour.x + "_" + upNeighbour.y);

                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + upNeighbour.x + "_" + upNeighbour.y));
                explosiveList.Add(GameObject.Find("Hex_" + upRightNeighbour.x + "_" + upRightNeighbour.y));
                Destroy(explosiveList[0]);
                Destroy(explosiveList[1]);
                Destroy(explosiveList[2]);

            }
            else if (hexColor == downRightNeighbourColor && hexColor == downNeighbourColor)
            {

            }
            else if (hexColor == downNeighbourColor && hexColor == downLeftNeighbourColor)
            {

            }
            else if (hexColor == downLeftNeighbourColor && hexColor == upLeftNeighbourColor)
            {

            }
            else if (hexColor == upLeftNeighbourColor && hexColor == upNeighbourColor)
            {

            }
        }
        return explosiveList;
    }
    public static Color getColor(Hex hex)
    {
        Color color;
        if (hex != null)
        {
            MeshRenderer mr = hex.GetComponentInChildren<MeshRenderer>();
            color = mr.material.color;
        }
        else
        {
            //TODO: null kontrolunu yukarıda yap
            color = Color.black;
        }

        return color;
    }

}

