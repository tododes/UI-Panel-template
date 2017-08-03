using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    public static PanelManager singleton;

    [SerializeField] private Panel current;

    void Awake(){
        singleton = this;
    }

	void Start () {
        MapSceneTree(current);
        current.startInflate();
	}

    public void MapSceneTree(Panel current){
        if (current.child.Count > 0){
            for (int i = 0; i < current.child.Count; i++){
                current.child[i].parent = current;
                MapSceneTree(current.child[i]);
            }
        }
       
    }

	void Update () {
		
	}

    public void setCurrentPanel(Panel p) {
        current = p;
        current.startInflate();
    }
}
