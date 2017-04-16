﻿namespace PeNet.PropertyTypes
{
    /// <summary>
    /// Represents a standard property in the header structures,
    /// where the value is of the type "byte".
    /// </summary>
    [PropertyType(typeof(byte))]
    public sealed class PropertyByte : Property<byte>
    {
        /// <summary>
        /// Create a new property object.
        /// </summary>
        /// <param name="valueOffset">Offset of the value in the structure
        /// to which the property belongs.</param>
        /// <param name="size">Size of the value type in bytes.</param>
        /// <param name="value">The value of the property.</param>
        public PropertyByte(uint valueOffset, uint size, byte value)
            : base(valueOffset, size, value) { }

        /// <summary>
        /// Create a new property object.
        /// </summary>
        /// <param name="valueOffset">Offset of the value in the structure
        /// to which the property belongs.</param>
        /// <param name="value">The value of the property.</param>
        public PropertyByte(uint valueOffset, byte value)
            : base(valueOffset, sizeof(byte), value) { }

        /// <summary>
        /// Create a new property object.
        /// </summary>
        /// <param name="structOffset">Offset of the structure in the PE header
        /// to which the property belongs.</param>
        /// <param name="valueOffset">Offset of the value in the structure
        /// to which the property belongs.</param>
        /// <param name="size">Size of the value type in bytes.</param>
        /// <param name="buffer">Buffer containing a PE structure.</param>
        public PropertyByte(byte[] buffer, uint structOffset, uint valueOffset, uint size)
            : base(buffer, structOffset, valueOffset, size)
        {
            Value = ParseValue();
        }


        /// <summary>
        /// Parses the value from the byte 
        /// array.
        /// </summary>
        /// <returns>The property value.</returns>
        protected override byte ParseValue()
        {
            var value = _buffer[_structOffset + ValueOffset];
            return value;
        }

        /// <summary>
        /// Serializes the property value to
        /// a byte array.
        /// </summary>
        /// <returns>Property value as a byte array.</returns>
        public override byte[] ToBytes()
        {
            return new byte[] {Value};
        }
    }
}