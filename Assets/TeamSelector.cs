using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelector : MonoBehaviour
{
    public List<GameObject> TeamPanelList;

    private void Start()
    {
        ClearPanels();
    }

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
        Debug.Log(index);

        //if (button == button1)
        //{
        //    NetworkManager.instance.SetPlayerPrefab("PlayerPrefab");
        //}
        //else
        //{
        //    NetworkManager.instance.SetPlayerPrefab("Cube");
        //}
    }
}

