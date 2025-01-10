using UnityEngine;

public class EXPMine : MonoBehaviour
{
    public DashSystem dashSystem;
    public PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dashSystem.isFilling = true;
            dashSystem.EnableDashRegen();
            playerMovement.EnableHealthRegen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dashSystem.isFilling = false;
        }
    }
}
