using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    // UserLogin myDeserializedClass = JsonConvert.DeserializeObject<UserLogin>(myJsonResponse); 
    public class UserLog
    {
        public int valor { get; set; }
        public object UserName { get; set; }
    }

    public class UserLogin
    {
        public List<UserLog> Table { get; set; }
    }


}
