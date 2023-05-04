using System;

namespace Cookbook.Models.Entities
{
    public class GroupEntity
    {
        public int Group_ID { get; set; }
        public string GroupName { get; set; }
        public int OwnerID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
