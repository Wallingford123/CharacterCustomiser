using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorUIController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> selections, colours;
    [SerializeField]
    private Text partText;

    [SerializeField]
    private GameObject darkener;
    
    public List<Color> colourList;

    [HideInInspector]
    public int currentID = 0;
    [HideInInspector]
    public List<int> colourID;
    private List<ImageGlow> colourGlows, arrowGlows;


    [HideInInspector]
    public int currentColumn = 0;

    public bool useColour;

    public void VerticalChange(int _direction)
    {
        switch (currentColumn) {
            case 0:
                selections[currentID].SetActive(false);
                if(useColour)
                    colours[colourID[currentID]].SetActive(false);
                currentID += (int)Mathf.Sign(_direction);
                if (currentID >= selections.Count) currentID = 0;
                if (currentID < 0) currentID = selections.Count - 1;

                selections[currentID].SetActive(true);
                if (useColour)
                {
                    Debug.Log(colourID.Count);
                    colours[colourID[currentID]].SetActive(true);
                }
                partText.text = selections[currentID].name;
                break;
            case 1:
                colours[colourID[currentID]].SetActive(false);
                colourGlows[colourID[currentID]].enabled = !colourGlows[colourID[currentID]].enabled;
                colourID[currentID] += (int)Mathf.Sign(_direction);

                if (colourID[currentID] >= colours.Count) colourID[currentID] = 0;
                if (colourID[currentID] < 0) colourID[currentID] = colours.Count - 1;

                colourGlows[colourID[currentID]].enabled = !colourGlows[colourID[currentID]].enabled;
                colours[colourID[currentID]].SetActive(true);
                break;
        }
    }
    public void HorizontalChange(int _direction)
    {
        currentColumn += _direction;
        if (currentColumn > 1) currentColumn = 0;
        if (currentColumn < 0) currentColumn = 1;
        colourGlows[colourID[currentID]].enabled = !colourGlows[colourID[currentID]].enabled;
        arrowGlows[currentID * 2].enabled = !arrowGlows[currentID * 2].enabled;
        arrowGlows[(currentID * 2)+1].enabled = !arrowGlows[currentID * 2+1].enabled;
    }
    // Start is called before the first frame update
    void Awake()
    {
        colourID = new List<int>();
        colourGlows = new List<ImageGlow>();
        foreach (GameObject g in selections)
        {
            colourID.Add(Random.Range(0,colourList.Count));
        }
        foreach(GameObject g in colours)
        {
            colourGlows.Add(g.GetComponent<ImageGlow>());
        }
        selections[currentID].SetActive(true);
        if (useColour)
            colours[colourID[currentID]].SetActive(true);
    }
    private void Start()
    {
        arrowGlows = new List<ImageGlow>();
        foreach (GameObject g in selections)
        {
            foreach (ImageGlow glow in g.GetComponentsInChildren<ImageGlow>())
                arrowGlows.Add(glow);
        }
    }
    // Update is called once per frame
    public void SetDarkener(bool _state)
    {
        darkener.SetActive(_state);
        if(_state)
            colours[colourID[currentID]].SetActive(false);
    }
    public void RandomiseColours()
    {
        if (useColour)
        {
            for(int i = 0; i < colourID.Count; i++)
            {
                colourID[i] = Random.Range(0, colours.Count);
            }
        }
    }
}
