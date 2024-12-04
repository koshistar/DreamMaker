using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public string loadScene;
    private PlayerData.SaveData saveData;
    private Transform player;
    public GameObject setting;
    public GameObject menu;
    
    // Start is called before the first frame update
    void Start()
    {
        saveData = SaveSystem.LoadFromJson<PlayerData.SaveData>(PlayerData.PLAYER_DATAFILE_NAME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene(loadScene);
    }

    public void LoadContinueScene()
    {
        if (saveData != null)
        {
            SceneManager.LoadScene(saveData.Level);
            player = GameObject.FindGameObjectWithTag("Player").transform;
            player.position = saveData.playerPosition;
        }
    }

    public void Setting()
    {
        if(menu!= null)
            menu.SetActive(false);
        if(setting != null)
            setting.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
