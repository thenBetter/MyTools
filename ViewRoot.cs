using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ViewRoot {

	private static Transform viewRoot;
    private static EventSystem eventSystem;

	// Use this for initialization
	public static void Init () {
		viewRoot = GameObject.FindWithTag("ViewRoot").transform;
        eventSystem = viewRoot.Find("EventSystem").GetComponent<EventSystem>();
	}

    public static void SetUIEventEnable(bool enable)
    {
        eventSystem.enabled = enable;
    }

	public static GameObject GetPanel(string name){
	
		GameObject panel = GameObject.Instantiate (Resources.Load<GameObject> ("View/" + name));
		panel.transform.SetParent (viewRoot);
        panel.gameObject.name = name;
        Canvas canvas = panel.GetComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 1;

		return panel;
	}
    public static Transform GetTransformByPath(string absPath)
    {
        return viewRoot.Find(absPath);
    }


    public static GameObject GetScreenPanel(string name)
    {

        GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>("View/" + name));
        panel.transform.SetParent(viewRoot);
        return panel;
    }
}
