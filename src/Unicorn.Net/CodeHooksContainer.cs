﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unicorn.Internal;

namespace Unicorn
{
    /// <summary>
    /// Callback for tracing code.
    /// </summary>
    /// <param name="emulator"><see cref="Emulator"/> which raised the callback.</param>
    /// <param name="address">Address where the code is being executed.</param>
    /// <param name="size">Size of the block.</param>
    /// <param name="userToken">Object associated with the callback.</param>
    public delegate void CodeHookCallback(Emulator emulator, ulong address, int size, object userToken);

    /// <summary>
    /// Represents hooks for code of an <see cref="Emulator"/>.
    /// </summary>
    public class CodeHooksContainer : HookContainer
    {
        internal CodeHooksContainer(Emulator emulator) : base(emulator)
        {
            // Space
        }

        /// <summary>
        /// Adds a <see cref="CodeHookCallback"/> to the <see cref="Emulator"/> with the specified user token which
        /// is called anytime the hook is triggered.
        /// </summary>
        /// 
        /// <param name="callback"><see cref="CodeHookCallback"/> to add.</param>
        /// <param name="userToken">Object associated with the callback.</param>
        /// <returns>A <see cref="HookHandle"/> which represents the hook.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public HookHandle Add(CodeHookCallback callback, object userToken)
        {
            Emulator.ThrowIfDisposed();

            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            return AddInternal(callback, 1, 0, userToken);
        }

        /// <summary>
        /// Adds a <see cref="CodeHookCallback"/> to the <see cref="Emulator"/> with the specified user token which
        /// is called when the hook is triggered within the specified start address and end address.
        /// </summary>
        /// 
        /// <param name="callback"><see cref="CodeHookCallback"/> to add.</param>
        /// <param name="begin">Start address of where the hook is effective (inclusive).</param>
        /// <param name="end">End address of where the hook is effective (inclusive).</param>
        /// <param name="userToken">Object associated with the callback.</param>
        /// <returns>A <see cref="HookHandle"/> which represents the hook.</returns>
        /// 
        /// <remarks>
        /// If <paramref name="begin"/> &gt; <paramref name="end"/>, the callback is called anytime the hook triggers.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public HookHandle Add(CodeHookCallback callback, ulong begin, ulong end, object userToken)
        {
            Emulator.ThrowIfDisposed();

            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            return AddInternal(callback, begin, end, userToken);
        }

        private HookHandle AddInternal(CodeHookCallback callback, ulong begin, ulong end, object userToken)
        {
            var wrapper = new uc_cb_hookcode((uc, addr, size, user_data) =>
            {
                Debug.Assert(uc == Emulator.Handle);
                callback(Emulator, addr, size, userToken);
            });

            var ptr = Marshal.GetFunctionPointerForDelegate(wrapper);
            return Add(UnicornHookType.Code, ptr, begin, end);
        }
    }
}
