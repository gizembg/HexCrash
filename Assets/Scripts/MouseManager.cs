using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public Text score;
    //public GameObject gameOver;
    public GameObject gameOverPanel;

    public GameObject MSelection;
    private List<Hex> selectedGroup;
    private Hex selectedHexagon;
    private GameObject cloneHex;
    private GameObject cloneMarkedHex;
    private List<GameObject> cloneMarkedHexList = new List<GameObject>();
    private int gameScore = 0;
    private static List<Hex> hexagonList;
    private int mouseClick = 0;
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
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Hitted: " + hitObject.x + hitObject.y);
            deseceltObjects();
            selectedGroup.Clear();
            selectionRule(hitObject);
            markSelectedObjects();

            mouseClick = mouseClick + 1;
            checkGameOver();
        }
        if (Input.GetMouseButtonUp(1))  //right click starts rotation
        {
            StartCoroutine(Rotation());
        }



        //mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                deseceltObjects();
                selectedGroup.Clear();
                selectionRule(hitObject);
                markSelectedObjects();

                mouseClick = mouseClick + 1;
                checkGameOver();
            }
            if (touch.phase == TouchPhase.Moved)
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
                    cloneMarkedHex = Instantiate(MSelection, mr2.bounds.center, Quaternion.identity);
                    cloneMarkedHexList.Add(cloneMarkedHex);
                }
            }
        }
        void deseceltObjects()
        {
            if (cloneMarkedHex != null && cloneMarkedHex.transform.childCount > 0)
            {
                for (int i = 0; i < cloneMarkedHexList.Count; ++i)
                {
                    Destroy(cloneMarkedHexList[i]);
                }
            }
        }
        void selectionRule(Hex hitObj)
        {
            GameObject obj = GameObject.Find("Map"); //go Map script is attached to
            Map mapInstance = obj.GetComponent<Map>();
            int mapHeight = mapInstance.height;//9
            int mapWidth = mapInstance.width;//8
            string pos = "";

            if (hitObj.x == mapHeight - 1 && hitObj.y == mapWidth - 1) //topright corner
            {
                pos = "pos5";
            }
            else if (hitObj.x == mapWidth - 1 && hitObj.y == 0) //topleft corner
            {
                pos = "pos3";
            }
            else if (hitObj.x == 0 && hitObj.y == mapWidth - 1) //downrightcorner
            {
                pos = "pos6";
            }
            else if (hitObj.x == mapHeight - 1)
            {
                pos = "pos3";
            }
            else if (hitObj.y == 0)
            {
                pos = "pos2";
            }
            else if (hitObj.y == mapWidth - 1)
            {
                pos = "pos5";
            }
            else if (hitObj.x == 0)
            {
                pos = "pos1";
            }
            else
            {
                pos = "pos2";
            }
            setSelectionPositions(pos, hitObj);
        }
        void setSelectionPositions(string pos, Hex hitObj)
        {
            if (pos == "pos1")
            {
                selectedGroup.Add(hitObj);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["upNeighbor"]);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["upRightNeighbor"]);
            }
            else if (pos == "pos2")
            {
                selectedGroup.Add(hitObj);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["upRightNeighbor"]);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["downRightNeighbor"]);
            }
            else if (pos == "pos3")
            {
                selectedGroup.Add(hitObj);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["downRightNeighbor"]);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["downNeighbor"]);
            }
            else if (pos == "pos4")
            {
                selectedGroup.Add(hitObj);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["downNeighbor"]);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["downLeftNeighbor"]);
            }
            else if (pos == "pos5")
            {
                selectedGroup.Add(hitObj);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["downLeftNeighbor"]);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["upLeftNeighbor"]);
            }
            else if (pos == "pos6")
            {
                selectedGroup.Add(hitObj);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["upLeftNeighbor"]);
                selectedGroup.Add(hitObj.GetComponent<Hex>().GetNeighbors()["upNeighbor"]);
            }
        }

        IEnumerator Rotation() //Coroutine
        {
            List<GameObject> matchedHexagons = null;
            for (int i = 0; i < selectedGroup.Count; ++i)
            {
                swapHexagons();
                yield return new WaitForSeconds(0.1f);  //wait untill swapping completed.
                matchedHexagons = checkExplosion(selectedGroup[i]);  //now it checks only selected group. //TODO: Modify for all hexs.

                if (matchedHexagons.Count == 3)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        Destroy(matchedHexagons[j]);
                        deseceltObjects();  //deselect when object destroyed
                        gameScore = gameScore + 10;
                        Debug.Log("scorree" + gameScore);
                        score.text = gameScore.ToString();


                    }
                }

                if (matchedHexagons.Count > 0)
                {
                    break; //stop if there is explosion
                }
            }
        }
        void swapHexagons()
        {
            Hex firstHex, secondHex, thirdHex;
            Vector2 pos1, pos2, pos3;

            firstHex = selectedGroup[0];
            secondHex = selectedGroup[1];
            thirdHex = selectedGroup[2];

            pos1 = firstHex.transform.position;
            pos2 = secondHex.transform.position;
            pos3 = thirdHex.transform.position;

            firstHex.transform.position = pos2;
            secondHex.transform.position = pos3;
            thirdHex.transform.position = pos1;

        }


        List<GameObject> checkExplosion(Hex hex)
        {
            List<GameObject> explosiveList = new List<GameObject>();
            Color hexColor, upRightNeighborColor, downRightNeighborColor, downNeighborColor, downLeftNeighborColor, upLeftNeighborColor, upNeighborColor;

            hexColor = getColor(hex);

            Hex upRightNeighbor = hex.GetNeighbors()["upRightNeighbor"];
            upRightNeighborColor = getColor(upRightNeighbor);

            Hex downRightNeighbor = hex.GetNeighbors()["downRightNeighbor"];
            downRightNeighborColor = getColor(downRightNeighbor);

            Hex downNeighbor = hex.GetNeighbors()["downNeighbor"];
            downNeighborColor = getColor(downNeighbor);

            Hex downLeftNeighbor = hex.GetNeighbors()["downLeftNeighbor"];
            downLeftNeighborColor = getColor(downLeftNeighbor);

            Hex upLeftNeighbor = hex.GetNeighbors()["upLeftNeighbor"];
            upLeftNeighborColor = getColor(upLeftNeighbor);

            Hex upNeighbor = hex.GetNeighbors()["upNeighbor"];
            upNeighborColor = getColor(upNeighbor);

            if (hexColor == upNeighborColor && hexColor == upRightNeighborColor)//hexColor == upNeighborColor && hexColor == upRightNeighborColor
            {
                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + upNeighbor.x + "_" + upNeighbor.y));
                explosiveList.Add(GameObject.Find("Hex_" + upRightNeighbor.x + "_" + upRightNeighbor.y));
            }
            else if (hexColor == upRightNeighborColor && hexColor == downRightNeighborColor)
            {
                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + upRightNeighbor.x + "_" + upRightNeighbor.y));
                explosiveList.Add(GameObject.Find("Hex_" + downRightNeighbor.x + "_" + downRightNeighbor.y));
            }
            else if (hexColor == downRightNeighborColor && hexColor == downNeighborColor)
            {
                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + downRightNeighbor.x + "_" + downRightNeighbor.y));
                explosiveList.Add(GameObject.Find("Hex_" + downNeighbor.x + "_" + downNeighbor.y));
            }
            else if (hexColor == downNeighborColor && hexColor == downLeftNeighborColor)
            {
                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + downNeighbor.x + "_" + downNeighbor.y));
                explosiveList.Add(GameObject.Find("Hex_" + downLeftNeighbor.x + "_" + downLeftNeighbor.y));
            }
            else if (hexColor == downLeftNeighborColor && hexColor == upLeftNeighborColor)
            {
                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + downLeftNeighbor.x + "_" + downLeftNeighbor.y));
                explosiveList.Add(GameObject.Find("Hex_" + upLeftNeighbor.x + "_" + upLeftNeighbor.y));
            }
            else if (hexColor == upLeftNeighborColor && hexColor == upNeighborColor)
            {
                explosiveList.Add(GameObject.Find("Hex_" + hex.x + "_" + hex.y));
                explosiveList.Add(GameObject.Find("Hex_" + upLeftNeighbor.x + "_" + upLeftNeighbor.y));
                explosiveList.Add(GameObject.Find("Hex_" + upNeighbor.x + "_" + upNeighbor.y));
            }

            return explosiveList;
        }
        Color getColor(Hex hex)
        {
            Color color;
            if (hex != null)
            {
                MeshRenderer mr = hex.GetComponentInChildren<MeshRenderer>();
                color = mr.material.color;
            }
            else
            {
                color = Color.black;
            }

            return color;
        }

        void checkGameOver()
        {
            if (mouseClick >= 10)
            {
                gameOverPanel.SetActive(true);
            }
        }

    }
}

