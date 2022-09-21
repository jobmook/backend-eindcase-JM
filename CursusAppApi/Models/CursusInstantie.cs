using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CursusApp.Core.Models
{
    public class CursusInstantie
    {
        public int Id { get; set; }
        public string Startdatum { get; set; }

        public int CursusId { get; set; }

        [JsonIgnore]
        public virtual Cursus Cursus { get; set; }
    }
}
