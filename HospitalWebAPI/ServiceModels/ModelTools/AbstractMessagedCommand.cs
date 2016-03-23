namespace ServiceModels.ModelTools
{
    public class AbstractMessagedCommand : AbstractTokenCommand
    {
        public bool? HasDialogMessage { get; set; }

        public string DialogMessage { get; set; }
    }
}
