using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;
using NodeSystem.Runtime.References;
using UnityEngine;

namespace _project.Scripts.Nodes
{
 
    [NodeInfo("Set Active Node", "Utils/Set Active Node")]
    public class SetActiveNode : NodeSystemNode
    {
        [SourceProperty(typeof(GameObject)), SerializeField]
        public string Source;

        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public bool state;


        public override ProcessInfo OnProcess(ExecInfo info)
        {
            GameObject gameObject = ReferenceManager.GetGameObject<GameObject>(Source);
            if (gameObject == null)
            {
                Debug.Log("Oh no");
            }
            else
            {
                gameObject.SetActive(GetValueOfProp<bool>(info, nameof(state)));
            }
            return base.OnProcess(info);
        }
    }
    
}