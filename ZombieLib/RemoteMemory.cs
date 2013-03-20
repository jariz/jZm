using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;

namespace ZombieAPI
{
    /// <summary>
    /// RemoteMemory is the core of jZm, it handles all memory reading/writing to the game.
    /// RemoteMemory is derived from TextWriter
    /// </summary>
    public class RemoteMemory : TextWriter
    {
        #region Public vars
        public Process Process
        {
            get
            {
                return _process;
            }
        }

        public IntPtr ProcessHandle
        {
            get
            {
                return _phandle;
            }
        }

        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public override Encoding Encoding
        {
            get
            {
                return _encoding;
            }
        }

        public bool Debug
        {
            get
            {
                return _debug;
            }
            set
            {
                _debug = value;
            }
        }
        #endregion

        #region Private vars
        Process _process;
        bool _debug = false;
        int _position;
        int _pid = -1;
        IntPtr _phandle = IntPtr.Zero;
        Encoding _encoding;
        #endregion

        #region WINAPI
        public enum Protection
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }
        #endregion

        #region Public functions
        public RemoteMemory(Process proc)
        {
            _process = proc;
            _pid = proc.Id;
            _encoding = Encoding.Default;
            if (_phandle == IntPtr.Zero) // fixed memory leak (open process only if it wasnt oppened)
                _phandle = I.OpenProcess(I.PROCESS_VM_OPERATION | I.PROCESS_VM_READ | I.PROCESS_VM_WRITE, false, _pid);
            if (_phandle == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        // call this when exiting jZm
        public void CloseRemoteMemory(Process proc)
        {
            I.CloseHandle(_phandle);
        }

        #region Write
        public override void Write(int value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public void Write(float[] value)
        {
            var bytes = new byte[value.Length * 4];
            Buffer.BlockCopy(value, 0, bytes, 0, bytes.Length);
            WriteInternal(bytes);
        }

        public override void Write(bool value)
        {
            BitConverter.GetBytes(value);
        }

        public void Write(short value)
        {
            BitConverter.GetBytes(value);
        }

        public override void Write(string value)
        {
            WriteInternal(_encoding.GetBytes(value));
        }

        public override void Write(char value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public override void Write(decimal value)
        {
            WriteInternal(BitConverter.GetBytes(Convert.ToDouble(value)));
        }

        public override void Write(uint value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public override void Write(ulong value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public override void Write(long value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public override void Write(object value)
        {
            WriteInternal(_encoding.GetBytes(value.ToString()));
        }

        public override void Write(char[] buffer)
        {
            WriteInternal(_encoding.GetBytes(buffer));
        }

        public override void Write(float value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public override void Write(double value)
        {
            WriteInternal(BitConverter.GetBytes(value));
        }

        public override void Write(string format, object arg0, object arg1)
        {
            Write(string.Format(format, arg0, arg1));
        }

        #endregion
        #region WriteLine

        public override void WriteLine()
        {
            Write(Environment.NewLine);
        }

        public override void WriteLine(bool value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(int value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(char value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(long value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(float value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(uint value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(ulong value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(decimal value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(string value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(object value)
        {
            Write(value.ToString() + Environment.NewLine);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            WriteLine(string.Format(format, arg) + Environment.NewLine);
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            WriteLine(string.Format(format, arg0, arg1, arg2) + Environment.NewLine);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            WriteLine(string.Format(format, arg0, arg1) + Environment.NewLine);
        }

        public override void WriteLine(double value)
        {
            Write(value + Environment.NewLine);
        }

        public override void WriteLine(char[] buffer)
        {
            Write(Convert.ToString(buffer) + Environment.NewLine);
        }

        #endregion
        #region Read
        public int ReadInt32()
        {
            return BitConverter.ToInt32(ReadInternal(4), 0);
        }
        public uint ReadUInt32()
        {
            return BitConverter.ToUInt32(ReadInternal(4), 0);
        }
        public long ReadInt64()
        {
            return BitConverter.ToInt64(ReadInternal(8), 0);
        }
        public string ReadString(int length)
        {
            return _encoding.GetString(ReadInternal(length));
        }
        public float[] ReadVec2()
        {
            return ReadFloatArr(2);
        }
        public float[] ReadVec3()
        {
            return ReadFloatArr(3);
        }
        public float[] ReadVec4()
        {
            return ReadFloatArr(4);
        }
        public short ReadInt16()
        {
            return BitConverter.ToInt16(ReadInternal(2), 0);
        }
        public float ReadFloat()
        {
            return BitConverter.ToSingle(ReadInternal(4), 0);
        }
        public string ReadString()
        {
            return _encoding.GetString(ReadInternal(16));
        }
        public string ReadStringSmart()
        {
            StringBuilder res = new StringBuilder();
            while (true)
            {
                if (res.Length > 100) return "";
                string part = _encoding.GetString(ReadInternal(1));
                if (part == "\0")
                    return res.ToString();
                else res.Append(part);
            }
        }
        #endregion
        #region Misc
        public void Move(int amount)
        {
            _position = _position + amount;
        }
        #endregion
        #endregion

        #region Private functions
        float[] ReadFloatArr(int amount)
        {
            byte[] bytes = ReadInternal(4 * amount);
            float[] floats = new float[bytes.Length / 4];

            for (int i = 0; i < bytes.Length / 4; i++)
                floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return floats;
        }
        byte[] ReadInternal(int amount)
        {
            uint read = 0;
            byte[] ret = new byte[amount];
            int x = I.ReadProcessMemory(_phandle, (IntPtr)Position, ret, (uint)amount, out read);
            if (_debug)
            {
                Console.Write("[ReadInternal] Reading from " + _position + " : ");
                PrintBytes(ret);
                Console.WriteLine(amount + "/" + read);
            }
            _position = _position + amount;
            return ret;
        }

        void PrintBytes(byte[] bytes)
        {
            Console.Write("[");
            foreach (byte b in bytes)
            {
                Console.Write(b);
            }
            Console.Write("\b] ");
        }

        void WriteInternal(byte[] bytes)
        {
            uint bytesW = 0;
            uint oldprotect;
            I.VirtualProtectEx(_phandle, (IntPtr)_position, (uint)bytes.Length, (uint)Protection.PAGE_EXECUTE_READWRITE, out oldprotect);
            I.WriteProcessMemory(_phandle, (IntPtr)_position, bytes, (uint)bytes.Length, out bytesW);
            if (_debug)
            {
                Console.Write("[WriteInternal] Writing to " + _position + " : ");
                PrintBytes(bytes);
                Console.WriteLine(bytes.Length + "/" + bytesW);
            }
            I.VirtualFreeEx(_phandle, (IntPtr)_position, (uint)bytes.Length, I.MEM_RELEASE);
            _position = _position + bytes.Length;
        }
        #endregion
    }
}
