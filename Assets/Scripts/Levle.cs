using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levle : MonoBehaviour {

    private bool isSelect;//是否该关卡可以被打开
    private Image image;//背景图片
    public Sprite levelBG;//背景精灵
    public Button levelButton;//小关卡按钮
    public GameObject[] levelStar;//存放每一个小关卡的星星

     void Awake()
    {
        image = GetComponent<Image>();
    }
    // Use this for initialization
    void Start () {
        levelButton.onClick.AddListener(Select);
        if (transform.parent.GetChild(0).name == gameObject.name)//默认第一个小关卡是开放的
        {
            isSelect = true;
        }
        else
        {
            int beforeLevel = int.Parse(gameObject.name) - 1;//表示前一关卡
            if (PlayerPrefs.GetInt("level" + beforeLevel.ToString()) > 0)//如果前一关卡星星数量大于0，则下一关卡开启
            {
                isSelect = true;
            }
        }
        if (isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            if (count > 0)
            {
                for(int i = 0; i < count; i++)
                {
                    levelStar[i].SetActive(true);
                }
            }
        }
	}

	/// <summary>
    /// 小关卡按钮监听事件
    /// </summary>
    public void Select()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);//保存当前关卡的名字
            SceneManager.LoadScene(2);//进入游戏场景
        }
    }
}
