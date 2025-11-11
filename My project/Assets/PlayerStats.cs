using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health=3;
    public int Lives=3;

    private float flickerTime=0f;
    public float flikerDuration=0.1f;

    private SpriteRenderer sr;

    public bool isImmune=false;
    private float ImmunityTime=0f;
    public float immunityDuration =1.5f;



    void SpriteFlicker()
    {
        if(flickerTime<flikerDuration)
        {
            flickerTime=flickerTime+Time.deltaTime;
        }
        else if(flickerTime >= flikerDuration)
        {
            sr.enabled=!(sr.enabled);
            flickerTime=0;
        }
    }

    public void TakeDamage(int damage)
    {
        if(isImmune==false)
        {
            health=health -damage ;
            if(health <0)
            health=0;
            if(Lives >0 && health==0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health =3;
                Lives--;
            }
            else if (Lives ==0 && health==0)
            {
                Debug.Log("GameOver");
                Destroy(this.gameObject);
            }
            Debug.Log("Player Health:" + health.ToString());
            Debug.Log("Player Lives:" + Lives.ToString());
        }
         isImmune =true;
         ImmunityTime=0f;
    }

        void Start()
    {
        sr= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isImmune==true)
        {
            SpriteFlicker();
            ImmunityTime=ImmunityTime+ Time.deltaTime;
            if(ImmunityTime>=immunityDuration)
            {
                isImmune=false ;
                sr.enabled=true;

            }
        }
    }
}
