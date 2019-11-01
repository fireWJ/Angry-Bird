using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour {

    private float maxDistance;//小鸟距离弹弓的最远距离
    private Transform slingshot;//弹弓transform
    private bool isClick;//是否点击鼠标
    [HideInInspector]
    public SpringJoint2D sp;//弹簧组件
    protected Rigidbody2D rbody;//小鸟刚体
    public LineRenderer rightLineRenderer;//右支架画线组件
    public LineRenderer leftLineRenderer;//左支架画线组件
    public Transform rightPos;//右支架位置
    public Transform leftPos;//左支架位置
    public GameObject boom;//爆炸特效
    protected TestMyTrail testMyTrail;
    [HideInInspector]
    public bool canMove;//用来飞出后是否能再移动
    private Vector3 pos;//记录鸟的位置，即一直记录第一只鸟的位置
    public AudioClip seclct;//小鸟被选中
    public AudioClip fly;//小鸟飞行
    private bool isFirstBird=false;//判断是不是在树枝上的小鸟
    private bool isFly=false;//判断小鸟是否在飞行
    [HideInInspector]
    public bool isReleased = false;//点击鼠标后是否松手
    public Sprite birdHurtSprite;//鸟受伤后的图片精灵
    protected SpriteRenderer spriteRenderer;//鸟的精灵渲染器组件

    void Awake()
    {
        slingshot = GameObject.Find("Slingshot").transform;
        sp= gameObject.GetComponent<SpringJoint2D>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        testMyTrail = GetComponent<TestMyTrail>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        maxDistance = 2.2f;
        isClick = false;
        canMove = true;
        testMyTrail.StopTrail();
    }

    void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (isClick)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z += 10;
            transform.position = mousePos;//将鼠标的位置赋值给小鸟
           
            if (Vector3.Distance(transform.position, slingshot.position) > maxDistance)
            {
                //TODO限制小鸟的最远位置
                Vector3 unitVector = (transform.position-slingshot.position).normalized;//先确定方向
                unitVector *= maxDistance;//将单位向量放大
                transform.position = unitVector+slingshot.position;
            }
            DarwLine();
        }
        pos = transform.position;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(pos.x,0,11), Camera.main.transform.position.y,Camera.main.transform.position.z), 1.5f * Time.deltaTime);
        if (Input.GetMouseButton(0)&&isFly)//如果小鸟在飞行，并且同时点击了鼠标左键
        {
            //TODO调用虚方法，触发技能
            ShowSkill();
        }
    }


	private void OnMouseDown()//点击鼠标，调用此方法记得给物体添加碰撞体
    {
        if (canMove&&transform.position.y>-2f)//防止后面的猪也触发该方法
        {
            AudioPlay(seclct);
            rbody.isKinematic = true;
            isClick = true;
            isFirstBird = true;
        }
  
    }
    private void OnMouseUp()//处理松开鼠标后的事
    {
        if (canMove&& isFirstBird)
        {
            isClick = false;
            rbody.isKinematic = false;
            //TODO处理小鸟释放后的运动状态
            StartCoroutine(Fly(0.18f));
            //gameObject.GetComponent<TestMyTrail>().StartTrail();
            leftLineRenderer.enabled = false;
            rightLineRenderer.enabled = false;
            canMove = false;
        }
        
    }
    IEnumerator Fly(float time)//携程控制小鸟受弹簧组件的时间
    {
        isReleased = true;
        isFly = true;
        yield return new WaitForSeconds(time);     
        sp.enabled = false;
        AudioPlay(fly);
        testMyTrail.StartTrail();
        Invoke("Next", 3.5f);
    }
    private void DarwLine()//实现画线
    {
        leftLineRenderer.enabled = true;
        rightLineRenderer.enabled = true;
        rightLineRenderer.SetPosition(0, rightPos.position);
        leftLineRenderer.SetPosition(0, leftPos.position);
        rightLineRenderer.SetPosition(1, transform.position);
        leftLineRenderer.SetPosition(1, transform.position);
    }

    public virtual void Next()//第一只小鸟飞出去之后，后面的小鸟的位置需要代替前一只小鸟
    {
        GameManager.instance.birds.Remove(this);//移除当前小鸟的脚本对象
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager.instance.NextFly();
    }

    /// <summary>
    /// 碰撞后停止画拖尾特效
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        isFly = false;
        testMyTrail.StopTrail();
    }

    /// <summary>
    /// 播放声音
    /// </summary>
    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 释放技能
    /// 注意虚方法不能是私有方法
    /// </summary>
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    /// <summary>
    /// 小鸟受伤后的处理
    /// </summary>
    public void BirdHurt()
    {
        spriteRenderer.sprite = birdHurtSprite;
    }
}
