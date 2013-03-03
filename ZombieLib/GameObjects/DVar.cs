using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZombieAPI.GameObjects
{
    class DVar : RemoteObject
    {
        ZombieAPI _parent;

        public DVar(Process Game, int DvarAddr, ZombieAPI Parent)
        {
            this.Mem = new RemoteMemory(Game);
            Position = DvarAddr;
            BaseOffset = DvarAddr;
            _parent = Parent;
            Mem.Position = Position;

            a_Name = aInt(); //0x0000
            Mem.Position = a_Name;
            Mem.Position = Mem.ReadInt32();
            _name = Mem.ReadStringSmart();
            Mem.Position = Position;

            a_Desc = aInt(); //0x0004
            Mem.Position = a_Desc;
            Mem.Position = Mem.ReadInt32();
            _desc = Mem.ReadStringSmart();
            Mem.Position = Position;

            Move(4); //0x0008
            a_Flags = aInt(); //0x000C
            a_Type = aInt(); //0x0010
            Move(4); //0x0014
            a_DvarValueBool = aInt(); //0x0018
            a_DvarValueInt = aInt();
            a_DvarValueUInt = aInt();
            a_DvarValueFloat = aInt();
            a_DvarValueVector = aVec(4);
            a_DvarValueStringPointer = aInt();
            Move(36); //0x001C

            Value = new DVarValue(this);
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Desc
        {
            get
            {
                return _desc;
            }
        }

        public DVarType Type
        {
            get
            {
                Mem.Position = a_Type;
                return (DVarType)Mem.ReadInt32();
            }
        }

        public DVarValue Value;
        public class DVarValue
        {
            DVar dvar;
            public DVarValue(DVar d)
            {
                dvar = d;
            }

            public object Value
            {
                get
                {
                    switch (dvar.Type)
                    {
                        case DVarType.Boolean:
                            return this.Boolean;
                        case DVarType.Float:
                            return this.Float;
                        case DVarType.Integer:
                            return this.Int;
                        case DVarType.String:
                            return this.String;
                        case DVarType.UnsignedInteger:
                            return this.UnsignedInt;
                        case DVarType.Vector:
                            return this.Vector;
                    }
                    return null;
                }
                set
                {
                    switch (dvar.Type)
                    {
                        case DVarType.Boolean:
                            this.Boolean = (bool)value;
                            break;
                        case DVarType.Float:
                            this.Float = (float)value;
                            break;
                        case DVarType.Integer:
                            this.Int = (int)value;
                            break;
                        case DVarType.String:
                            this.String = (string)value;
                            break;
                        case DVarType.UnsignedInteger:
                            this.UnsignedInt = (uint)value;
                            break;
                        case DVarType.Vector:
                            this.Vector = (float[])value;
                            break;
                    }
                }
            }

            public bool Boolean
            {
                get
                {
                    dvar.Mem.Position = dvar.a_DvarValueBool;
                    return Convert.ToBoolean(dvar.Mem.ReadInt32());
                }
                set
                {
                    dvar.Mem.Position = dvar.a_DvarValueBool;
                    dvar.Mem.Write(value);
                }
            }

            public int Int
            {
                get
                {
                    dvar.Mem.Position = dvar.a_DvarValueInt;
                    return dvar.Mem.ReadInt32();
                }
                set
                {
                    dvar.Mem.Position = dvar.a_DvarValueInt;
                    dvar.Mem.Write(value);
                }
            }

            public float Float
            {
                get
                {
                    dvar.Mem.Position = dvar.a_DvarValueFloat;
                    return dvar.Mem.ReadFloat();
                }
                set
                {
                    dvar.Mem.Position = dvar.a_DvarValueFloat;
                    dvar.Mem.Write(value);
                }
            }

            public float[] Vector
            {
                get
                {
                    dvar.Mem.Position = dvar.a_DvarValueVector;
                    return dvar.Mem.ReadVec4();
                }
                set
                {
                    dvar.Mem.Position = dvar.a_DvarValueVector;
                    dvar.Mem.Write(value);
                }
            }

            public uint UnsignedInt
            {
                get
                {
                    dvar.Mem.Position = dvar.a_DvarValueUInt;
                    return dvar.Mem.ReadUInt32();
                }
                set
                {
                    dvar.Mem.Position = dvar.a_DvarValueUInt;
                    dvar.Mem.Write(value);
                }
            }

            public string String
            {
                get
                {
                    dvar.Mem.Position = dvar.a_DvarValueStringPointer;
                    dvar.Mem.Position = dvar.Mem.ReadInt32();
                    return dvar.Mem.ReadStringSmart();
                }
                set
                {
                    dvar.Mem.Position = dvar.a_DvarValueStringPointer;
                    dvar.Mem.Position = dvar.Mem.ReadInt32();
                    dvar.Mem.Write(value);
                }
            }
        }

        public DVarFlag Flag
        {
            get
            {
                Mem.Position = a_Flags;
                return (DVarFlag)Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_Flags;
                Mem.Write(value);
            }
        }

        string _name;
        string _desc;
        int a_Name;
        int a_Desc;
        int a_Flags;
        int a_Type;
        int a_DvarValueBool;
        int a_DvarValueInt;
        int a_DvarValueUInt;
        int a_DvarValueFloat;
        int a_DvarValueVector;
        int a_DvarValueStringPointer;
    }
}
