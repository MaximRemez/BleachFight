using UnityEngine;

public class BlockProj : MonoBehaviour
{
    #region Variables

    private BoxCollider2D boxCollider;
    #endregion

    #region Basic_Method

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    #endregion

    #region Custom_Method

    public void SetDirection()
    {
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    public void Diactivate()
    {
        gameObject.SetActive(false);
        boxCollider.enabled = false;
    }
    #endregion

}
