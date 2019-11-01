using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {


    public List<Sprite> pigSprite = new List<Sprite>();//存放猪受伤的精灵图片
    private SpriteRenderer spriteRenderer;//当前猪的精灵图片
    public float maxRelativeSpeed = 6;
    public float minRelativeSpeed = 3;
    public GameObject boom;//猪死亡爆炸
    public GameObject pigScore;//猪的分数
    private bool isPig=true;
    public AudioClip hurtClip;//猪或者木头受伤的声音
    public AudioClip dieClip;//猪或者木头死亡的声音
	// Use this for initialization
    void Awake()
    {
        
    }
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Bird")
        {
            other.gameObject.GetComponent<Bird>().BirdHurt();
            if (other.relativeVelocity.magnitude > maxRelativeSpeed)
            {
                //TODO猪死亡
                PigDie();
            }
            else if (other.relativeVelocity.magnitude > minRelativeSpeed && other.relativeVelocity.magnitude < maxRelativeSpeed)
            {
                //TODO猪受伤的变化
                PigHurt();
              
            }
            else
            {
            }
        }
    }
    private void PigHurt()//猪受伤的变化
    {
        spriteRenderer.sprite = pigSprite[0];
        AudioPlay(hurtClip);
    }
    public void PigDie()//猪死亡后的处理
    {
        if (isPig)
        {
            GameManager.instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        AudioPlay(dieClip);
        Instantiate(boom, transform.position , Quaternion.identity);
        GameObject score = Instantiate(pigScore, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(score, 0.8f);
    }

    /// <summary>
    /// 播放声音
    /// </summary>
    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}