﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitProxyEditor : GridObjectProxyEdit
{
    public GameObject aegisObj;
    public GameObject rankObj;

    //public static float NO_ATK_WAIT = .5f;
    //public static float ATK_WAIT = 1.1f;
    public static float MV_TIME = .25f;

    private UnitEdit _data;
    protected override GridObjectEdit data
    {
        get { return _data; }
    }

    public override void OnSelected()
    {
        InteractivityManagerEditor.instance.OnUnitSelected(this);
    }

    void Awake(){
        aegisObj.SetActive(false);
        rankObj.SetActive(false);
    }

    public void Init()
    {
        if (_data == null)
            _data = new UnitEdit();
        SnapToCurrentPosition();
        int rnk = GetData().GetUnitClassRank();
        if (rnk > 0) {
          rankObj.SetActive(true);
          rankObj.GetComponent<SpriteRenderer>().sprite = BoardEditProxy.instance.glossary.GetComponent<Glossary>().ranks[rnk - 1];
        }
    }

    //public void AddLevel(){
    //  FloatUp(Skill.Actions.DidKill, "+1xp", Color.green, "gained +1 xp", true);
    //  _data.SetLvl(_data.GetLvl()+1);
    //}

    //public bool IsAlive(){
    //    return GetData().GetCurrHealth() > 0;
    //}

    //public bool IsAttacked(UnitProxyEditor oppUnit, bool useAttack = true)
    //{
    //  Debug.Log("IsAttacked");
    //  if (useAttack) {
    //      oppUnit.GetData().GetTurnActions().Attack();
    //  }
    //  //PanelControllerNew.SwitchChar(oppUnit);

    //  //Damage the unit
    //  /*
    //    Trigger the opponent's attack trigger here
    //  */

    //  Vector3Int animStart = oppUnit.GetPosition();
    //  Vector3Int animEnd = GetPosition();

    //  Debug.Log("animStart: " + animStart.ToString());
    //  Debug.Log("animEnd: " + animEnd.ToString());

    //  Vector3Int diff = animEnd - animStart;

    //  if (_data.GetAegis()) {
    //      Debug.Log("Aegis!");
    //      _data.SetAegis(false);
    //      StartCoroutine(AttackAnim(oppUnit.gameObject.transform.GetChild(0).GetComponent<Animator>(), 
    //        oppUnit.gameObject.transform, diff, ""));
    //      FloatUp(Skill.Actions.DidDefend, "-aegis", Color.blue, "Lost aegis", true);
    //      return false;
    //  }
    //  Debug.Log("No Aegis");

    //  StartCoroutine(AttackAnim(oppUnit.gameObject.transform.GetChild(0).GetComponent<Animator>(), 
    //    oppUnit.gameObject.transform, diff, "-" + oppUnit.GetData().GetAttack().ToString()));

    //  GetData().IsAttacked(oppUnit.GetData().GetAttack());
    //  if (GetData().IsDead())
    //  {
    //    if (oppUnit == this) {
    //      BoardEditProxy.instance.GiveLowestCharLvl(this);
    //    } else {
    //      oppUnit.AddLevel();
    //    }
    //    return true;
    //  }
    //  return false;
    //}

    //IEnumerator AttackAnim(Animator anim, Transform opp, Vector3Int diff, string msg){
    //  if (anim != null) {
    //      Vector3 theScale = opp.localScale;
    //      if (diff.x > 0) {
    //        //Debug.Log("right");
    //        theScale.x = -1;
    //        anim.SetBool("IDLE_FRONT_LEFT", false);
    //        while(anim.GetBool("IDLE_FRONT_LEFT")){ }
    //        anim.SetTrigger("ATK_BACK_LEFT");
    //      } else if (diff.x < 0) {
    //        //Debug.Log("left");
    //        theScale.x = 1;
    //        anim.SetBool("IDLE_FRONT_LEFT", true);
    //        while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
    //        anim.SetTrigger("ATK_FRONT_LEFT");
    //      } else {
    //        //Defender is right below or above attacker
    //        if (diff.y > 0) {
    //          //Debug.Log("up");
    //          theScale.x = 1;
    //          anim.SetBool("IDLE_FRONT_LEFT", false);
    //          while(anim.GetBool("IDLE_FRONT_LEFT")){ }
    //          anim.SetTrigger("ATK_BACK_LEFT");
    //        } else if (diff.y < 0) {
    //          //Debug.Log("down");
    //          theScale.x = -1;
    //          anim.SetBool("IDLE_FRONT_LEFT", true);
    //          while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
    //          anim.SetTrigger("ATK_FRONT_LEFT");
    //        }
    //      }
    //      opp.localScale = theScale;
    //  }

    //  FloatUp(Skill.Actions.DidAttack, msg, Color.red, "Was attacked", true);
    //  GetComponent<UnitProxyEditor>().CreateLaserHit();
    //  yield return null;
    //}

    //public bool IsAttackedEnvironment(int atkPwr)
    //{
    //  if (_data.GetAegis()) {
    //      _data.SetAegis(false);
    //      //FloatUp("-aegis", Color.blue, ATK_WAIT);
    //      FloatUp(Skill.Actions.None, "-aegis", Color.blue, "Lost aegis env", true);
    //      return false;
    //  }

    //  //Damage the unit
    //  GetData().IsAttacked(atkPwr);
    //  //FloatUp("-" + atkPwr.ToString(), Color.red, ATK_WAIT);
    //  FloatUp(Skill.Actions.None, "-" + atkPwr.ToString(), Color.red, "Took env damage", true);
    //  if (GetData().IsDead())
    //  {
    //    BoardProxy.instance.GiveLowestCharLvl(this);
    //    return true;
    //  }
    //  return false;
    //}

    //public void ReceiveHPBuff(int buff){
    //    GetData().SetHpBuff(GetData().GetHpBuff() + buff);
    //    //int newHp = GetData().GetMaxHP() + buff;
    //    if (buff != 0) {
    //      if (buff > 0){
    //          FloatUp(Skill.Actions.None, "+" + buff + " hp", Color.blue, "HP Buff", false);
    //      } else {
    //          FloatUp(Skill.Actions.None, "-" + buff + " hp", Color.cyan, "HP Sickness", true);
    //      }
    //    }
    //}

    //public void ReceiveAtkBuff(int buff){
    //    GetData().SetAttackBuff(GetData().GetAttackBuff() + buff);
    //    //int newAtk = GetData().GetAttack() + buff;
    //    if (buff != 0) {
    //      if (buff > 0){
    //          FloatUp(Skill.Actions.None, "+" + buff + " atk", Color.blue, "atk buff", false);
    //      } else {
    //          FloatUp(Skill.Actions.None, "-" + buff + " atk", Color.cyan, "atk sickness", true);
    //      }
    //    }
    //}

    //public void DelayedKill(UnitProxyEditor obj, UnitProxyEditor cUnit){
    //    StartCoroutine(DelayKill(obj, cUnit));
    //}

    //IEnumerator DelayKill(UnitProxyEditor obj, UnitProxyEditor cUnit){
    //    //Log the kill with the unit
    //    //cUnit.AddLevel();
    //    //Perform after kill skills
    //    cUnit.AcceptAction(Skill.Actions.DidKill,obj);

    //    //yield return new WaitForSeconds(UnitProxy.ATK_WAIT * 2);

    //    obj.FloatUp(Skill.Actions.DidKill, "Death", Color.red, "Killed Unit", false, true);

    //    //yield return new WaitForSeconds(UnitProxy.NO_ATK_WAIT);
    //    //Check the conditiontracker for game end
    //    //ConditionTracker.instance.EvalDeath(obj);                     
    //    //Turn off the tiles
    //    //StartCoroutine(ResetTiles());
    //    yield return null;
    //}

    public void Shake(){
        StartCoroutine(ShakeChar());
    }

    IEnumerator ShakeChar()
    {
        iTween.ShakePosition(gameObject,new Vector3(.25f,0,0), .2f);
        yield return null;
    }

    public void Jump(){
        StartCoroutine(JumpChar());
    }

    IEnumerator JumpChar()
    {
        iTween.ShakePosition(gameObject,new Vector3(0,.25f,0), .2f);
        yield return null;
    }

    //public void FloatUp(string msg, Color color, float wait){
    //    StartCoroutine(FloatUpAnim(msg, color, wait));
    //}

    //public void FloatUp(Skill.Actions interaction, string msg, Color color, string desc, bool shakeChar, bool deathConsideration = false){
    //    AnimationInteractionController.InteractionAnimation(interaction, this, msg, color, desc, shakeChar, deathConsideration);
    //}

    //public void HealUnit(int value, Skill.Actions action){
    //   Debug.Log("Healing: " + GetData().characterMoniker + " for " + value.ToString());
    //   int nwHlth = GetData().GetCurrHealth() + value;
    //   nwHlth = nwHlth > GetData().GetMaxHP() ? GetData().GetMaxHP() : nwHlth;
    //   if (nwHlth != GetData().GetCurrHealth()) {
    //     GetData().SetCurrHealth(nwHlth);
    //     FloatUp(action, "+" + value.ToString(), Color.green, "Unit Healed", false);
    //   }
    //}

    public int GetMoveSpeed()
    {
        return _data.GetMoveSpeed();
    }

    public int GetAttackRange()
    {
        return _data.GetAtkRange();
    }

    public void PutData(UnitEdit _data)
    {
      this._data = _data;
    }

    public UnitEdit GetData()
    {
      return _data;
    }

    //public void AcceptAction(Skill.Actions action, UnitProxyEditor u1)
    //{
    //  _data.AcceptAction(action, this, u1, null);
    //}

    //public void AcceptAction(Skill.Actions action, UnitProxyEditor u1, List<TileEditorProxy> path)
    //{
    //  _data.AcceptAction(action, this, u1, path);
    //}

    public virtual IEnumerator CreatePathToTileAndLerpToPosition(TileEditorProxy destination, Action callback)
    {
        var currentTile = BoardEditProxy.instance.GetTileAtPosition(GetPosition());
        var path = BoardEditProxy.instance.GetPath(currentTile, destination, this);
        yield return StartCoroutine(SetPathAndLerpToEnd(path));
        if (callback != null)
            callback();
    }

    protected virtual IEnumerator SetPathAndLerpToEnd(Path<TileEditorProxy> path)
    {
        yield return 0f;
        foreach (var t in path.Reverse())
        {
            yield return StartCoroutine(LerpToTile(t, MV_TIME));
        }
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        if (anim != null) {
            //if (anim.GetBool("MV_BACK_LEFT")) {
            //    anim.SetBool("IDLE_FRONT_LEFT", false);
            //} else {
            //    anim.SetBool("IDLE_FRONT_LEFT", true);
            //}
            anim.SetBool("MV_BACK_LEFT", false);
            anim.SetBool("MV_FRONT_LEFT", false);
        }
        //AcceptAction(Skill.Actions.DidMove, null, path.ToList());
        SnapToCurrentPosition();
    }

    public virtual IEnumerator LerpToTile(TileEditorProxy tile, float time)
    {
        Vector3 start = transform.position;
        Vector3 end = BoardEditProxy.GetWorldPosition(tile.GetPosition());
        /*
          Right here is where the animation needs to be set
        */
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        if (anim != null) {
          Vector3 theScale = transform.localScale;

          Vector3Int animStart = GetPosition();
          Vector3Int animEnd = tile.GetPosition();
  
          Debug.Log("animStart: " + animStart.ToString());
          Debug.Log("animEnd: " + animEnd.ToString());

          Vector3 diff = animEnd - animStart;
          //float turnWait = .1f;

          Debug.Log("Diff: " + diff.ToString());

          if (diff.x > 0) {
            Debug.Log("right");
            theScale.x = -1;
            anim.SetBool("IDLE_FRONT_LEFT", false);
            while(anim.GetBool("IDLE_FRONT_LEFT")){ }
            anim.SetBool("MV_BACK_LEFT", true);
            anim.SetBool("MV_FRONT_LEFT", false);
          } else if (diff.x < 0) {
            Debug.Log("left");
            theScale.x = 1;
            anim.SetBool("IDLE_FRONT_LEFT", true);
            while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
            anim.SetBool("MV_BACK_LEFT", false);
            anim.SetBool("MV_FRONT_LEFT", true);
          } else {
            //Defender is right below or above attacker
            if (diff.y > 0) {
              Debug.Log("up");
              theScale.x = 1;
              anim.SetBool("IDLE_FRONT_LEFT", false);
              while(anim.GetBool("IDLE_FRONT_LEFT")){ }
              anim.SetBool("MV_BACK_LEFT", true);
              anim.SetBool("MV_FRONT_LEFT", false);
            } else if (diff.y < 0) {
              Debug.Log("down");
              theScale.x = -1;
              anim.SetBool("IDLE_FRONT_LEFT", true);
              while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
              anim.SetBool("MV_BACK_LEFT", false);
              anim.SetBool("MV_FRONT_LEFT", true);
            }
          }
          transform.localScale = theScale;
        }
        float timer = 0f;
        while (timer < time)
        {
            //            Debug.Log(transform.position);
            transform.position = Vector3.Lerp(start, end, timer / time);
            timer += Time.deltaTime;
            yield return 0f;
        }

        data.SetPosition(tile.GetPosition());
        tile.CreateSmoke();
    }

    public void ZapToTile(TileEditorProxy newTl, TileEditorProxy oldTl, string actStr){  
        StartCoroutine(WaitForZap(newTl, oldTl, actStr));
    }

    IEnumerator WaitForZap(TileEditorProxy newTl, TileEditorProxy oldTl, string actStr){
        yield return new WaitForSeconds(AnimationInteractionController.ANIMATION_WAIT_TIME_LIMIT);

        newTl.ReceiveGridObjectProxy(this);
        //newTl.FloatUp(Skill.Actions.None, "whabam!", Color.blue, actStr);

        oldTl.RemoveGridObjectProxy(this);
        //oldTl.FloatUp(Skill.Actions.None, "poof", Color.cyan, actStr);
        SnapToCurrentPosition(); 
    }

    void Update(){
        if (GetData().GetAegis()) {
            aegisObj.SetActive(true);
        } else {
            if (aegisObj.activeInHierarchy) {
                StartCoroutine(PopAegis());
            }
        }
    }

    IEnumerator PopAegis(){
        yield return new WaitForSeconds(AnimationInteractionController.ANIMATION_WAIT_TIME_LIMIT);
        aegisObj.SetActive(false);
    }

    public void CreateLaserHit(){
        StartCoroutine(CreateLaserAnim());
    }

    IEnumerator CreateLaserAnim(){
        yield return new WaitForSeconds(AnimationInteractionController.ATK_WAIT);
        Vector3 instPos = transform.position;
        instPos.y += .6f;
        GameObject smoke = Instantiate(BoardEditProxy.instance.glossary.GetComponent<Glossary>().fxLaser, instPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(smoke);
    }
}
