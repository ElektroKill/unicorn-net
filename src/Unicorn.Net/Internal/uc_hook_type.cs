namespace Unicorn.Internal
{
    internal enum uc_hook_type
    {
        // From unicorn.h

        /// <summary>
        /// Hook all interrupt/syscall events
        /// </summary>
        UC_HOOK_INTR = 1 << 0,
        /// <summary>
        /// Hook a particular instruction - only a very small subset of instructions supported here
        /// </summary>
        UC_HOOK_INSN = 1 << 1,
        /// <summary>
        /// Hook a range of code
        /// </summary>
        UC_HOOK_CODE = 1 << 2,
        /// <summary>
        /// Hook basic blocks
        /// </summary>
        UC_HOOK_BLOCK = 1 << 3,
        /// <summary>
        /// Hook for memory read on unmapped memory
        /// </summary>
        UC_HOOK_MEM_READ_UNMAPPED = 1 << 4,
        /// <summary>
        /// Hook for invalid memory write events
        /// </summary>
        UC_HOOK_MEM_WRITE_UNMAPPED = 1 << 5,
        /// <summary>
        /// Hook for invalid memory fetch for execution events
        /// </summary>
        UC_HOOK_MEM_FETCH_UNMAPPED = 1 << 6,
        /// <summary>
        /// Hook for memory read on read-protected memory
        /// </summary>
        UC_HOOK_MEM_READ_PROT = 1 << 7,
        /// <summary>
        /// Hook for memory write on write-protected memory
        /// </summary>
        UC_HOOK_MEM_WRITE_PROT = 1 << 8,
        /// <summary>
        /// Hook for memory fetch on non-executable memory
        /// </summary>
        UC_HOOK_MEM_FETCH_PROT = 1 << 9,
        /// <summary>
        /// Hook memory read events.
        /// </summary>
        UC_HOOK_MEM_READ = 1 << 10,
        /// <summary>
        /// Hook memory write events.
        /// </summary>
        UC_HOOK_MEM_WRITE = 1 << 11,
        /// <summary>
        /// Hook memory fetch for execution events
        /// </summary>
        UC_HOOK_MEM_FETCH = 1 << 12,
        /// <summary>
        /// Hook memory read events, but only successful access.
        /// The callback will be triggered after successful read.
        /// </summary>
        UC_HOOK_MEM_READ_AFTER = 1 << 13,
        /// <summary>
        /// Hook invalid instructions exceptions.
        /// </summary>
        UC_HOOK_INSN_INVALID = 1 << 14,

        /// <summary>
        /// Hook type for all events of unmapped memory access
        /// </summary>
        UC_HOOK_MEM_UNMAPPED = (UC_HOOK_MEM_READ_UNMAPPED + UC_HOOK_MEM_WRITE_UNMAPPED + UC_HOOK_MEM_FETCH_UNMAPPED),
        /// <summary>
        /// Hook type for all events of illegal protected memory access
        /// </summary>
        UC_HOOK_MEM_PROT = (UC_HOOK_MEM_READ_PROT + UC_HOOK_MEM_WRITE_PROT + UC_HOOK_MEM_FETCH_PROT),
        /// <summary>
        /// Hook type for all events of illegal read memory access
        /// </summary>
        UC_HOOK_MEM_READ_INVALID = (UC_HOOK_MEM_READ_PROT + UC_HOOK_MEM_READ_UNMAPPED),
        /// <summary>
        /// Hook type for all events of illegal write memory access
        /// </summary>
        UC_HOOK_MEM_WRITE_INVALID = (UC_HOOK_MEM_WRITE_PROT + UC_HOOK_MEM_WRITE_UNMAPPED),
        /// <summary>
        /// Hook type for all events of illegal fetch memory access
        /// </summary>
        UC_HOOK_MEM_FETCH_INVALID = (UC_HOOK_MEM_FETCH_PROT + UC_HOOK_MEM_FETCH_UNMAPPED),
        /// <summary>
        /// Hook type for all events of illegal memory access
        /// </summary>
        UC_HOOK_MEM_INVALID = (UC_HOOK_MEM_UNMAPPED + UC_HOOK_MEM_PROT),
        /// <summary>
        /// Hook type for all events of valid memory access
        /// NOTE: UC_HOOK_MEM_READ is triggered before UC_HOOK_MEM_READ_PROT and UC_HOOK_MEM_READ_UNMAPPED, 
        /// so this hook may technically trigger on some invalid reads. 
        /// </summary>
        UC_HOOK_MEM_VALID = (UC_HOOK_MEM_READ + UC_HOOK_MEM_WRITE + UC_HOOK_MEM_FETCH)

    }
}
