using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public Vector2Int Size = Vector2Int.one;
  [SerializeField] private Renderer[] _mainRenderer;

  private void OnDrawGizmosSelected()
  {
    for (int x = 0; x < Size.x; x++)
    {
      for (int z = 0; z < Size.y; z++)
      {
        Gizmos.color = new Color(0.01f, 1f, 0f, 0.31f);
        Gizmos.DrawCube(transform.position+new Vector3(x,0,z),new Vector3(1,1f,1));
      }
    }
  }
  public void SetTransparent(bool available)
  {
    if (available)
    {
      for (int i = 0; i < _mainRenderer.Length; i++)
      {
        _mainRenderer[i].material.color = new Color(0f, 0.35f, 0f);
      }
         
    }
    else
    {
      for (int i = 0; i < _mainRenderer.Length; i++)
      {
        _mainRenderer[i].material.color = new Color(0.48f, 0f, 0f);
      }
    }
  }
  public void SetNormal()
  {
    for (int i = 0; i < _mainRenderer.Length; i++)
    {
      _mainRenderer[i].material.color = Color.white;
    }
  }
}
