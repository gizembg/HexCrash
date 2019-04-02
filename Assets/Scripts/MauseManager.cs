using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MauseManager : MonoBehaviour
{
    public GameObject MSelection;
    private List<Hex> selectedGroup;
    private Hex selectedHexagon;
    private GameObject cloneMarks;
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

            //TODO: deselction errors
            //deseceltObjects();

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
                cloneMarks = Instantiate(MSelection, mr2.bounds.center, Quaternion.identity);
            }
        }

    }
    private void deseceltObjects()
    {
        if (cloneMarks != null && cloneMarks.transform.childCount > 0)
        {
            foreach (Transform child in cloneMarks.transform)
                Destroy(child.gameObject);

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
        StartCoroutine(Rotation());


    }


    private IEnumerator Rotation() //Coroutine
    {

        List<Hex> matchedHexagons = null;
        for (int i = 0; i < selectedGroup.Count; ++i)
        {
            swapHexagons(); //!explosiveHexagons
            yield return new WaitForSeconds(0.01f);  //wait untill swapping completed.

            matchedHexagons = checkExplosion(hexagonList);  //!checkexplosion()
            // if (matchedHexagons.Count > ZERO)
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





    public static List<Hex> checkExplosion(List<Hex> hexagonList)
    {
        List<Hex> explosiveList = new List<Hex>();

        // cube.GetComponent<MeshRenderer>().sharedMaterial.color = getColor(hex.neighbours["upNeighbour"]);

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





            if (hexColor == upNeighbourColor && hexColor == upRightNeighbourColor)
            {
                explosiveList.Add(hex);
                explosiveList.Add(hex.neighbours["upNeighbour"]);
                explosiveList.Add(hex.neighbours["upRightNeighbour"]);


                // Debug.Log("HEELLELE");
                //destroy
            }
            else if (hexColor == upRightNeighbourColor && hexColor == downRightNeighbourColor)
            {

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

