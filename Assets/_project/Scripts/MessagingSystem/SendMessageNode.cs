using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;

namespace _project.Scripts.MessagingSystem
{
    [NodeInfo("Send Message", "MessagingSystem/Send Message")]
    public class SendMessageNode : NodeSystemNode
    {
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public string conversationId;
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public Message MessageToSend;
        
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public bool playNotifSound = true;
        
        public override ProcessInfo OnProcess(ExecInfo info)
        {
            MessagingService.Instance.SendMessage(GetValueOfProp<Message>(info, nameof(MessageToSend)), GetValueOfProp<string>(info, nameof(conversationId)), GetValueOfProp<bool>(info, nameof(playNotifSound)));
            return base.OnProcess(info);
        }
    }
}