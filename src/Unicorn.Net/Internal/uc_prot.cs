using System;
using System.Collections.Generic;
using System.Text;

namespace Unicorn.Internal
{
    internal enum uc_prot : uint
    {
        UC_PROT_NONE = 0,
        UC_PROT_READ = 1,
        UC_PROT_WRITE = 2,
        UC_PROT_EXEC = 4,
        UC_PROT_ALL = UC_PROT_READ | UC_PROT_WRITE | UC_PROT_EXEC,
    }
}