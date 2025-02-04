using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;
using UnityEngine;

namespace _project.Scripts.Nodes
{
    [NodeInfo("Play Sfx", "Audio/Play Sfx Node")]
    public class PlaySfxNode : NodeSystemNode
    {
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public AudioClip audioClip;

        public override ProcessInfo OnProcess(ExecInfo info)
        {
            AudioManager.PlaySfx(audioClip);
            return base.OnProcess(info);
        }
    }
}