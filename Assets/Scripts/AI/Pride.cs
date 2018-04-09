﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BTAI;
using UnityEngine.Events;
using System;
using Gamekit2D;

public class Pride : MonoBehaviour
#if UNITY_EDITOR
    , BTAI.IBTDebugable
#endif
{
    [System.Serializable]
    public class BossRound
    {
        public int bossHP = 10;
    }

    public Transform target;

    public float viewDistance = 10.0f;
    public int strikeCount = 2;
    public float trackingSpeed = 30.0f;
    public float delay = 2;
    public float punchDelay, throwDelay, cleanupDelay, deathDelay;

    public GameObject Fist;
    public Damageable damageable;
    [Space]
    public BossRound[] rounds;
    [Space]
    public GameObject[] disableOnDeath;
    public UnityEvent onDefeated;

    [Header("Audio")]
    public AudioClip bossDeathClip;
    public AudioClip playerDeathClip;
    public AudioClip postBossClip;
    public AudioClip bossMusic;
    [Space]
    public RandomAudioPlayer stepAudioPlayer;
    public RandomAudioPlayer punchAudioPlayer;
    public RandomAudioPlayer throwAudioPlayer;
    public RandomAudioPlayer takingDamage;
    [Space]
    public AudioSource roundDeathSource;
    public AudioClip startRound2Clip;
    public AudioClip startRound3Clip;
    public AudioClip deathClip;

    [Header("UI")]
    public Slider healthSlider;

    bool inRange = false;
    int round = 0;

    private int m_TotalHealth = 0;
    private int m_CurrentHealth = 0;

    //used to track target movement, to correct for it.
    private Vector2 m_PreviousTargetPosition;


    public void SetTargetRange(bool inRange)
    {
        this.inRange = inRange;
    }

    Animator animator;
    Root ai;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        
        round = 0;
        
        ai = BT.Root();
        ai.OpenBranch(
            //First Round
            BT.Repeat(rounds.Length).OpenBranch(
                
                BT.While(Test).OpenBranch(
                    BT.RandomSequence(new int[] { 1, 6, 4 }).OpenBranch(
                        BT.Root().OpenBranch(
                            BT.Call(PrintWalk),
                            BT.Wait(delay)
                        ),
                        BT.If(TargetInRange).OpenBranch(
                            BT.Call(PrintInRange),
                            BT.Repeat(strikeCount).OpenBranch(
                                BT.Call(PrintPunch),
                                BT.Wait(punchDelay),
                                BT.Call(Punch),
                                BT.Wait(delay)
                            )
                        ),
                        BT.If(TargetOutOfRange).OpenBranch(
                            BT.Call(PrintThrow),
                            BT.Wait(throwDelay),
                            BT.Call(ThrowObject),
                            BT.Wait(delay)
                        )
                    )
                ),
                BT.While(IsAlive).OpenBranch(BT.Wait(0))
            ),
            BT.Wait(cleanupDelay),
            BT.Call(Cleanup),
            BT.Wait(deathDelay),
            BT.Call(Die),
            BT.Terminate()
        );

        BackgroundMusicPlayer.Instance.ChangeMusic(bossMusic);
        BackgroundMusicPlayer.Instance.Play();
        BackgroundMusicPlayer.Instance.Unmute(2.0f);

        //we aggregate the total health to set the slider to the proper value
        //(as the boss is actually "killed" every round and regenerated, we can't use directly its current health)
        for(int i = 0; i < rounds.Length; ++i)
        {
            m_TotalHealth += rounds[i].bossHP;
        }
        m_CurrentHealth = m_TotalHealth;

        if (target != null)
            m_PreviousTargetPosition = target.transform.position;
    }

    private void OnDisable()
    {
        
    }

    void PlayerDied(Damager d, Damageable da)
    {
        BackgroundMusicPlayer.Instance.PushClip(playerDeathClip);
    }

    bool Test()
    {
        return true;
    }

    void Punch()
    {
        //punchAudioPlayer.PlayRandomSound();
    }

    void ThrowObject()
    {
        //throwAudioPlayer.PlayRandomSound();
    }

    void PrintWalk()
    {
        Debug.Log(Time.realtimeSinceStartup + " : Pride will now Walk.");
    }

    void PrintInRange()
    {
        Debug.Log(Time.realtimeSinceStartup + " : Target is in range");
    }

    void PrintPunch()
    {
        Debug.Log(Time.realtimeSinceStartup + " : Pride will now Punch " + strikeCount + " times");
    }

    void PrintThrow()
    {
        Debug.Log(Time.realtimeSinceStartup + " : Target is not in range. Pride will now Throw an object.");
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            m_PreviousTargetPosition = target.position;
            Vector3 distance = target.position - transform.position;

            SetTargetRange(distance.sqrMagnitude < (viewDistance * viewDistance));

        }
    }

    void Update()
    {
        ai.Tick();
        if (target != null)
        {
            Vector2 targetMovement = (Vector2)target.position - m_PreviousTargetPosition;
            targetMovement.Normalize();
            Vector3 targetPos = target.position + Vector3.up * (1.0f + targetMovement.y * 0.5f);
        }
    }

    void Cleanup()
    {
        BackgroundMusicPlayer.Instance.ChangeMusic(postBossClip);
        BackgroundMusicPlayer.Instance.PushClip(bossDeathClip);

        foreach (var g in disableOnDeath)
            g.SetActive(false);
    }

    void Die()
    {
        onDefeated.Invoke();
    }

    bool TargetInRange()
    {
        return inRange;
    }

    bool TargetOutOfRange()
    {
        return !inRange;
    }

    bool IsAlive()
    {
        var alive = damageable.CurrentHealth > 0;
        return alive;
    }

    void NextRound()
    {
        damageable.SetHealth(rounds[round].bossHP);
        damageable.EnableInvulnerability(true);
       
        round++;

        if (round == 2)
        {
           
        }
        else if (round == 3)
        {
            
        }
    }

    void Disabled()
    {

    }

    void Enabled()
    {

    }

    public void Damaged(Damager damager, Damageable damageable)
    {
        takingDamage.PlayRandomSound();

        m_CurrentHealth -= damager.damage;
        healthSlider.value = m_CurrentHealth;
    }

    public void PlayStep()
    {
        stepAudioPlayer.PlayRandomSound();
    }

#if UNITY_EDITOR
    public BTAI.Root GetAIRoot()
    {
        return ai;
    }
#endif
}