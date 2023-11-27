using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    // API ��������Ʈ URL
    private string apiUrl = "https://app.vpspace.net/Team/all";


    public GameObject TeamPanelPrefab;
    public GameObject TeamScrollView;

    public static TeamManager instance;
    public List<GameObject> TeamPanelList;

    // JSON �����͸� ������ Ŭ����
    [System.Serializable]
    private class Team
    {
        public int team_index;
        public int team_leader;
        public string team_name;
        public string team_introduction;
        public string team_description;
        public int team_views;
        public bool team_recruting;
        public string created_at;
        public string updated_at;
        public int team_member_count;

        // ���⿡ API���� ��ȯ�Ǵ� �ʵ忡 �´� �ٸ� �������� �߰��ϼ���.
    }

    private void Start()
    {
        instance = this;
        StartCoroutine(GetJsonData());
    }


    public IEnumerator GetJsonData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            string jsonResult = www.downloadHandler.text;
            // JSON �����͸� Data Ŭ������ ������ȭ

            Debug.Log(jsonResult);
            Team[] jsonData = JsonHelper.FromJson<Team>(jsonResult);

            // ������ ó�� ��: �α׿� ���
            foreach (Team team in jsonData)
            {
                
                GenerateTeamPanel(team);
                Debug.Log($"{team.team_index}:{team.team_name} - {team.team_description}");
                //Debug.Log("ID: " + data.id);
                //Debug.Log("Name: " + data.name);
                // �ٸ� �ʵ忡 ���� ó���� �߰��ϼ���.
            }
            
        }
    }

    private void GenerateTeamPanel(Team team)
    {
        //GameObject _teampanel = Instantiate(TeamPanelPrefab);
        //_teampanel.GetComponent<TeamPanelManager>().SetTeamInfo(team.team_index, team.team_name, team.team_introduction, team.team_description);
        ////_teampanel.transform.SetParent(TeamScrollView.transform);

        GameObject _teampanel = Instantiate(TeamPanelPrefab, TeamScrollView.transform); // Content�� �ڽ����� �г� ����
        _teampanel.GetComponent<TeamPanelManager>().SetTeamInfo(team.team_index, team.team_name, team.team_introduction, team.team_description);

        TeamPanelList.Add(_teampanel);
        

    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] items;
        }
    }

    public void SetTeam(GameObject panel)
    {
        ClearPanels();
        
        
        int index = TeamPanelList.IndexOf(panel);
        Debug.Log($"�� ���� : {index}");
        NetworkManager.instance.SetTeamIndex(index);
    }

    [ContextMenu("�г� Ŭ����")]
    public void ClearPanels()
    {
        foreach (GameObject panel in TeamPanelList)
        {
            panel.GetComponent<TeamPanelManager>().ImageToggle(true);
        }
    }

    public void hello()
    {
        Debug.Log("hello");
    }
}
