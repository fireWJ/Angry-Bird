using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    private bool isSelect=false;//关卡是否开放
    public int starsNum=0;//需要获得的星星总数量
    public GameObject locks;//锁
    public GameObject stars;//星星
    public GameObject levelPanel;//关卡面板
    private Button mapButton;//地图按钮
    private Image image;//地图背景图片
    public Button levelReturnButton;//关卡界面返回按钮
    public int startLevle=1;//开始的关卡
    public int endLevle = 3;//结束的关卡
    private int count=0;//一个地图获得的星星总书
    public Text countText;//地图上获得星星的文本


	// Use this for initialization
	void Start () {
      //  PlayerPrefs.DeleteAll();
        image = GetComponent<Image>();
        
       // image.color = new Color(128f/255,128f/255,128f/255,255f/255);
        image.color = Color.gray;
        if (PlayerPrefs.GetInt("starTotal", 0) >= starsNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            image.color = Color.white;
            locks.SetActive(false);
            stars.SetActive(true);
        }
        mapButton = gameObject.GetComponent<Button>();
        mapButton.onClick.AddListener(MapSelect);
        levelReturnButton.onClick.AddListener(LevelReturn);
        for (; startLevle <= endLevle; startLevle++)
        {
            count += PlayerPrefs.GetInt("level" + startLevle.ToString(),0);
        }
        countText.text = count.ToString() + "/9";
    }
	

    /// <summary>
    /// 地图按钮监听事件
    /// </summary>
	private void MapSelect()
    {
        if (isSelect)
        {
            levelPanel.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 点击等级关卡的返回按钮
    /// </summary>
    private void LevelReturn()
    {
        levelPanel.SetActive(false);
        transform.parent.gameObject.SetActive(true);
    }
}
