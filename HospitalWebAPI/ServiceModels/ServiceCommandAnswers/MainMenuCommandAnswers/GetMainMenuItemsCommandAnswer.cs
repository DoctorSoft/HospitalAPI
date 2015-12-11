using System.Collections.Generic;
using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers
{
    public class GetMainMenuItemsCommandAnswer : AbstractCommandAnswer
    {
        public List<MainMenuItem> MainMenuItems { get; set; }
    }
}
