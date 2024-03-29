using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
