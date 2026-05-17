using UnityEngine;

public class PlayerScaler : MonoBehaviour
{
    private Vector3 normalScale;
    public Vector3 pacmanScale = new Vector3(2f, 2f, 2f);

    void Start()
    {
        normalScale = transform.localScale;
    }

    public void EnterPacman()
    {
        transform.localScale = pacmanScale;
    }

    public void ExitPacman()
    {
        transform.localScale = normalScale;
    }
}