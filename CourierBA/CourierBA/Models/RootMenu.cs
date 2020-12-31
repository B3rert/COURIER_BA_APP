using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class RootMenu
    {
        public int Application { get; set; }
        [JsonProperty("Observacion_1")]
        public string Name { get; set; }
    }
}
