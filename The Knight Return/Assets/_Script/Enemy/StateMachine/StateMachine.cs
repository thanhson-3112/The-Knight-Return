using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;
    public bool canUpdate = false; 

    public void Start()
    {
        StartCoroutine(WaitBeforeStart());
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(3f); 
        canUpdate = true; 
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    public void Update()
    {
        if (canUpdate && currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (canUpdate && currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
