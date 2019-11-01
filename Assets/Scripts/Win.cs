using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {


    /// <summary>
    /// 调用GameManager中的显示星星的方法
    /// </summary>
	public void Show()
    {
        GameManager.instance.ShowStar();
    }
}
