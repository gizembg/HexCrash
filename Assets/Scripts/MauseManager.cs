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
            //TODO: Make a new selection rule(first clik,second click)
            selectedGroup.Add(hitObject);  //Add selected object and its two neighbours to a group.
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["upRightNeighbour"]);
            selectedGroup.Add(hitObject.GetComponent<Hex>().GetNeighbours()["downRightNeighbour"]);
            markSelectedObjects();
        }
    }

    void markSelectedObjects()
    {
        foreach (Hex markedHex in selectedGroup)
        {
            MeshRenderer mr2 = markedHex.GetComponentInChildren<MeshRenderer>();
            Instantiate(MSelection, mr2.bounds.center, Quaternion.identity);

        }

    }

}

