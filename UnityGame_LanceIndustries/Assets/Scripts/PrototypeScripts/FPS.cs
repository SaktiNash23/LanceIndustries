using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    float deltaTime = 0.0f;
    public int targetFPS;
    public Text RefreshRateText;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = targetFPS;
        Debug.Log("Target FPS : " + Application.targetFrameRate);
        Debug.Log("Refresh Rate : " + Screen.currentResolution);
        RefreshRateText.text = Screen.currentResolution.ToString();
    }
    
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
 
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
 
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}
