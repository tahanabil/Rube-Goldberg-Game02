using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> PlayerObj;

    public GameObject clones;


    private int objCount;
    private int currentObjIndex;
    bool IsInAction = false;

    bool UiVisable = false;
    bool startup = false;

    //void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnLoaded;
    //}

    //void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnLoaded;
    //}

    //private void OnLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    StartScene();
    //}



    void StartLevel()
    {
      


        objCount = PlayerObj.Count - 1;
        currentObjIndex = 0;
        UI_ChangeItem(currentObjIndex);


        foreach (var item in PlayerObj)
        {
            var _objFeature = item.GetComponent<objFeature>();
            _objFeature.UsedCount = 0;
            startup = true;
        }

        clones = new GameObject("ClonesGO"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!startup || LevelData.ReloadScene)
        {
            StartLevel();
            LevelData.ReloadScene=false;
        }


        UI_Controller(SteamVR_Actions._default);
        IsInAction = false;
    }

    private void UI_Controller(SteamVR_Input_ActionSet_default ActionSet)
    {

        try
        {
            int index = 0;
            //var rh_UP = ActionSet.DP_Up.GetLastState(SteamVR_Input_Sources.RightHand);
            //var rh_DOWN = ActionSet.DP_Down.GetLastState(SteamVR_Input_Sources.RightHand);
            index = !ActionSet.DP_Left.GetLastStateUp(SteamVR_Input_Sources.RightHand) ? index : 1;
            index = !ActionSet.DP_Right.GetLastStateUp(SteamVR_Input_Sources.RightHand) ? index : -1;
            var CreateElement = ActionSet.CreateElement.GetLastStateUp(SteamVR_Input_Sources.RightHand);

            //if (IsInAction)
            //{
            //    return;
            //}


            if (index != 0)
            {

                IsInAction = true; ;
                UI_ChangeItem(index);
                var parent = this.transform.parent.gameObject;
                parent.SetActive(true);
                return;
            }

            if (CreateElement)
            {
                CreateElementAction(currentObjIndex);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Controller");
            Debug.Log(e.Message); 
        }
        


    }

    private void CreateElementAction(int currentObjIndex)
    {
        try
        {
            var obj = PlayerObj[currentObjIndex];
            var _objFeature = obj.GetComponent<objFeature>();
            var pos = this.transform.parent.transform.parent.transform.position;
            if (_objFeature.UsedCount == _objFeature.itemCount[LevelData.level])
            {
                //TODO:Make reject audio
                return;
            }
            _objFeature.UsedCount++;


            var CountLabel = this.transform.Find("itemCount").gameObject;
            Text tCount = CountLabel.GetComponent<Text>();
            string countText;
            int availableItem = _objFeature.itemCount[LevelData.level];
            var leftCount = availableItem - _objFeature.UsedCount;


            countText = $"{leftCount}/{availableItem}";
            tCount.text = countText;
            
         var newgo=   UnityEngine.Object.Instantiate(obj, pos, Quaternion.identity);
            newgo.transform.parent= clones.transform;

            //foreach (Transform item in clones.transform)
            //{
            //    GameObject.Destroy(item.gameObject);
            //}


        }
        catch (Exception e)
        {
            //error
            Debug.Log(" ***********Error:");
            Debug.Log(e.Message);
        }
     
      


        //GameObject go = new GameObject();
        //SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        //renderer.sprite = _objFeature.ImageThamble;
        
    
    }

    void UI_ChangeItem(int i)
    {
        currentObjIndex += i;
        if (currentObjIndex > objCount) currentObjIndex = 0;
        if (currentObjIndex<0) currentObjIndex = objCount;

        Debug.Log(currentObjIndex);
        var obj = PlayerObj[currentObjIndex];
        //this.transform.GetChild(0).GetChild

        var pheader = this.transform.Find("pHeader").Find("ObjNameHeaderTeaxt").gameObject;
        var CountLabel = this.transform.Find("itemCount").gameObject;

        var objImage = this.transform.Find("objImage").gameObject.GetComponent<Image>();

        Text t =  pheader.GetComponent<Text>();
        Text tCount = CountLabel.GetComponent<Text>();


        var _objFeature = obj.GetComponent<objFeature>();

        string countText;
        int availableItem = _objFeature.itemCount[LevelData.level];
        countText = $"{availableItem-_objFeature.UsedCount}/{availableItem}";

        tCount.text = countText;


        //Debug.Log(_objFeature.DispalyName);
        //Debug.Log(_objFeature.fontSize);

        objImage.sprite = _objFeature.ImageThamble;
        t.text = _objFeature.DispalyName;
        //t.fontSize= _objFeature.fontSize;
      

        IsInAction = false;
    }




}
