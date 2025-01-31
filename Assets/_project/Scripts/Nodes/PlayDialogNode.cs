using System.Collections;
using System.Collections.Generic;
using _project.Scripts.DialogSystem;
using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;
using UnityEngine;

namespace _project.Scripts.Nodes
{
    [NodeInfo("Play Dialog", "DialogSystem/Play Dialog", tooltip: "Continues execution after the audio clip has been played")]
    public class PlayDialogNode : NodeSystemNode
    {
        [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        public DialogInfo dialogInfo;
        
        // [ExposedProperty(PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
        // public float dialogDuration;

        
        public override ProcessInfo OnProcess(ExecInfo info)
        {
            DialogPanelScript.Instance.SetDialogText(GetValueOfProp<DialogInfo>(info, nameof(dialogInfo)).GetDialogForLoc(GameSettings.Instance.loc).content);
            // dialogDuration = GetValueOfProp<DialogInfo>(info, nameof(dialogInfo))
            //     .GetDialogForLoc(GameSettings.Instance.loc).GetClipDuration();
            // return base.OnProcess(info);
            DialogPanelScript.ShowDialog();
            info.NodeSystemExecutioner.GetObject().StartCoroutine(Wait(info, GetValueOfProp<DialogInfo>(info, nameof(dialogInfo)).GetDialogForLoc(GameSettings.Instance.loc).GetClipDuration()));
            return new ProcessInfo(id, GetNextNode(info.GraphInstance).id, ProcessInfo.ExecutionFlowType.Wait);
        }

        public override IEnumerator Wait(ExecInfo info, float duration)
        {
            yield return new WaitForSeconds(duration);
            DialogPanelScript.HideDialog();
            info.NodeSystemExecutioner.TickProcess();
        }
    }
}