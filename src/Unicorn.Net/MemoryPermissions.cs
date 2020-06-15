using System;
using Unicorn.Internal;

namespace Unicorn
{
    /// <summary>
    /// Defines memory region permissions.
    /// </summary>
    [Flags]
    public enum MemoryPermissions : uint
    {
        /// <summary>
        /// No permission.
        /// </summary>
        None = uc_prot.UC_PROT_NONE,

        /// <summary>
        /// Read permission.
        /// </summary>
        Read = uc_prot.UC_PROT_READ,

        /// <summary>
        /// Write permission.
        /// </summary>
        Write = uc_prot.UC_PROT_WRITE,

        /// <summary>
        /// Execute permission.
        /// </summary>
        Execute = uc_prot.UC_PROT_EXEC,

        /// <summary>
        /// All permission.
        /// </summary>
        All = uc_prot.UC_PROT_ALL
    }
}
