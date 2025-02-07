using System.Collections.Generic;
using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _project.Scripts.MessagingSystem
{
    [NodeInfo("Send Messages", "MessagingSystem/Send Messages")]
    public class SendMessagesNode : NodeSystemNode
    {
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public string conversationId;
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public List<Message> MessagesToSend;
        
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public bool playNotifSoundAtEnd = true;
        
        public override ProcessInfo OnProcess(ExecInfo info)
        {
            List<Message> messages = GetValueOfProp<List<Message>>(info, nameof(MessagesToSend));
            for (int i = 0; i < messages.Count-1; i++)
            {
                MessagingService.Instance.SendMessage(messages[i], GetValueOfProp<string>(info, nameof(conversationId)));
            }
            MessagingService.Instance.SendMessage(messages[messages.Count-1], GetValueOfProp<string>(info, nameof(conversationId)), GetValueOfProp<bool>(info, nameof(playNotifSoundAtEnd)));

            return base.OnProcess(info);
        }
    }
}