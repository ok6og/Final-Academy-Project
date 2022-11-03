using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Requests.UserRequests
{
    public class AddUserRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
