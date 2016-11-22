﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PeNet.Parser
{
    internal class MetaDataStreamUSParser : SafeParser<List<string>>
    {
        private readonly uint _size;

        public MetaDataStreamUSParser(byte[] buff, uint offset, uint size) 
            : base(buff, offset)
        {
            _size = size;
        }

        protected override List<string> ParseTarget()
        {
            var stringList = new List<string>();

            for (var i = _offset; i < _offset + _size; i++)
            {
                var tmpString = Utility.GetUnicodeString(i, _buff);
                i += (uint) tmpString.Length * 2 + 1 ;

                if (String.IsNullOrWhiteSpace(tmpString))
                    continue;

                stringList.Add(tmpString);
            }

            return stringList;
        }
    }
}