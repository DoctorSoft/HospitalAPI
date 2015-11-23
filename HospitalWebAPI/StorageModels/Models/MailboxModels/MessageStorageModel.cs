using StorageModels.Enums;
using StorageModels.Interfaces;

namespace StorageModels.Models.MailboxModels
{
    public class MessageStorageModel : IIdModel, IShowStatusModel
    {
        public int Id { get; set; }
        public TwoSideShowStatus ShowStatus { get; set; }

        //

        
    }
}
