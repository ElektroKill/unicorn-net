﻿using System;
using System.Diagnostics;

namespace Unicorn
{
    /// <summary>
    /// Represents a unicorn-engine emulator.
    /// </summary>
    public class Emulator : IDisposable
    {
        private bool _disposed;
        private readonly Hooks _hooks;
        private readonly Memory _memory;
        private readonly Registers _registers;
        private readonly IntPtr _handle;

        internal readonly UnicornArch _arch;
        internal readonly UnicornMode _mode;

        /// <summary>
        /// Gets the handle of the <see cref="Emulator"/>.
        /// </summary>
        public IntPtr Handle => _handle;

        /// <summary>
        /// Gets the <see cref="IBindings"/> of the <see cref="Emulator"/>.
        /// </summary>
        internal IBindings Bindings { get; }

        /// <summary>
        /// Gets the <see cref="UnicornMode"/> of the <see cref="Emulator"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public UnicornMode Mode
        {
            get
            {
                ThrowIfDisposed();
                return _mode;
            }
        }

        /// <summary>
        /// Gets the <see cref="UnicornArch"/> of the <see cref="Emulator"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public UnicornArch Arch
        {
            get
            {
                ThrowIfDisposed();
                return _arch;
            }
        }

        /// <summary>
        /// Gets the <see cref="Unicorn.Registers"/> of the <see cref="Emulator"/>.
        /// </summary>
        public Registers Registers
        {
            get
            {
                ThrowIfDisposed();
                return _registers;
            }
        }

        /// <summary>
        /// Gets the <see cref="Unicorn.Memory"/> of the <see cref="Emulator"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public Memory Memory
        {
            get
            {
                ThrowIfDisposed();
                return _memory;
            }
        }

        /// <summary>
        /// Gets the <see cref="Unicorn.Hooks"/> of the <see cref="Emulator"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public Hooks Hooks
        {
            get
            {
                ThrowIfDisposed();
                return _hooks;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Unicorn.Context"/> of the <see cref="Emulator"/> instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> has a differnt mode or architecture than the <see cref="Emulator"/>.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public Context Context
        {
            get
            {
                ThrowIfDisposed();

                var context = new Context(this);
                context.Capture(this);
                return context;
            }
            set
            {
                ThrowIfDisposed();

                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value._disposed)
                    throw new ObjectDisposedException(null, "Can not access disposed Context object.");
                if (value._arch != _arch || value._mode != _mode)
                    throw new ArgumentException("value must have same arch and mode as the Emulator instance.", nameof(value));

                value.Restore(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Emulator"/> class with the specified <see cref="UnicornArch"/>
        /// and <see cref="UnicornMode"/>.
        /// </summary>
        /// <param name="arch"><see cref="UnicornArch"/> to use.</param>
        /// <param name="mode"><see cref="UnicornMode"/> to use.</param>
        public Emulator(UnicornArch arch, UnicornMode mode) : this(arch, mode, Unicorn.Bindings.Instance)
        {
            /* Space */
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Emulator"/> class with the specified <see cref="IBindings"/>
        /// instance.
        /// </summary>
        internal Emulator(UnicornArch arch, UnicornMode mode, IBindings bindings)
        {
            _arch = arch;
            _mode = mode;
            _registers = new Registers(this);
            _memory = new Memory(this);
            _hooks = new Hooks(this);

            Bindings = bindings;
            Bindings.Open(arch, mode, ref _handle);
        }

        /// <summary>
        /// Starts emulation at the specified begin address and end address.
        /// </summary>
        /// <param name="begin">Address at which to begin emulation.</param>
        /// <param name="until">Address at which to end emulation.</param>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Start(ulong begin, ulong until)
        {
            ThrowIfDisposed();
            Bindings.EmuStart(Handle, begin, until, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// Starts emulation at the specified begin address, end address, timeout and number of instructions to execute.
        /// </summary>
        /// <param name="begin">Address at which to begin emulation.</param>
        /// <param name="until">Address at which to end emulation.</param>
        /// <param name="timeout">Duration to run emulation.</param>
        /// <param name="count">Number of instructions to execute.</param>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Start(ulong begin, ulong until, TimeSpan timeout, UIntPtr count)
        {
            ThrowIfDisposed();
            var microSeconds = (ulong)Math.Round(timeout.TotalMilliseconds * 1000);
            Bindings.EmuStart(Handle, begin, until, microSeconds, count);
        }

        public void Start(ulong begin, ulong until, TimeSpan timeout, uint count) 
            => Start(begin, until, timeout, new UIntPtr(count));

        public void Start(ulong begin, ulong until, TimeSpan timeout, ulong count)
            => Start(begin, until, timeout, new UIntPtr(count));

        /// <summary>
        /// Stops the emulation.
        /// </summary>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Stop()
        {
            ThrowIfDisposed();
            Bindings.EmuStop(Handle);
        }

        /// <summary>
        /// Finalizes the <see cref="Emulator"/> instance.
        /// </summary>
        ~Emulator()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="Emulator"/> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all unmanaged and optionally managed resources used by the current instance of the <see cref="Emulator"/> class.
        /// </summary>
        /// <param name="disposing"><c>true</c> to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            try { Bindings.Close(Handle); }
            catch { Debug.WriteLine("Bindings.Close() threw an exception."); }

            _disposed = true;
        }

        internal void RegRead(int regId, ref int value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegRead(int regId, ref uint value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegRead(int regId, ref long value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegRead(int regId, ref ulong value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegRead(int regId, ref float value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegRead(int regId, ref double value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegRead(int regId, ref NeonRegister value)
            => Bindings.RegRead(Handle, regId, ref value);

        internal void RegWrite(int regId, ref int value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void RegWrite(int regId, ref uint value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void RegWrite(int regId, ref long value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void RegWrite(int regId, ref ulong value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void RegWrite(int regId, ref float value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void RegWrite(int regId, ref double value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void RegWrite(int regId, ref NeonRegister value)
            => Bindings.RegWrite(Handle, regId, ref value);

        internal void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(null, "Can not access disposed Emulator object.");
        }
    }
}
