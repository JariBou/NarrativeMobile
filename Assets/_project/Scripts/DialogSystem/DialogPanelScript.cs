using NodeSystem.Runtime;
using TMPro;
using UnityEngine;

namespace _project.Scripts.DialogSystem
{
    public class DialogPanelScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text dialogText;
        public static DialogPanelScript Instance;
        private INodeSystemExecutioner _lastExecutionner;

        private void Awake()
        {
            Instance = this;
            HideDialog();
        }

        public void SetDialogText(string dialogText)
        {
            this.dialogText.text = dialogText;
        }

        public static void ShowDialog(INodeSystemExecutioner lastExecutioner)
        {
            Instance._lastExecutionner?.GetObject().StopAllCoroutines();
            Instance._lastExecutionner = lastExecutioner;
            Instance.gameObject.SetActive(true);
        }

        public static void HideDialog()
        {
            Instance.gameObject.SetActive(false);
            Instance._lastExecutionner = null;
        }
    }
}