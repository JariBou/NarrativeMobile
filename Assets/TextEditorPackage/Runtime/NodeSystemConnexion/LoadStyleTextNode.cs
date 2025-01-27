
#if NodeSystemImplemented //Has NodeSystem Implemented

using NodeSystem.Runtime;
using NodeSystem.Runtime.Attributes;

[NodeInfo("Get Saved Stylized Text", "TextEditor/GetStylizedText", FlowDirection.None, isPure: true)]
public class GetStylizedTextNode : NodeSystemNode
{
    [ExposedProperty(portDirection: PropPortDirection.Input, preferredLocation: PropContainerLocation.InputContainer)]
    public string FileName;

    [ExposedProperty(portDirection: PropPortDirection.Output, preferredLocation: PropContainerLocation.OutputContainer)]
    public string StylizedText;

    public override ProcessInfo OnProcess(ExecInfo info)
    {
        string givenFileName = GetValueOfProp<string>(info, nameof(FileName));

        if (SavedTextManager.DoesTextExist(givenFileName))
        {
            StylizedText = SavedTextManager.LoadText(givenFileName).GetTextRichFormat();
        }
        else
        {
            StylizedText = "File not found";
        }
            
        return base.OnProcess(info);
    }
}

#endif
