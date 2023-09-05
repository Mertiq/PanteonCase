﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Soldier : MonoBehaviour, ISetupable, ILeftClickable
{
    [SerializeField] private GameEvent onSoldierSelected;
    [SerializeField] private SpriteRenderer soldierImage;
    [HideInInspector] public SoldierData data;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isAttacking;
    private float speed = .3f;
    private Vector2 offset = new(1 / Config.BoardScaleFactor / 2, 1 / Config.BoardScaleFactor / 2);

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        var spawnPos = (Vector2)args[1];
        transform.position = spawnPos;
        soldierImage.sprite = data.sprite;
        position = spawnPos - offset;
    }

    private void Move(Vector2 newPos)
    {
        isMoving = true;
        transform.DOMove(newPos + offset, speed * Time.deltaTime);
        position = newPos;
    }

    public void ResetView(params object[] args)
    {
        throw new System.NotImplementedException();
    }

    public void OnLeftClick()
    {
        onSoldierSelected.Raise(this);
    }

    public IEnumerator FollowPath(List<Tile> path, IDamageable damageable = null)
    {
        var targetIndex = 0;
        
        if (path.Count > 0)
        {
            var currentWaypoint = path[targetIndex];
            while (true)
            {
                if (position == currentWaypoint.position)
                {
                    targetIndex++;
                    if (targetIndex >= path.Count)
                    {
                        isMoving = false;
                        break;
                    }

                    currentWaypoint = path[targetIndex];
                }

                yield return new WaitForSeconds(speed);

                Move(currentWaypoint.position);
            }
        }

        if (damageable is not null)
            StartCoroutine(AttackCoroutine(damageable));
    }

    private IEnumerator AttackCoroutine(IDamageable damageable)
    {
        while (true)
        {
            if (!((Building)damageable).IsAlive())
            {
                yield break;
            }

            damageable.TakeDamage(data.damage);
            yield return new WaitForSeconds(1);
        }
    }
}