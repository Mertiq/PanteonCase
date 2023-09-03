using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    private void Update()
    {
        var hit = Physics2D.Raycast(Camera.main!.ScreenToWorldPoint(Input.mousePosition),
            transform.position, 100);

        if (hit.collider == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.transform.gameObject.TryGetComponent(out ILeftClickable clickable))
                clickable.OnLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (hit.transform.gameObject.TryGetComponent(out IRightClickable clickable))
                clickable.OnRightClick();
        }
    }
}