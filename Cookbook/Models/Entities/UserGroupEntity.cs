using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class UserGroupEntity
    {
        public int User_Group_ID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
