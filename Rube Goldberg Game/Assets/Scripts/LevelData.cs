using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

 


public class LevelData : MonoBehaviour
{
     
    public bool GameStart = false;
    public static bool ReloadScene=false;
    public static int level = 0;
    
    public List<GameObject> Stars;
    public string NextLevelSence;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    GameObject platformpos;
     GameObject platform ;

    public bool AllowCollect { get; internal set; }
    public static int LevelCount { get; internal set; }

    void setup() {
        if (!GameStart)
        {
            GameStart = !GameStart;
        
            LevelCount = 4;
            ReloadScene = true;

            ////Set platform postion
            // platformpos = GameObject.FindGameObjectWithTag("platformpos");

            // platform = GameObject.FindGameObjectWithTag("platform");

          
            //platform.transform.position = new Vector3(0f, 0f, 0f);
            //platform.transform.parent = platformpos.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

        setup();
        //platform.active = false;
      //  Vector3 x= platform.transform.position;//= new Vector3(0, 0, 0);
        //var ClonesGO = GameObject.Find("ClonesGO");
        //GameObject.Destroy(ClonesGO);
    }

    public void MoveToNExtLevel()
    {
        //  SceneManager.LoadScene(NextLevelSence);

        //Distroy created objects
        DestroyByTag("Structure");
        DestroyByTag("StructureTeleport");
        DestroyByTag("Player");
        GameObject.Destroy( GameObject.Find("ClonesGO"));
        //Debug.Log("Done.....");
         
        //GameObject.Destroy(ClonesGO);
        LevelData.ReloadScene = true;
        LevelData.level++;
        SteamVR_LoadLevel.Begin(NextLevelSence); 
    }

    void DestroyByTag(string tag)
    {
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var item in objs)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}
