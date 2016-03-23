namespace ServiceModels.ModelTools
{
    public class AbstractMessagedCommandAnswer : AbstractTokenCommandAnswer
    {
        public bool HasDialogMessage { get; set; }

        public string DialogMessage { get; set; }
    }
}
