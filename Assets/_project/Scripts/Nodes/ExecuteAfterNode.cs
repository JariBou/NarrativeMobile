using System.Collections;
using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;
using UnityEngine;

namespace _project.Scripts.Nodes
{
    [NodeInfo("Execute After", "Execute After")]
    public class ExecuteAfterNode : NodeSystemNode
    {
        [ExposedProperty(portDirection: PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public uint time;
        
        public override ProcessInfo OnProcess(ExecInfo info)
        {
            info.NodeSystemExecutioner.GetObject().StartCoroutine(Wait(info, GetValueOfProp<uint>(info, nameof(time))));
            // info.NodeSystemExecutioner.GetObject().StartCoroutine(Wait(info));
            return base.OnProcess(info);
        }
        
        public virtual IEnumerator Wait(ExecInfo info, float duration)
        {
            yield return new WaitForSeconds(duration);
            info.NodeSystemExecutioner.TickProcess();
        }
    }
}