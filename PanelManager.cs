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
        current.startInflate();
	}

	void Update () {
		
	}

    public void setCurrentPanel(Panel p) {
        current = p;
        current.startInflate();
    }
}
