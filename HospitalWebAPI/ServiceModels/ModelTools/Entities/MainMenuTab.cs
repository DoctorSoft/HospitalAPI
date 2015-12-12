namespace ServiceModels.ModelTools.Entities
{
    public class MainMenuTab
    {
        public string Label { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsActive { get; set; }
    }
}
