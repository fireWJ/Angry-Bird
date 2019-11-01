using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    private List<Pig> blocks = new List<Pig>();//存放进入触发器的木块或者猪
    /// <summary>
    /// 小鸟碰撞触发器触发
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
        
    }


    /// <summary>
    /// 退出触发器后
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 其他物体还在触发器内时点击鼠标左键调用该方法
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blocks.Count > 0 && blocks != null)//安全校验
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].PigDie();
            }
        }
        OnClear();
    }


    /// <summary>
    /// 处理Heisenberg小鸟爆炸的后事 
    /// </summary>
    private void OnClear()
    {
        rbody.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);//实例化爆炸特效
        spriteRenderer.enabled = false;//将图片隐藏
        testMyTrail.StopTrail();//关闭拖尾特效
        GetComponent<CircleCollider2D>().enabled = false;//将碰撞器关闭
    }

    public override void Next()
    {
        GameManager.instance.birds.Remove(this);//移除当前小鸟的脚本对象
        Destroy(gameObject);
        GameManager.instance.NextFly();
    }
}
