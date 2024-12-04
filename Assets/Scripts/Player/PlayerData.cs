using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] Vector3 position;

    [Header("需要重新激活的东西")]
    public List<GameObject> rocks;
    [Header("需要复位的东西")]
    public List<GameObject> tracks;
    public List<Vector3> tracksPositions;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Save();
    }
    
    public int Level
    {
       get => level;
        set => level = value;
    }
    public Vector3 Position
    {
        get => position;
        set => position = value;
    }

    public const string PLAYER_DATA_KEY = "PlayerData";
    public const string PLAYER_DATAFILE_NAME = "PlayerData.sav";

    [System.Serializable] public class SaveData 
    {
        public int Level;
        public Vector3 playerPosition;
    }
    private void Update()
    {
        //按R键读档
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject rock in rocks)
            {
                if(rock != null)
                    rock.SetActive(true);
            }

            for (int i=0;i<tracks.Count;i++)
            {
                if(tracks[i] != null)
                    tracks[i].transform.position = tracksPositions[i];
            }
            Load();
        }
    }
    //遇到存档点
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            Save();   
        }
    }

    public void Save()//
    {
        //SaveByPlayerPrefs();
        SaveByJson();
    }
    public void Load()
    {

        //LoadFromPlayerPrefs();
        LoadFromJson();
    }

    SaveData SavingData()
    {
        var saveData = new SaveData();
        
        saveData.Level = SceneManager.GetActiveScene().buildIndex;
        saveData.playerPosition = transform.position;

        return saveData;
    }
    void LoadData(SaveData saveData)
    {
        level = saveData.Level;
        transform.position = saveData.playerPosition;
        // CharacterController2D ch=GetComponent<CharacterController2D>();
        // ch.isDying = false;
        
        Animator anim = GetComponent<Animator>();
    }

    void SaveByPlayerPrefs()
    {
        //PlayerPrefs.SetInt("FileMaxNum", fireMaxNum);
        //PlayerPrefs.SetInt("FileNum", fireNum);
        //PlayerPrefs.SetInt("Level", level);

        //PlayerPrefs.SetFloat("PlayerPositionX",transform.position.x);
        //PlayerPrefs.SetFloat("PlayerPositionY",transform.position.y);

        //PlayerPrefs.Save();

        SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, SavingData());
    }
    void LoadFromPlayerPrefs()
    {
        var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        var saveData=JsonUtility.FromJson<SaveData>(json);
        LoadData(saveData);
        //fireMaxNum = PlayerPrefs.GetInt("FileMaxNum", 2);
        //fireNum = PlayerPrefs.GetInt("FireNum", 0);
        //level = PlayerPrefs.GetInt("Level", 0);

        //transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerPositionX", 0f), PlayerPrefs.GetFloat("PlayerPositionY", 0f));
    }

    // [UnityEditor.MenuItem("Developer/Delete Player Data Prefs")]
    public static void DeletePlayerDataPrefs()
    {
        PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
    }

    void SaveByJson()
    {
        SaveSystem.SaveByJson(PLAYER_DATAFILE_NAME, SavingData());
    }
    void LoadFromJson()
    {
        var saveData=SaveSystem.LoadFromJson<SaveData>(PLAYER_DATAFILE_NAME); 
        LoadData(saveData);
    }

    // [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
    public static void DeletePlayerDataSvaeFile()
    {
        SaveSystem.DeleteSavaFile(PLAYER_DATAFILE_NAME);
    }
}
