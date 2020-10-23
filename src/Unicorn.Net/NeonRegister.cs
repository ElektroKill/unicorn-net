using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 16)]
    public struct NeonRegister
    {
        [FieldOffset(0)]
        public float Float0;

        [FieldOffset(4)]
        public float Float1;

        [FieldOffset(8)]
        public float Float2;

        [FieldOffset(12)]
        public float Float3;

        [FieldOffset(0)]
        public double Double0;

        [FieldOffset(8)]
        public double Double1;
    }
}
