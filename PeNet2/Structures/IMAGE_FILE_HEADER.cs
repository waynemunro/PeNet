﻿/***********************************************************************
Copyright 2016 Stefan Hausotte

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*************************************************************************/

using System.Text;

namespace PeNet.Structures
{
    /// <summary>
    ///     The File header contains information about the structure
    ///     and properties of the PE file.
    /// </summary>
    public class IMAGE_FILE_HEADER
    {
        private readonly byte[] _buff;
        private readonly uint _offset;

        /// <summary>
        ///     Create a new IMAGE_FILE_HEADER object.
        /// </summary>
        /// <param name="buff">A PE file as byte array.</param>
        /// <param name="offset">Raw offset to the file header.</param>
        public IMAGE_FILE_HEADER(byte[] buff, uint offset)
        {
            _buff = buff;
            _offset = offset;
        }

        /// <summary>
        ///     The machine (CPU type) the PE file is intended for.
        ///     Can be resolved with Utility.ResolveTargetMachine(machine).
        /// </summary>
        public ushort Machine
        {
            get { return Utility.BytesToUInt16(_buff, _offset); }
            set { Utility.SetUInt16(value, _offset, _buff); }
        }

        /// <summary>
        ///     The number of sections in the PE file.
        /// </summary>
        public ushort NumberOfSections
        {
            get { return Utility.BytesToUInt16(_buff, _offset + 0x2); }
            set { Utility.SetUInt16(value, _offset + 0x2, _buff); }
        }

        /// <summary>
        ///     Time and date stamp.
        /// </summary>
        public uint TimeDateStamp
        {
            get { return Utility.BytesToUInt32(_buff, _offset + 0x4); }
            set { Utility.SetUInt32(value, _offset + 0x4, _buff); }
        }

        /// <summary>
        ///     Pointer to COFF symbols table. They are rare in PE files,
        ///     but often in obj files.
        /// </summary>
        public uint PointerToSymbolTable
        {
            get { return Utility.BytesToUInt32(_buff, _offset + 0x8); }
            set { Utility.SetUInt32(value, _offset + 0x8, _buff); }
        }

        /// <summary>
        ///     The number of COFF symbols.
        /// </summary>
        public uint NumberOfSymbols
        {
            get { return Utility.BytesToUInt32(_buff, _offset + 0xC); }
            set { Utility.SetUInt32(value, _offset + 0xC, _buff); }
        }

        /// <summary>
        ///     The size of the optional header which follow the file header.
        /// </summary>
        public ushort SizeOfOptionalHeader
        {
            get { return Utility.BytesToUInt16(_buff, _offset + 0x10); }
            set { Utility.SetUInt16(value, _offset + 0x10, _buff); }
        }

        /// <summary>
        ///     Set of flags which describe the PE file in detail.
        ///     Can be resolved with Utility.ResolveCharacteristics(characteristics).
        /// </summary>
        public ushort Characteristics
        {
            get { return Utility.BytesToUInt16(_buff, _offset + 0x12); }
            set { Utility.SetUInt16(value, _offset + 0x12, _buff); }
        }

        /// <summary>
        ///     Creates a string representation of all object properties.
        /// </summary>
        /// <returns>The file header properties as a string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder("IMAGE_FILE_HEADER\n");
            sb.Append(Utility.PropertiesToString(this, "{0,-15}:\t{1,10:X}\n"));
            return sb.ToString();
        }
    }
}