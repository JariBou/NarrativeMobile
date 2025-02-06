using System.Collections;
using System.Collections.Generic;
using _project.Scripts.DialogSystem;
using _project.Scripts.Localisation;
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
            dialogInfo = GetValueOfProp<DialogInfo>(info, nameof(dialogInfo));
            TranslatedDialogData translatedDialogData = dialogInfo.GetDialogForLoc(GameSettings.Instance.loc);
            
            DialogPanelScript.Instance.SetDialogText(translatedDialogData.content);
            
            DialogPanelScript.ShowDialog(info.NodeSystemExecutioner);
            AudioManager.PlayDialog(translatedDialogData.audioClip);
            
            info.NodeSystemExecutioner.GetObject().StartCoroutine(Wait(info, translatedDialogData.GetClipDuration()));

            string nextNodeId = GetNextNodeId(info.GraphInstance);
            return new ProcessInfo(id, nextNodeId, nextNodeId == "" ? ProcessInfo.ExecutionFlowType.EndExecution : ProcessInfo.ExecutionFlowType.Wait);
        }

        public override IEnumerator Wait(ExecInfo info, float duration)
        {
            yield return new WaitForSeconds(duration);
            DialogPanelScript.HideDialog();
            info.NodeSystemExecutioner.TickProcess();
        }
    }
}