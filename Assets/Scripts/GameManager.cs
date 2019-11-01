using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



    public List<Pig> pigs = new List<Pig>();//存放猪的脚本对象
    public List<Bird> birds = new List<Bird>();//存放鸟的脚本对象
    public static GameManager instance;
    private Vector3 nextPos;//下一只小鸟开始的位置
    public GameObject win;//游戏胜利面板
    public GameObject lose;//游戏失败面板
    public GameObject[] stars;//存放星星的数组
    private int starNum=0;//每一个小关卡获得的星星数量

    void Awake()
    {
        instance = this;
       
    }


	void Start()
    {
        nextPos = birds[0].transform.position;
        Init();
    }

   
    private void Init()//初始化，将每只小鸟
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                //第一只鸟
                birds[i].transform.position = nextPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].sp.enabled = false;
                birds[i].enabled = false;//还应禁用该脚本，防止后面的小鸟位置跟随鼠标移动 
            }
        }
    }

    /// <summary>
    /// 下一只小鸟的飞出和判断游戏是否结束
    /// </summary>
    public void NextFly()
    {
        if (pigs.Count > 0)//如果猪的数量大于0，则进行下一步判断
        {
            if (birds.Count > 0)
            {
                Init();//再一次初始化
            }
            else
            {
                //游戏失败
                lose.SetActive(true);
   
            }
        }
        else
        {
            //游戏胜利
            win.SetActive(true);
        
        }
    }

    /// <summary>
    /// 显示游戏胜利的星星
    /// </summary>
    public void ShowStar()
    {
        StartCoroutine(WaitStar(0.25f));
    }

    /// <summary>
    /// 让星星一颗一颗的显示
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStar(float time)
    {
        for (; starNum < birds.Count + 1; starNum++)//因为最多只可能剩下两只鸟，所以要加1
        {
            if (starNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(time);
            stars[starNum].SetActive(true);
        }
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void Continue()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    public void SaveDate()
    {
        //还应先比较两次starNum的大小，选择大的保存
       if ( starNum>=PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")) )
       {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNum);
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"), starNum));
       }
        int sum = 0;//一张地图中已经获得所有小关卡的星星总数
        for(int i = 1; i < 10; i++)//十个关卡
        {
            sum += PlayerPrefs.GetInt("level"+i.ToString());
        }
        PlayerPrefs.SetInt("starTotal", sum);
    }
}
