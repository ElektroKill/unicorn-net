using System;
using System.Runtime.InteropServices;
using Unicorn.Internal;

using static Unicorn.Internal.unicorn;

namespace Unicorn
{
    public class Bindings : IBindings
    {
        public static readonly Bindings Instance = new Bindings();

        private static void ThrowIfError(uc_err err)
        {
            if (err != uc_err.UC_ERR_OK)
                throw new UnicornException(err);
        }

        public UnicornError ErrNo(IntPtr uc)
            => (UnicornError)uc_errno(uc);

        public string StrError(UnicornError err)
            => Marshal.PtrToStringAnsi(uc_strerror((uc_err)err));

        public int Version(ref uint major, ref uint minor)
            => uc_version(ref major, ref minor);

        public void Free(IntPtr ptr)
            => ThrowIfError(uc_free(ptr));

        public void Open(UnicornArch arch, UnicornMode mode, ref IntPtr uc)
            => ThrowIfError(uc_open((uc_arch)arch, (uc_mode)mode, ref uc));

        public void Close(IntPtr uc)
            => ThrowIfError(uc_close(uc));

        public void ContextAlloc(IntPtr uc, ref IntPtr ctx)
            => ThrowIfError(uc_context_alloc(uc, ref ctx));

        public void ContextRestore(IntPtr uc, IntPtr ctx)
            => ThrowIfError(uc_context_restore(uc, ctx));

        public void ContextSave(IntPtr uc, IntPtr ctx)
            => ThrowIfError(uc_context_save(uc, ctx));

        public UIntPtr ContextSize(IntPtr uc)
            => uc_context_size(uc);

        public void EmuStart(IntPtr uc, ulong begin, ulong until, ulong timeout, UIntPtr count)
            => ThrowIfError(uc_emu_start(uc, begin, until, timeout, count));

        public void EmuStop(IntPtr uc)
            => ThrowIfError(uc_emu_stop(uc));

        public void HookAdd(IntPtr uc, ref IntPtr hh, UnicornHookType type, IntPtr callback, IntPtr userData, ulong address, ulong end)
            => ThrowIfError(uc_hook_add(uc, ref hh, (uc_hook_type)type, callback, userData, address, end));

        public void HookAdd(IntPtr uc, ref IntPtr hh, UnicornHookType type, IntPtr callback, IntPtr userData, ulong address, ulong end, int arg0)
            => ThrowIfError(uc_hook_add(uc, ref hh, (uc_hook_type)type, callback, userData, address, end, arg0));

        public void HookAdd(IntPtr uc, ref IntPtr hh, UnicornHookType type, IntPtr callback, IntPtr userData, ulong address, ulong end, ulong arg0, ulong arg1)
            => ThrowIfError(uc_hook_add(uc, ref hh, (uc_hook_type)type, callback, userData, address, end, arg0, arg1));

        public void HookDel(IntPtr uc, IntPtr hh)
            => ThrowIfError(uc_hook_del(uc, hh));

        public void MemMap(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions perms)
            => ThrowIfError(uc_mem_map(uc, address, size, (uc_prot)perms));

        public void MemMapPtr(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions permissions, IntPtr ptr)
            => ThrowIfError(uc_mem_map_ptr(uc, address, size, (uc_prot)permissions, ptr));

        public void MemMapPtr(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions permissions, byte[] ptr)
            => ThrowIfError(uc_mem_map_ptr(uc, address, size, (uc_prot)permissions, ptr));

        public void MemProtect(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions perms)
            => ThrowIfError(uc_mem_protect(uc, address, size, (uc_prot)perms));

        public void MemRead(IntPtr uc, ulong address, byte[] buffer, UIntPtr count)
            => ThrowIfError(uc_mem_read(uc, address, buffer, count));

        public void MemRegions(IntPtr uc, ref MemoryRegion[] regions)
        {
            uint count = 0;
            IntPtr regionsPtr = IntPtr.Zero;

            ThrowIfError(uc_mem_regions(uc, ref regionsPtr, ref count));

            regions = new MemoryRegion[count];

            if (count > 0 && regionsPtr != IntPtr.Zero)
            {
                var ptr = regionsPtr;
                var size = Marshal.SizeOf(typeof(uc_mem_region));
                for (int i = 0; i < count; i++)
                {
                    var nativeRegion = (uc_mem_region)Marshal.PtrToStructure(ptr, typeof(uc_mem_region));
                    regions[i] = new MemoryRegion(nativeRegion.begin, nativeRegion.end, (MemoryPermissions)nativeRegion.perms);
                    ptr += size;
                }

                Free(regionsPtr);
            }
        }

        public void MemUnmap(IntPtr uc, ulong address, UIntPtr size)
            => ThrowIfError(uc_mem_unmap(uc, address, size));

        public void MemWrite(IntPtr uc, ulong address, byte[] bytes, UIntPtr size)
            => ThrowIfError(uc_mem_write(uc, address, bytes, size));

        public void Query(IntPtr uc, UnicornQueryType type, ref UIntPtr value)
            => ThrowIfError(uc_query(uc, (uc_query_type)type, ref value));


        public void RegRead(IntPtr uc, int regId, IntPtr value)
            => ThrowIfError(uc_reg_read(uc, regId, value));

        public void RegRead(IntPtr uc, int regId, byte[] value)
            => ThrowIfError(uc_reg_read(uc, regId, value));

        public void RegRead(IntPtr uc, int regId, ref int value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));

        public void RegRead(IntPtr uc, int regId, ref uint value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));

        public void RegRead(IntPtr uc, int regId, ref long value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));

        public void RegRead(IntPtr uc, int regId, ref ulong value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));

        public void RegRead(IntPtr uc, int regId, ref float value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));

        public void RegRead(IntPtr uc, int regId, ref double value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));
        public void RegRead(IntPtr uc, int regId, ref NeonRegister value)
            => ThrowIfError(uc_reg_read(uc, regId, ref value));


        public void RegWrite(IntPtr uc, int regId, IntPtr value)
            => ThrowIfError(uc_reg_write(uc, regId, value));

        public void RegWrite(IntPtr uc, int regId, byte[] value)
            => ThrowIfError(uc_reg_write(uc, regId, value));

        public void RegWrite(IntPtr uc, int regId, ref int value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));

        public void RegWrite(IntPtr uc, int regId, ref uint value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));

        public void RegWrite(IntPtr uc, int regId, ref long value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));

        public void RegWrite(IntPtr uc, int regId, ref ulong value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));

        public void RegWrite(IntPtr uc, int regId, ref float value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));

        public void RegWrite(IntPtr uc, int regId, ref double value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));

        public void RegWrite(IntPtr uc, int regId, ref NeonRegister value)
            => ThrowIfError(uc_reg_write(uc, regId, ref value));
    }
}
