﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControllerNew : MonoBehaviour
{
    public static float TURN_TEXT_WAIT = 2f;
    
    public static PanelControllerNew instance;

    public GameObject playerMain;
    public GameObject playerSub1;
    public GameObject playerSub2;
    public GameObject playerSub3;

    public GameObject enemyMain;
    public GameObject enemySub1;
    public GameObject enemySub2;
    public GameObject enemySub3;

    public GameObject turnUI;
    public TextMeshProUGUI turnTransition;

    public GameObject dialogPnl;

    private List<UnitProxy> players;
    private List<UnitProxy> enemies;

    private void Awake()
    {
        instance = this;

        instance.dialogPnl.SetActive(true);

        instance.dialogPnl.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = StoryStatic.GetLevelStory();

        ClearPanels();
    }

    public void LoadInitUnits(List<UnitProxy> units){
        players = new List<UnitProxy>();
        enemies = new List<UnitProxy>();
        foreach(UnitProxy unit in units){
            if (unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM) {
                enemies.Add(unit);
            }
            if (unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM) {
                players.Add(unit);
            }
        }
    }

    public static void ClearPanels(){
        //Debug.Log("Clear Panels");        

        instance.playerMain.SetActive(false);
        instance.playerSub1.SetActive(false);
        instance.playerSub2.SetActive(false);
        instance.playerSub3.SetActive(false);
        
        instance.enemyMain.SetActive(false);
        instance.enemySub1.SetActive(false);
        instance.enemySub2.SetActive(false);
        instance.enemySub3.SetActive(false);

        instance.turnUI.SetActive(false);
    }

    public static void DisplayTT(string msg){
        instance.StartCoroutine(instance.DisplayTurnText(msg));
    }

    IEnumerator DisplayTurnText(string msg){
        if(instance.dialogPnl.activeInHierarchy){
          yield return new WaitForSeconds(1f);
          instance.StartCoroutine(instance.DisplayTurnText(msg));
        } else {
          instance.turnUI.SetActive(true);
          instance.turnUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
          yield return new WaitForSeconds(TURN_TEXT_WAIT);
          instance.turnUI.SetActive(false);
          //instance.turnUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public static void SwitchChar(UnitProxy unit)
    {
        ClearPanels();
        if (unit != null) {
            //Debug.Log("SwitchChar: " + unit.GetData().characterMoniker);
            if (unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM) {
                //Debug.Log("Player Panel");
                LoadPlayerPanel(unit);
            } else {
                //Debug.Log("Enemy Panel");
                LoadEnemyPanel(unit);
            }
        }
    }

    public static void SwitchChar(UnitProxy player, UnitProxy enemy)
    {
        ClearPanels();
        if (player != null && enemy != null) {
            //Debug.Log("Attacking Chars: " + player.GetData().characterMoniker + "-" + enemy.GetData().characterMoniker);
            int playerDmg = enemy.GetData().GetTurnActions().CanAttack() ? (TurnController.instance.currentTeam == BoardProxy.PLAYER_TEAM ? 0 : enemy.GetData().GetAttack()) : 0;
            int enemyDmg = player.GetData().GetTurnActions().CanAttack() ? (TurnController.instance.currentTeam == BoardProxy.ENEMY_TEAM ? 0 : player.GetData().GetAttack()) : 0;

            LoadPlayerPanel(player, playerDmg);
            LoadEnemyPanel(enemy, enemyDmg);
        }
    }

    static void LoadPlayerPanel(UnitProxy unit, int hpMissing = 0){
        List<UnitProxy> remainingPlayers = new List<UnitProxy>(instance.players.Where(unt => unt.GetData().GetCurrHealth() > 0));
        remainingPlayers.Remove(unit);
        LoadPanelSuite(instance.playerMain, instance.playerSub1, instance.playerSub2, instance.playerSub3, unit, remainingPlayers, hpMissing);
    }

    static void LoadEnemyPanel(UnitProxy unit, int hpMissing = 0){
        List<UnitProxy> remainingEnemies = new List<UnitProxy>(instance.enemies.Where(unt => unt.GetData().GetCurrHealth() > 0));
        remainingEnemies.Remove(unit);
        LoadPanelSuite(instance.enemyMain, instance.enemySub1, instance.enemySub2, instance.enemySub3, unit, remainingEnemies, hpMissing);
    }

    static void LoadPanelSuite(GameObject main, GameObject sub1, GameObject sub2, GameObject sub3, UnitProxy unit, List<UnitProxy> remainingUnits, int hpMissing = 0){
        remainingUnits.Remove(unit);
        RefreshMainPanel(main, unit, hpMissing);
        if (remainingUnits.Count > 0) {
            RefreshSubPanel(sub1, remainingUnits[0]);
        }
        if (remainingUnits.Count > 1) {
            RefreshSubPanel(sub2, remainingUnits[1]);
        }
        if (remainingUnits.Count > 2) {
            RefreshSubPanel(sub3, remainingUnits[2]);
        }
    }

    static void RefreshMainPanel(GameObject panel, UnitProxy unit, int hpMissing = 0){
        Debug.Log("RefreshMainPanel: " + hpMissing.ToString());
        panel.SetActive(true);
        foreach(Transform child in panel.transform.GetChild(0)){
            if (child.name.Equals("CharImg")) {
                child.GetComponent<Image>().sprite = unit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
            if (child.name.Equals("CharName")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().characterMoniker.Replace(" ","\n");
            }
            if (child.name.Equals("CharType")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().GetCurrentClass() != null 
                  ? unit.GetData().GetCurrentClass().ClassName() : unit.GetData().GetUnitType().ToString();
            }
            if (child.name.Equals("Skills")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().GetSkillDescription();
            }
            if (child.name.Equals("Exp")) {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetData().GetLvl().ToString();
            }
            if (child.name.Equals("Rank")) {
                int rnk = unit.GetData().GetUnitClassRank();
                if (rnk > 0) {
                  child.gameObject.SetActive(true);
                  child.GetChild(0).GetComponent<Image>().sprite = BoardProxy.instance.glossary.GetComponent<Glossary>().ranks[rnk - 1];
                } else {
                  child.gameObject.SetActive(false);
                }
            }
            if (child.name.Equals("Aegis")) {
                if (unit.GetData().GetAegis()) {
                    child.gameObject.SetActive(true);
                } else {
                    child.gameObject.SetActive(false);
                }
            }
            if (child.name.Equals("Nullify")) {
                if (unit.GetData().GetNullified()) {
                    child.gameObject.SetActive(true);
                } else {
                    child.gameObject.SetActive(false);
                }
            }
            if (child.name.Equals("HealthOutline")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("HealthFillBar"))
                    {
                      t.GetComponent<Image>().fillAmount = (float) unit.GetData().GetCurrHealth() / (float)unit.GetData().GetMaxHP();
                    } else if (t.name.Equals("HealthFillBarPredict"))
                    {
                      float predicted = (float) unit.GetData().GetCurrHealth() - hpMissing;
                      predicted = predicted < 0 ? 0 : predicted;
                      t.GetComponent<Image>().fillAmount = predicted / (float)unit.GetData().GetMaxHP();
                    } else if (t.name.Equals("HealthText"))
                    {
                      t.GetComponent<TextMeshProUGUI>().text = unit.GetData().GetCurrHealth().ToString() + " / " + unit.GetData().GetMaxHP().ToString();
                    }
                }
            }
            if (child.name.Equals("Stats")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("Move"))
                    {
                        RefreshSkillPnl(t, unit.GetData().GetMoveSpeed().ToString());
                    } 
                    if (t.name.Equals("AtkPwr"))
                    {
                        RefreshSkillPnl(t, unit.GetData().GetAttack().ToString());
                    }
                    if (t.name.Equals("AtkRng"))
                    {
                        RefreshSkillPnl(t, unit.GetData().GetAtkRange().ToString());
                    }
                }
            }
            if (child.name.Equals("Turn")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("MvTrn"))
                    {
                        if (TurnController.instance.GetTeam() != unit.GetData().GetTeam()) {
                          RefreshSkillPnl(t, "<color=red>" + unit.GetData().GetTurnMoves().ToString() + "</color>");
                        } else {
                          RefreshSkillPnl(t, unit.GetData().GetTurnActions().GetMoves().ToString());
                        }
                    } 
                    if (t.name.Equals("AtkTrn"))
                    {
                        if (TurnController.instance.GetTeam() != unit.GetData().GetTeam()) {
                          RefreshSkillPnl(t, "<color=red>" + unit.GetData().GetTurnAttacks().ToString() + "</color>");
                        } else {
                          RefreshSkillPnl(t, unit.GetData().GetTurnActions().GetAttacks().ToString());
                        }
                    }
                }
            }
        }
    }

    static void RefreshSkillPnl(Transform pnl, string val){
        foreach (Transform t in pnl)
        {
            if (t.name.Equals("Val"))
            {
              t.GetChild(0).GetComponent<TextMeshProUGUI>().text = val;
            }
        }
    }

    static void RefreshSubPanel(GameObject panel, UnitProxy unit){
        panel.SetActive(true);
        foreach(Transform child in panel.transform){
            if (child.name.Equals("type")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().GetUnitType().ToString();
            }
            if (child.name.Equals("mv")) {
                string txt = unit.GetData().GetTurnActions().GetMoves().ToString();
                if (TurnController.instance.GetTeam() != unit.GetData().GetTeam()) {
                    txt = "<color=red>" + unit.GetData().GetTurnMoves() + "</color>";
                }
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = txt;
            }
            if (child.name.Equals("atk")) {
                string txt = unit.GetData().GetTurnActions().GetAttacks().ToString();
                if (TurnController.instance.GetTeam() != unit.GetData().GetTeam()) {
                    txt = "<color=red>" + unit.GetData().GetTurnAttacks() + "</color>";
                }
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = txt;
            }
            if (child.name.Equals("HealthOutline")) {
                child.GetChild(0).GetComponent<Image>().fillAmount = (float) unit.GetData().GetCurrHealth() / (float)unit.GetData().GetMaxHP();
            }
            if (child.name.Equals("img")) {
                child.GetComponent<Image>().sprite = unit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
}
