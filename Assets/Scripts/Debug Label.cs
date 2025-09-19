using TMPro;
using UnityEngine;

public class DebugLabel : MonoBehaviour
{
    [SerializeField] private StateMachine behaviorStateMach;

    private TMP_Text debugLabel;

    private void Start()
    {
        debugLabel = transform.GetChild(0).GetComponent<TMP_Text>();
        behaviorStateMach = GetComponentsInParent<StateMachine>()[1];
    }

    private void FixedUpdate()
    {
        debugLabel.text = behaviorStateMach.StateList[behaviorStateMach.CurrentState].ToString();
    }
}
