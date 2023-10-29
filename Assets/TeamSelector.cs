using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelector : MonoBehaviour
{
    public static TeamSelector instance;
    public List<GameObject> TeamPanelList;

    //private void Start()
    //{
    //    SelectPanel(TeamPanelList[0]);
    //}

    public void ClearPanels()
    {
        foreach (GameObject panel in TeamPanelList)
        {
            panel.GetComponent<Outline>().enabled = false;
        }
    }

    public void SelectPanel(GameObject panel)
    {
        ClearPanels();
        panel.GetComponent<Outline>().enabled = true;
        int index = TeamPanelList.IndexOf(panel);
        NetworkManager.instance.SetTeamIndex(index);
    }
}

