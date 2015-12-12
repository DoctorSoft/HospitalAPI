using System.Collections.Generic;
using Enums.Enums;
using ServiceModels.ModelTools;
using ServiceModels.ModelTools.Entities;

namespace ServiceModels.ServiceCommandAnswers.MainMenuCommandAnswers
{
    public class GetMainMenuItemsCommandAnswer : AbstractCommandAnswer
    {
        public List<MainMenuItemValue> MainMenuTabs { get; set; } 
    }
}
