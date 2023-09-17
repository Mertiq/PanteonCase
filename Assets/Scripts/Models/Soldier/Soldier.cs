using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Soldier : MonoBehaviour, ILeftClickable, IRightClickable, IDamageable
{
    [SerializeField] private GameEvent onSoldierSelected;
    [SerializeField] private GameEvent onSoldierSelectedWithRightClick;
    [SerializeField] private SpriteRenderer soldierImage;
    [SerializeField] private HealthBar healthBar;
    [HideInInspector] public SoldierData data;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public float currentHealth;
    private SoldierState state;
    private float speed = .3f;
    private Vector2 offset = new(1 / Config.BoardScaleFactor / 2, 1 / Config.BoardScaleFactor / 2);

    public void Setup(params object[] args)
    {
        data = (SoldierData)args[0];
        var spawnPos = (Vector2)args[1];
        transform.position = spawnPos;
        soldierImage.sprite = data.sprite;
        position = spawnPos - offset;
        GameBoard.Instance.SetTiles(new Rect(position, Vector2.one / 2), false);
        currentHealth = data.health;
    }

    private void Move(Vector2 newPos)
    {
        GameBoard.Instance.SetTiles(new Rect(position, Vector2.one / 2), true);
        state = SoldierState.Moving;
        transform.DOMove(newPos + offset, speed * Time.deltaTime);
        position = newPos;
        GameBoard.Instance.SetTiles(new Rect(newPos, Vector2.one / 2), false);
    }

    public void ResetView(params object[] args)
    {
        healthBar.SetHealthBar(data.health, data.health);
    }

    public void OnLeftClick()
    {
        onSoldierSelected.Raise(this);
    }

    public IEnumerator FollowPath(List<Tile> path, IDamageable damageable = null)
    {
        if (state != SoldierState.Idle) yield break;

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
                        state = SoldierState.Idle;
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
        state = SoldierState.Attacking;

        while (true)
        {
            yield return new WaitForSeconds(1);
            
            if (!damageable.IsAlive())
            {
                state = SoldierState.Idle;
                yield break;
            }

            damageable.TakeDamage(data.damage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealthBar(currentHealth, data.health);
        if (!IsAlive())
            SoldierFactory.Instance.ReleaseSoldier(this);
    }

    public bool IsAlive() => currentHealth > 0;

    public void OnRightClick()
    {
        onSoldierSelectedWithRightClick.Raise(this);
    }
}