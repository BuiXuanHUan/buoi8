using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Lab05.BUS
{
    public class MajorService
    {
        public List<Major> GetAllByFacutly (int facutlyID )
        {
            Model1 model1 = new Model1 ();
            return model1.Majors.Where (p => p.FacultyID == facutlyID).ToList ();
        }
    }
}
