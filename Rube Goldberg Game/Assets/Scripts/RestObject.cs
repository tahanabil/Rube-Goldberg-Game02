using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class RestObject : MonoBehaviour
{

    string factoryTag = "Ground";
    string startTag = "Star";
    string GoalTag = "Goal";
    string PlayerTag = "Player";
    string StructureTeleportTag = "StructureTeleport";
    int telportx = 0;



    Vector3 v;
    Vector3 restVec;
    //GameObject pnlBlocker;
    // Use this for initialization

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoaded;
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        StartScene();
    }


    void StartScene()
    {

        //fadein();
        v = this.gameObject.transform.position;
        Debug.Log(v);
        restVec = new Vector3(0f, 0f, 0f);

    }

     
 





    bool allStarCollected = false;
    void OnCollisionEnter(Collision col)
    {

        var levelData = gameObject.GetComponent<LevelData>();

        var senderTag = col.gameObject.tag;

        //hit floor
        if (senderTag == factoryTag)
        {
            
            var r = GetComponent<Rigidbody>();
            r.velocity = restVec;
            r.angularVelocity = restVec; 
            this.gameObject.transform.position = v;

            //Reset allStarCollected
            allStarCollected = false;
            telportx = 0;
            //resetstars 
            foreach (var item in levelData.Stars)
            {
                item.active = true;
            }
        }
        if (senderTag == startTag && Teleport.CurrentLocation== "TeleportPoint")
        {
            var star = col.gameObject;
            var collectedStarAudio = gameObject;
            var audio = collectedStarAudio.GetComponent<AudioSource>();
            audio.Play();
            star.active = false;
            //check 
        }

        var ActiveSTarts = levelData.Stars.Where(x => x.active).Count();

        if (ActiveSTarts == 0)
        {
            allStarCollected = true;
        }


        // Check if Hit Goal
        if (senderTag == PlayerTag )
        {
            levelData.AllowCollect = false;
        }



        //Check if Hit Goal
        if (senderTag == GoalTag && allStarCollected)
        {
            var Goal = col.gameObject;
            //
            //hide current ball
            this.gameObject.active = false;
            //Move to next Level
            Debug.Log("Move to next level");
            if (LevelData.level== LevelData.LevelCount-1)
            {
                //Show WIN UI
              var ui=  GameObject.FindGameObjectWithTag("winui");
                ui.active = true;
                return;
            }
            else
            {
                levelData.MoveToNExtLevel();
            }
           

        }

        //Check if Hit Goal
        if (senderTag == StructureTeleportTag)
        {
            if (telportx == 0)
            {
                telportx++;
            }
            else if (telportx == 1)
            {
              
                return;
            }

            var teleport = col.gameObject;
            var StructureTeleport=   GameObject.FindGameObjectsWithTag(StructureTeleportTag);
            var antherTeleport=  StructureTeleport.Where(x => x != teleport).FirstOrDefault();

            var newpos=  antherTeleport.GetComponent<Renderer>().bounds.center;
            this.gameObject.transform.position = newpos;
            //Move to next Level
            Debug.Log("Teleport");
           
        }

        


    }
}
