using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using guestNetwork.Models;

namespace guestNetwork.DAL
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
