using UnityEngine;

public class CartridgeAnimation : MonoBehaviour
{
    public enum CartridgeType
    {
        Pacman,
        Mario,
        Tetris
    }

    [Header("Type of this cartridge")]
    public CartridgeType cartridgeType;

    [Header("Animator Reference")]
    public Animator animator;

    [Header("Animator Controllers")]
    public RuntimeAnimatorController pacmanController;
    public RuntimeAnimatorController marioController;
    public RuntimeAnimatorController tetrisController;

    void Awake()
    {
        ApplyAnimation();
    }

    private void ApplyAnimation()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        switch (cartridgeType)
        {
            case CartridgeType.Pacman:
                animator.runtimeAnimatorController = pacmanController;
                break;

            case CartridgeType.Mario:
                animator.runtimeAnimatorController = marioController;
                break;

            case CartridgeType.Tetris:
                animator.runtimeAnimatorController = tetrisController;
                break;
        }
    }
}