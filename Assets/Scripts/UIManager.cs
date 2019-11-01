using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Button pauseButton;//游戏暂停按钮
    public Button continueButton;//游戏继续按钮
    public Button homeButton;//主菜单按钮
    public Button retryButton;//再来一次按钮
    public GameObject pausePalne;//游戏暂停的面板
    public Button winHomeButton;//赢面板上的主菜单按钮
    public Button winRetryButton;//赢面板上的再来一次按钮
    public Button loseHomeButton;//输面板上的主菜单按钮
    public Button loseRetryButton;//输面板上的再来一次按钮
    private Animator pauseAnimator;//游戏暂停面板的动画机

    void Start()
    {
        pauseButton = GameObject.Find("Canvas/PauseButton").GetComponent<Button>();
        pauseButton.onClick.AddListener(ShowPausePlane);
        pauseAnimator = pausePalne.GetComponent<Animator>();
        continueButton.onClick.AddListener(ContinueGame);
        homeButton.onClick.AddListener(ReturnHome);
        retryButton.onClick.AddListener(RetryGame);
        winHomeButton.onClick.AddListener(ReturnHome);
        winRetryButton.onClick.AddListener(RetryGame);
        loseHomeButton.onClick.AddListener(ReturnHome);
        loseRetryButton.onClick.AddListener(RetryGame);
    }


    /// <summary>
    /// 点击暂停按钮显示游戏暂停的面板
    /// </summary>
    private void ShowPausePlane()
    {
   

        pauseButton.gameObject.SetActive(false);
        StartCoroutine(waitAnimation(1.5f));
        pausePalne.SetActive(true);
        pauseAnimator.SetBool("isContinue", false);
        if (GameManager.instance.birds.Count > 0)
        {
            if (GameManager.instance.birds[0].isReleased == false)
            {
                GameManager.instance.birds[0].canMove = false;
            }
        }

    }

    /// <summary>
    /// 点击继续游戏按钮来继续游戏
    /// </summary>
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseAnimator.SetBool("isContinue", true);
        StartCoroutine(waitAnimationContinue(1.5f));
      //  pausePalne.SetActive(false);
        if (GameManager.instance.birds.Count > 0)
        {
            if (GameManager.instance.birds[0].isReleased == false)
            {
                GameManager.instance.birds[0].canMove = true;
            }
        }
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    private void ReturnHome()
    {
        Time.timeScale = 1;
       
        SceneManager.LoadScene(1);
        GameManager.instance.SaveDate();
    }

    /// <summary>
    /// 再进行一次游戏
    /// </summary>
    private void RetryGame()
    {
        Time.timeScale = 1;
       
        SceneManager.LoadScene(2);
        GameManager.instance.SaveDate();
    }

    /// <summary>
    /// 开始携程防止动画播放不完全
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator waitAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
    }

    IEnumerator waitAnimationContinue(float time)
    {
        yield return new WaitForSeconds(time);
        pauseButton.gameObject.SetActive(true);
    }
}
