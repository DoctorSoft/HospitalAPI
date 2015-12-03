using System.Collections.Generic;

namespace ServiceModels.ModelTools
{
    public abstract class AbstractCommandAnswer
    {
        protected AbstractCommandAnswer()
        {
            Errors = new List<CommandAnswerError>();
        }

        public List<CommandAnswerError> Errors { get; set; } 
    }
}
