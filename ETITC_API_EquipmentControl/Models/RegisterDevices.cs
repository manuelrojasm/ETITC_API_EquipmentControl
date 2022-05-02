using MongoDB.Bson;

namespace equipmentControl.Models
{
    public class RegisterDevices
    {
        public ObjectId Id { get; set; }

        public int IdUser { get; set; }
        public int IdDevices { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime OutputDate { get; set; }


    }
}