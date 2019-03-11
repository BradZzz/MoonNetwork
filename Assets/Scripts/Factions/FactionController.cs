﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionController : MonoBehaviour
{
    public GameObject btn;
    public GameObject finale;
    public TextMeshProUGUI desc;

    public GameObject human;
    public GameObject egypt;
    public GameObject cthulhu;

    private bool finalWorld;

    // Start is called before the first frame update
    void Awake()
    {
        btn.SetActive(false);
        finale.SetActive(false);
        desc.text = "";

        human.SetActive(false);
        egypt.SetActive(false);
        cthulhu.SetActive(false);

        GameMeta game = BaseSaver.GetGame();
        List<Unit.FactionType> factions = new List<Unit.FactionType>(game.unlockedFactions);
        if (factions.Contains(Unit.FactionType.Human)) {
            human.SetActive(true);
        }
        if (factions.Contains(Unit.FactionType.Egypt)) {
            egypt.SetActive(true);
        }
        if (factions.Contains(Unit.FactionType.Cthulhu)) {
            cthulhu.SetActive(true);
        }
        if (factions.Contains(Unit.FactionType.Human) && factions.Contains(Unit.FactionType.Egypt) && factions.Contains(Unit.FactionType.Cthulhu)) {
            finale.SetActive(true);
            SetFinaleColors();
        }
    }

    // Update is called once per frame
    public void Finale()
    {
        finalWorld = !finalWorld;
        SetFinaleColors();
    }

    void SetFinaleColors(){
        Color c = new Color();
        if (finalWorld) {
            ColorUtility.TryParseHtmlString("#F59C9CFF", out c);
        } else {
            ColorUtility.TryParseHtmlString("#FFFFFFFF", out c);
        }
        finale.GetComponent<Image>().color = c;
    }

    // Update is called once per frame
    public void Click(string faction)
    {
        string aiStr = "\nCampaign: Easy\n";
        GameMeta game = BaseSaver.GetGame();
        PlayerMeta player = BaseSaver.GetPlayer();
        player.faction = (Unit.FactionType)Enum.Parse(typeof(Unit.FactionType), faction);

        if (finalWorld) {
            player.world = GameMeta.World.candy;
            aiStr = "\nCampaign: Hard\n";
        } else {
            switch(player.faction){
              case Unit.FactionType.Human: player.world = GameMeta.World.nile;break;
              case Unit.FactionType.Egypt: player.world = GameMeta.World.mountain;break;
              case Unit.FactionType.Cthulhu: player.world = GameMeta.World.pyramid; aiStr = "\nCampaign: Hard\n"; break;
              default: player.world = GameMeta.World.nile;break;
            }
        }

        BaseSaver.PutPlayer(player);
        desc.text = "- " + faction + " - " + aiStr + "Strategy: " + Unit.GetFactionDesc(player.faction);
        btn.SetActive(true);
    }
}
