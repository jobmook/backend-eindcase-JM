using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusApp.Core.Models
{
    public class Cursus
    {
        public int Id { get; set; }

        public int Duur { get; set; }

        [MaxLength(300)]
        public string Titel { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        public virtual List<CursusInstantie> CursusInstanties { get; set; }
    }
}
