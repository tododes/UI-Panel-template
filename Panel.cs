using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelStatus { ZERO, VISIBLE, INFLATE_DEFFLATE }

public class Panel : MonoBehaviour {

    [SerializeField] private int desiredSize;
    private int inflateIndex;
    private RectTransform rect;
    private Vector3 inflateRate;
    private PanelManager manager;

    public PanelStatus status;
    public Panel prev;
    public List<Panel> next = new List<Panel>();

    void Start(){
        manager = PanelManager.singleton;
        rect = GetComponent<RectTransform>();
        inflateRate = new Vector3(desiredSize * 1.5f, desiredSize * 1.5f, 0);
    }

    public void startInflate(){
        inflateIndex = 1;
        status = PanelStatus.INFLATE_DEFFLATE;
    }

    public void startDeflate(){
        inflateIndex = 0;
        status = PanelStatus.INFLATE_DEFFLATE;
    }

    void FixedUpdate() {
        if(inflateIndex == 1){
            if (rect.localScale.x < desiredSize)
                rect.localScale += inflateRate * Time.deltaTime;
            else {
                status = PanelStatus.VISIBLE;
            }
        }
        else if (inflateIndex == 0){
            if (rect.localScale.x > 0f)
                rect.localScale -= inflateRate * Time.deltaTime;
            else {
                status = PanelStatus.ZERO;
            }
        }
    }

    public void GoToNext(int index) {
        startDeflate();
        StartCoroutine(DelayToNextPanel(next[index]));
    }

    public void GoToPrev() {
        startDeflate();
        StartCoroutine(DelayToNextPanel(prev));
    }

    public float getInflateTime(){
        return desiredSize / inflateRate.x;
    }

    private IEnumerator DelayToNextPanel(Panel next){
        yield return new WaitForSeconds(getInflateTime() + 1f);
        manager.setCurrentPanel(next);
    }

}
