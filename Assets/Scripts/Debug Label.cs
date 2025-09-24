using TMPro;
using UnityEngine;

public class DebugLabel : MonoBehaviour
{
    [SerializeField] private StateMachine stateMach;

    [SerializeField] private TMP_Text debugLabel;

    private void Start()
    {
        debugLabel = GetComponent<TMP_Text>();
        if(stateMach == null )
            stateMach = GetComponentsInParent<StateMachine>()?[1];
    }

    private void FixedUpdate()
    {
        debugLabel.text = stateMach?.StateList[stateMach.CurrentState].ToString();
    }
}
