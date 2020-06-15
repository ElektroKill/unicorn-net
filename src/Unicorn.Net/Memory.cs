using System;
using System.Diagnostics;

namespace Unicorn
{
    /// <summary>
    /// Represents the memory of an <see cref="Emulator"/>.
    /// </summary>
    public class Memory
    {
        // Emulator object instance which owns this Memory object instance.
        private readonly Emulator _emulator;

        internal Memory(Emulator emulator)
        {
            Debug.Assert(emulator != null);
            _emulator = emulator;
        }

        /// <summary>
        /// Gets all the <see cref="MemoryRegion"/> mapped for emulation.
        /// </summary>
        /// 
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public MemoryRegion[] Regions
        {
            get
            {
                MemoryRegion[] regions = null;
                _emulator.ThrowIfDisposed();
                _emulator.Bindings.MemRegions(_emulator.Handle, ref regions);
                return regions;
            }
        }

        /// <summary>
        /// Gets the page size of <see cref="Memory"/> which is 4KB.
        /// </summary>
        /// 
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public UIntPtr PageSize
        {
            get
            {
                UIntPtr size = UIntPtr.Zero;
                _emulator.ThrowIfDisposed();
                _emulator.Bindings.Query(_emulator.Handle, UnicornQueryType.PageSize, ref size);
                return size;
            }
        }

        /// <summary>
        /// Maps a memory region for emulation with the specified starting address, size and <see cref="MemoryPermissions"/>.
        /// </summary>
        /// <param name="address">Starting address of memory region.</param>
        /// <param name="size">Size of memory region.</param>
        /// <param name="permissions">Permissions of memory region.</param>
        /// 
        /// <exception cref="ArgumentException"><paramref name="address"/> is not aligned with <see cref="PageSize"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="size"/> is not a multiple of <see cref="PageSize"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is less than 0.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Map(ulong address, UIntPtr size, MemoryPermissions permissions)
        {
            _emulator.ThrowIfDisposed();

            if ((address & (ulong)PageSize) != 0)
                throw new ArgumentException("Address must be aligned with page size.", nameof(address));
            if ((size.ToUInt64() & PageSize.ToUInt64()) != 0)
                throw new ArgumentException("Size must be a multiple of page size.", nameof(size));
            /*
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be non-negative.");
            */

            if (permissions > MemoryPermissions.All)
                throw new ArgumentException("Permissions is invalid.", nameof(permissions));

            
           _emulator.Bindings.MemMap(_emulator.Handle, address, size, permissions);
        }

        public void Map(ulong address, uint size, MemoryPermissions permissions)
            => Map(address, new UIntPtr(size), permissions);

        public void Map(ulong address, ulong size, MemoryPermissions permissions)
            => Map(address, new UIntPtr(size), permissions);

        /// <summary>
        /// Unmaps a region of memory used for emulation with the specified starting address and size.
        /// </summary>
        /// <param name="address">Starting address of memory region.</param>
        /// <param name="size">Size of memory region.</param>
        /// 
        /// <exception cref="ArgumentException"><paramref name="address"/> is not aligned with <see cref="PageSize"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="size"/> is not a multiple of <see cref="PageSize"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is less than 0.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Unmap(ulong address, UIntPtr size)
        {
            _emulator.ThrowIfDisposed();

            if ((address & (ulong)PageSize) != (ulong)PageSize)
                throw new ArgumentException("Address must be aligned with page size.", nameof(address));
            if ((size.ToUInt64() & PageSize.ToUInt64()) != PageSize.ToUInt64())
                throw new ArgumentException("Size must be a multiple of page size.", nameof(size));

            /*
            if (size.ToUInt64() < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be non-negative.");
            */

            _emulator.Bindings.MemUnmap(_emulator.Handle, address, size);
        }

        public void Unmap(ulong address, uint size)
            => Unmap(address, new UIntPtr(size));

        public void Unmap(ulong address, ulong size)
            => Unmap(address, new UIntPtr(size));

        /// <summary>
        /// Sets permissions for a region of memory with the specified starting address, size and <see cref="MemoryPermissions"/>.
        /// </summary>
        /// <param name="address">Starting address of memory region.</param>
        /// <param name="size">Size of memory region.</param>
        /// <param name="permissions">Permissions of memory region.</param>
        /// 
        /// <exception cref="ArgumentException"><paramref name="address"/> is not aligned with <see cref="PageSize"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="size"/> is not a multiple of <see cref="PageSize"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is less than 0.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Protect(ulong address, UIntPtr size, MemoryPermissions permissions)
        {
            _emulator.ThrowIfDisposed();

            if ((address & (ulong)PageSize) != (ulong)PageSize)
                throw new ArgumentException("Address must be aligned with page size.", nameof(address));
            if ((size.ToUInt64() & PageSize.ToUInt64()) != PageSize.ToUInt64())
                throw new ArgumentException("Size must be a multiple of page size.", nameof(size));

            /*
            if (size.ToUInt64() < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be non-negative.");
            */

            if (permissions > MemoryPermissions.All)
                throw new ArgumentException("Permissions is invalid.", nameof(permissions));

            _emulator.Bindings.MemProtect(_emulator.Handle, address, size, permissions);
        }

        public void Protect(ulong address, uint size, MemoryPermissions permissions) 
            => Protect(address, new UIntPtr(size), permissions);

        public void Protect(ulong address, ulong size, MemoryPermissions permissions)
            => Protect(address, new UIntPtr(size), permissions);

        /// <summary>
        /// Writes the specified buffer to the specified memory address.
        /// </summary>
        /// <param name="address">Address to write data.</param>
        /// <param name="buffer">Data to write.</param>
        /// <param name="count">Amount of data to write.</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0 or greater than the length of <paramref name="buffer"/>.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Write(ulong address, byte[] buffer, UIntPtr count)
        {
            _emulator.ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (/* count.ToUInt64() < 0 || */ count.ToUInt64() > (ulong)buffer.LongLength)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative and less or equal to the length of data.");

            _emulator.Bindings.MemWrite(_emulator.Handle, address, buffer, count);
        }

        public void Write(ulong address, byte[] buffer, uint count) 
            => Write(address, buffer, new UIntPtr(count));
        public void Write(ulong address, byte[] buffer, ulong count) 
            => Write(address, buffer, new UIntPtr(count));

        /// <summary>
        /// Reads data at the specified address to the specified buffer.
        /// </summary>
        /// <param name="address">Address to read.</param>
        /// <param name="buffer">Buffer thats going to contain the read data.</param>
        /// <param name="count">Amount of data to read.</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0 or greater than the length of <paramref name="buffer"/>.</exception>
        /// <exception cref="UnicornException">Unicorn did not return <see cref="Binds.UnicornError.Ok"/>.</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Emulator"/> instance is disposed.</exception>
        public void Read(ulong address, byte[] buffer, UIntPtr count)
        {
            _emulator.ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (/* count < 0 || */ count.ToUInt64() > (ulong)buffer.LongLength)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative and less or equal to the length of data.");

            _emulator.Bindings.MemRead(_emulator.Handle, address, buffer, count);
        }

        public void Read(ulong address, byte[] buffer, uint count) 
            => Read(address, buffer, new UIntPtr(count));

        public void Read(ulong address, byte[] buffer, ulong count)
            => Read(address, buffer, new UIntPtr(count));
    }
}
