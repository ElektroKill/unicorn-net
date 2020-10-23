using Unicorn.Internal;

namespace Unicorn
{
    /// <summary>
    /// Types of unicorn-engine query.
    /// </summary>
    public enum UnicornQueryType
    {
        /// <summary>
        /// Queries the mode.
        /// </summary>
        Mode = uc_query_type.UC_QUERY_MODE,

        /// <summary>
        /// Queries the page size.
        /// </summary>
        PageSize = uc_query_type.UC_QUERY_PAGE_SIZE,

        /// <summary>
        /// query architecture of engine (for ARM to query Thumb mode)
        /// </summary>
        Arch = uc_query_type.UC_QUERY_ARCH,

        /// <summary>
        /// query if emulation stops due to timeout (indicated if result = True)
        /// </summary>
        Timeout = uc_query_type.UC_QUERY_TIMEOUT
    }
}
