namespace Guardian.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    public class MsilReada
    {
        private static readonly Dictionary<short, OpCode> OpCodeLookup = typeof(OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Select(field => field.GetValue(null))
            .Cast<OpCode>()
            .ToDictionary(opCode => opCode.Value);

        private static readonly OpCode[] OpCodeIgnoreLookup = new[] { OpCodes.Constrained, OpCodes.Box };

        private static OpCode GetOpCode(BinaryReader binaryReader)
        {
            int opCodeValue;
            if (binaryReader.BaseStream.Position == binaryReader.BaseStream.Length - 1)
            {
                opCodeValue = binaryReader.ReadByte();
            }
            else
            {
                opCodeValue = binaryReader.ReadUInt16();
                if ((opCodeValue & OpCodes.Prefix1.Value) != OpCodes.Prefix1.Value)
                {
                    opCodeValue &= 0xff;
                    binaryReader.BaseStream.Position--;
                }
                else
                {
                    opCodeValue = ((0xFF00 & opCodeValue) >> 8) | ((0xFF & opCodeValue) << 8);
                }
            }

            return OpCodeLookup[(short)opCodeValue];
        }

        private static int GetSize(OperandType opType)
        {
            switch (opType)
            {
                case OperandType.ShortInlineBrTarget:
                case OperandType.ShortInlineI:
                case OperandType.ShortInlineVar:
                    return 1;
                case OperandType.InlineVar:
                    return 2;
                case OperandType.InlineBrTarget:
                case OperandType.InlineField:
                case OperandType.InlineI:
                case OperandType.InlineMethod:
                case OperandType.InlineSig:
                case OperandType.InlineString:
                case OperandType.InlineSwitch:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.ShortInlineR:
                    return 4;
                case OperandType.InlineI8:
                case OperandType.InlineR:
                    return 8;
                case OperandType.InlineNone:
                default:
                    return 0;
            }
        }

        public static string Parse<T>(Func<T> expression)
        {
            var il = expression.Method.GetMethodBody().GetILAsByteArray();
            
            using (var memoryStream = new MemoryStream(il))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                var msil = new Stack<string>();

                while (memoryStream.Position != il.Length)
                {
                    var opCode = GetOpCode(binaryReader);
                    var data = binaryReader.ReadBytes(GetSize(opCode.OperandType));

                    var instruction = opCode.Name;
                    if (data.Length > 0)
                    {
                        instruction += " 0x" + string.Join(string.Empty, data.Select(@byte => @byte.ToString("x2")).ToArray());
                    }

                    msil.Push(instruction);
                }

                var x = string.Join("\r\n", msil.Reverse().ToArray());
                return x;
            }
        }
    }
}