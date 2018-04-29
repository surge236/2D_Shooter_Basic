using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {

    public string sceneName;

    private Button startButton;

    // Use this for initialization
    void Start () {
        startButton = this.GetComponent<Button>();
        startButton.onClick.AddListener(onStartClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onStartClick() {
        SceneManager.LoadScene(sceneName);
        Debug.Log(SceneManager.sceneCount);
    }

}
