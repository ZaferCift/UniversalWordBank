using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBank.Entities.Abstract
{
    public interface ILogicEntity
    {
        int Id { get; set; }
        string Word { get; set; }
        string Mean1 { get; set; }
        string Mean2 { get; set; }
        string Mean3 { get; set; }
        int Category { get; set; }
    }
}
