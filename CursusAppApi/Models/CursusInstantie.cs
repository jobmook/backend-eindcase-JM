using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CursusApp.Core.Models
{
    public class CursusInstantie
    {
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Startdatum { get; set; }

        public int CursusId { get; set; }

        [JsonIgnore]
        public virtual Cursus Cursus { get; set; }
    }
}
