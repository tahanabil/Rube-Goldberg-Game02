using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class objFeature : MonoBehaviour
{
    public string DispalyName;
    public Sprite ImageThamble;
    public int fontSize;
    public string fontColor;
    public List< int> itemCount;
    public int UsedCount;


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



    // Start is called before the first frame update
    void StartScene()
    {
        UsedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
