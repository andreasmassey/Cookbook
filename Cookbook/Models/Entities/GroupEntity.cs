using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class GroupEntity
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int OwnerID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
