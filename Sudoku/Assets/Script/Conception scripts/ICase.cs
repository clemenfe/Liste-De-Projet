using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDeSession
{
    public interface ICase
    {
        void changeValue(int _value);
        int? getOldValue();
    }
}
