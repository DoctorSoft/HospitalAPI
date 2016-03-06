using StorageModels.Interfaces;

namespace StorageModels.Models.MailboxModels
{
    public class DischargeStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public byte[] Body { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }

        //

        public MessageStorageModel Message { get; set; }

        //

        public int MessageId { get; set; }
    }
}
