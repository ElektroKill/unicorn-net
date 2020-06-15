namespace Unicorn.Internal
{
    internal enum uc_query_type
    {
        // From unicorn.h

        /// <summary>
        /// Dynamically query current hardware mode.
        /// </summary>
        UC_QUERY_MODE = 1,
        /// <summary>
        /// query pagesize of engine
        /// </summary>
        UC_QUERY_PAGE_SIZE,
        /// <summary>
        /// query architecture of engine (for ARM to query Thumb mode)
        /// </summary>
        UC_QUERY_ARCH,
        /// <summary>
        /// query if emulation stops due to timeout (indicated if result = True)
        /// </summary>
        UC_QUERY_TIMEOUT
    }
}
