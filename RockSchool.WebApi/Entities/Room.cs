using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int SchoolId { get; set; }
        public string RoomName { get; set; }
    }
}
