using System;
using System.Runtime.InteropServices;

namespace Unicorn.Internal
{
    /// <summary>
    /// Provides DLL imports of the unicorn library.
    /// </summary>
    internal static class unicorn
    {
        public const string UNICORN_LIB = "unicorn";

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uc_version(ref uint major, ref uint minor);

#if !RELEASE
        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool uc_arch_supported(int arch); // Not used.
#endif

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_open(uc_arch arch, uc_mode mode, ref IntPtr uc);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_close(IntPtr uc);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_query(IntPtr uc, uc_query_type query, ref UIntPtr result);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_errno(IntPtr uc);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr uc_strerror(uc_err err);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, IntPtr value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, byte[] value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, ref int value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, ref uint value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, ref long value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, ref ulong value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, ref float value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read(IntPtr uc, int regid, ref double value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, IntPtr value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, byte[] value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, ref int value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, ref uint value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, ref long value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, ref ulong value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, ref float value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write(IntPtr uc, int regid, ref double value);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_read_batch(IntPtr uc, int[] regid, IntPtr[] vals, int count);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_reg_write_batch(IntPtr uc, int[] regid, IntPtr[] vals, int count);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_emu_start(IntPtr uc, ulong begin, ulong until, ulong timeout, UIntPtr count);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_emu_stop(IntPtr uc);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_map(IntPtr uc, ulong address, UIntPtr size, uc_prot perms);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_map_ptr(IntPtr uc, ulong address, UIntPtr size, uc_prot perms, IntPtr ptr);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_map_ptr(IntPtr uc, ulong address, UIntPtr size, uc_prot perms, byte[] ptr);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_unmap(IntPtr uc, ulong address, UIntPtr size);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_write(IntPtr uc, ulong address, byte[] bytes, UIntPtr size);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_read(IntPtr uc, ulong address, byte[] bytes, UIntPtr size);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_protect(IntPtr uc, ulong address, UIntPtr size, uc_prot perms);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_mem_regions(IntPtr uc, ref IntPtr regions, ref uint count);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_context_alloc(IntPtr uc, ref IntPtr ctx);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_free(IntPtr mem);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_context_save(IntPtr uc, IntPtr ctx);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_context_restore(IntPtr uc, IntPtr ctx);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr uc_context_size(IntPtr uc);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_hook_add(IntPtr uc, ref IntPtr hh, uc_hook_type type, IntPtr callback, IntPtr user_data, ulong begin, ulong end);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_hook_add(IntPtr uc, ref IntPtr hh, uc_hook_type type, IntPtr callback, IntPtr user_data, ulong begin, ulong end, int arg0);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_hook_add(IntPtr uc, ref IntPtr hh, uc_hook_type type, IntPtr callback, IntPtr user_data, ulong begin, ulong end, ulong arg0, ulong arg1);

        [DllImport(UNICORN_LIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern uc_err uc_hook_del(IntPtr uc, IntPtr hh);
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uc_cb_hookcode(IntPtr uc, ulong address, uint size, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uc_cb_hookintr(IntPtr uc, uint into, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uc_cb_hookinsn_invalid(IntPtr uc, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate uint uc_cb_insn_in(IntPtr uc, uint port, int size, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uc_cb_insn_out(IntPtr uc, uint port, int size, uint value, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void uc_cb_hookmem(IntPtr uc, uc_mem_type type, ulong address, int size, long value, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool uc_cb_eventmem(IntPtr uc, uc_mem_type type, ulong address, int size, long value, IntPtr user_data);
}
