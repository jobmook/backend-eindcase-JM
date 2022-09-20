using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusApp.Core.Models
{
    public class CursusInstantie
    {
        public int Id { get; set; }
        public string Startdatum { get; set; }

        public virtual Cursus Cursus { get; set; }
    }
}
