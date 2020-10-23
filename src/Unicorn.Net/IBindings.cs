using System;

namespace Unicorn
{
    public interface IBindings
    {
        UnicornError ErrNo(IntPtr uc);
        string StrError(UnicornError err);
        int Version(ref uint major, ref uint minor);
        void Free(IntPtr ptr);

        void Open(UnicornArch arch, UnicornMode mode, ref IntPtr uc);
        void Close(IntPtr uc);

        void ContextAlloc(IntPtr uc, ref IntPtr ctx);
        void ContextRestore(IntPtr uc, IntPtr ctx);
        void ContextSave(IntPtr uc, IntPtr ctx);
        UIntPtr ContextSize(IntPtr uc);

        void EmuStart(IntPtr uc, ulong begin, ulong until, ulong timeout, UIntPtr count);
        void EmuStop(IntPtr uc);

        void HookAdd(IntPtr uc, ref IntPtr hh, UnicornHookType type, IntPtr callback, IntPtr userData, ulong address, ulong end);
        void HookAdd(IntPtr uc, ref IntPtr hh, UnicornHookType type, IntPtr callback, IntPtr userData, ulong address, ulong end, int arg0);
        void HookAdd(IntPtr uc, ref IntPtr hh, UnicornHookType type, IntPtr callback, IntPtr userData, ulong address, ulong end, ulong arg0, ulong arg1);
        void HookDel(IntPtr uc, IntPtr hh);

        void MemMap(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions permissions);
        void MemMapPtr(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions permissions, IntPtr ptr);
        void MemMapPtr(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions permissions, byte[] ptr);
        void MemProtect(IntPtr uc, ulong address, UIntPtr size, MemoryPermissions permissions);
        void MemRead(IntPtr uc, ulong address, byte[] buffer, UIntPtr count);
        void MemWrite(IntPtr uc, ulong address, byte[] buffer, UIntPtr count);
        void MemRegions(IntPtr uc, ref MemoryRegion[] regions);
        void MemUnmap(IntPtr uc, ulong address, UIntPtr size);


        void Query(IntPtr uc, UnicornQueryType type, ref UIntPtr value);

        void RegRead(IntPtr uc, int regId, IntPtr value);
        void RegRead(IntPtr uc, int regId, byte[] value);
        void RegRead(IntPtr uc, int regId, ref int value);
        void RegRead(IntPtr uc, int regId, ref uint value);
        void RegRead(IntPtr uc, int regId, ref long value);
        void RegRead(IntPtr uc, int regId, ref ulong value);
        void RegRead(IntPtr uc, int regId, ref float value);
        void RegRead(IntPtr uc, int regId, ref double value);
        void RegRead(IntPtr uc, int regId, ref NeonRegister value);

        void RegWrite(IntPtr uc, int regId, IntPtr value);
        void RegWrite(IntPtr uc, int regId, byte[] value);
        void RegWrite(IntPtr uc, int regId, ref int value);
        void RegWrite(IntPtr uc, int regId, ref uint value);
        void RegWrite(IntPtr uc, int regId, ref long value);
        void RegWrite(IntPtr uc, int regId, ref ulong value);
        void RegWrite(IntPtr uc, int regId, ref float value);
        void RegWrite(IntPtr uc, int regId, ref double value);
        void RegWrite(IntPtr uc, int regId, ref NeonRegister value);
    }
}