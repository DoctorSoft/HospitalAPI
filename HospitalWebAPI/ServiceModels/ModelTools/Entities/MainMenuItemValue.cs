using Enums.Enums;

namespace ServiceModels.ModelTools.Entities
{
    public class MainMenuItemValue
    {
        public MainMenuItem MainMenuItem { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsActive { get; set; }
    }
}
