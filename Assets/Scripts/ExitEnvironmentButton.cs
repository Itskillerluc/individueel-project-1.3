using UnityEngine;

public class ExitEnvironmentButton : MonoBehaviour
{
    public ExitConfirmation exitConfirmation;
    
    public void Exit()
    {
        Instantiate(exitConfirmation, transform.parent);
    }
}