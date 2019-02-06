﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileProxy : MonoBehaviour, IHasNeighbours<TileProxy>, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Transform anchor;

    private List<GridObjectProxy> objectProxies = new List<GridObjectProxy>();

    public Vector3Int GetPosition()
    {
        return tile.position;
    }

    public IEnumerable<TileProxy> Neighbours
    {
        get
        {
            return BoardProxy.instance.GetNeighborTiles(tile.position);
        }
    }

    void SnapToPosition()
    {
        transform.position = BoardProxy.instance.grid.CellToLocal(tile.position);
    }

    public void Init(Tile t)
    {
        tile = t;
        name = t.position.ToString();//for convenience in the heirarchy
        SnapToPosition();
    }



    public void HighlightSelected()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
    }
    public void UnHighlight()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    public void ReceiveGridObjectProxy(GridObjectProxy proxy)
    {
        if (!objectProxies.Contains(proxy))
        {
            objectProxies.Add(proxy);
            proxy.SetPosition(tile.position);
        }
    }

    public void RemoveGridObjectProxy(GridObjectProxy proxy)
    {
        if (objectProxies.Contains(proxy))
        {
            objectProxies.Remove(proxy);
        }
    }

    public List<GridObjectProxy> GetContents()
    {
        return objectProxies;
    }

    public bool CanReceive(GridObjectProxy obj)
    {
        if (obj is UnitProxy)
        {
            if (objectProxies.Where(op => op is UnitProxy).Count() > 0)//TODO: rework to a better system with layers
                return false;
        }
        return true;//for now
    }

    #region events
    public void OnPointerDown(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileSelected(this);
        foreach (var obj in objectProxies)
        {
            obj.OnSelected();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileUnHovered(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileHovered(this);
    }

    #endregion
}