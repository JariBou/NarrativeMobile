using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;
using UnityEngine;

namespace _project.Scripts.MessagingSystem
{
    [NodeInfo("Send Message", "MessagingSystem/Send Message")]
    public class SendMessageNode : NodeSystemNode
    {
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public string conversationId;
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public Message MessageToSend;
        public override ProcessInfo OnProcess(ExecInfo info)
        {
            MessagingService.Instance.SendMessage(GetValueOfProp<Message>(info, nameof(MessageToSend)), GetValueOfProp<string>(info, nameof(conversationId)));
            return base.OnProcess(info);
        }
    }
}