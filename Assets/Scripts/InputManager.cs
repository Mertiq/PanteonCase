using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var hit = Physics2D.Raycast(Camera.main!.ScreenToWorldPoint(Input.mousePosition),
            transform.position, 100);

        if (hit.collider == null) return;

        if (hit.transform.gameObject.TryGetComponent(out IClickable clickable))
        {
            clickable.OnClick();
        }
    }
}