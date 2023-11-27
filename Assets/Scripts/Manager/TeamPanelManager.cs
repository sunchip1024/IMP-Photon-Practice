using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPanelManager : MonoBehaviour
{
    public Text TeamName;
    public Text TeamIntroduction;
    public Text TeamDescription;
    public Image NonSelected;

    public void SelectTeam()
    {
        Debug.Log("select!");
    }
    
    public void SetTeamInfo(int team_index, string team_name, string team_introduction, string team_description)
    {
        TeamName.text = team_index + ":" + team_name;
        TeamIntroduction.text = team_introduction;
        TeamDescription.text = team_description;
        
        //teaminfo.text = "0 : ������\n����� �����ϴ� ���Դϴ�";
    }

    public void SelectPanel(GameObject panel)
    {
        TeamManager.instance.SetTeam(panel);
        ImageToggle(false);
    }

    public void ImageToggle(bool toggle)
    {
        NonSelected.enabled = toggle;
    }
}
