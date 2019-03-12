﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glossary : MonoBehaviour
{
    public static bool PROD = false;

    public UnitProxy[] units;
    public ObstacleProxy[] obstacles;

    public UnitProxy humanSoldier;
    public UnitProxy humanScout;
    public UnitProxy humanMage;

    public UnitProxy egyptSoldier;
    public UnitProxy egyptScout;
    public UnitProxy egyptMage;

    public UnitProxy cthulhuSoldier;
    public UnitProxy cthulhuScout;
    public UnitProxy cthulhuMage;    
    public UnitProxy cthulhuWisp;

    public UnitProxy finalBlack;
    public UnitProxy finalRed;
    public UnitProxy finalBlue;

    public UnitProxy summonedSkeleton;

    public GameObject Smoke;
    public GameObject Laser;

    public GameObject fxBarrage;
    public GameObject fxBloodExplosion;
    public GameObject fxBloodSplatter;
    public GameObject fxEGExplosion;
    public GameObject fxFirePillar;
    public GameObject fxFireBaseLarge;
    public GameObject fxFireBaseSmall;
    public GameObject fxFireShield;
    public GameObject fxHMExplosion;
    public GameObject fxLaser;
    public GameObject fxLPExplosion;
    public GameObject fxSmoke1;
    public GameObject fxSmoke2;
    public GameObject fxSmoke3;

    public UnitProxyEditor playerTile;
    public UnitProxyEditor enemyTile;
    public ObstacleProxyEdit obstacleEditTile;

    public Sprite grassTile;
    public Sprite fireTile;
    public Sprite snowTile;
    public Sprite wallTile;
    public Sprite divineTile;

    public Sprite humanFaction;
    public Sprite egyptFaction;
    public Sprite chtulhuFaction;

    public Sprite[] ranks;

}
