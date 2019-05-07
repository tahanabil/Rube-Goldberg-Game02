using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class OPEN_UI : MonoBehaviour
{
    bool uiIsVisable = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      bool toogleUI    = SteamVR_Actions._default.UiToggle.GetLastStateUp(SteamVR_Input_Sources.LeftHand)  ;

        if (toogleUI)
        {
            uiIsVisable = !uiIsVisable;
           
                var chiled = this.transform.GetChild(0).gameObject;
                chiled.SetActive(uiIsVisable); 
                return;
           

        }

        if (!uiIsVisable)
        { 
        bool  pressed =false;
            pressed = SteamVR_Actions._default.DP_Left.GetLastStateUp(SteamVR_Input_Sources.RightHand)|| SteamVR_Actions._default.DP_Right.GetLastStateUp(SteamVR_Input_Sources.RightHand);

            if (SteamVR_Actions._default.DP_Left.GetLastStateUp(SteamVR_Input_Sources.RightHand))
            {

            }
        if (pressed)
        {
            var chiled = this.transform.GetChild(0).gameObject;
            chiled.SetActive(true);
            uiIsVisable = true;
        }
         
        }


    }
}



  
