﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PeNet.PropertyTypes;

namespace PeNet.PEStructures
{
    /// <summary>
    /// Abstract PE header structure.
    /// </summary>
    public abstract class PEStructure : IEquatable<PEStructure>
    {
        /// <summary>
        /// Buffer containing the PE header structure.
        /// </summary>
        protected byte[] _buffer;

        /// <summary>
        /// Offset in the buffer where the structure starts.
        /// </summary>
        protected readonly uint _structOffset;

        /// <summary>
        /// Create a new PEStructure object.
        /// </summary>
        /// <param name="buffer">Buffer containing the PE structure.</param>
        /// <param name="structOffset">Offset in the buffer where the structure starts.</param>
        protected PEStructure(byte[] buffer, uint structOffset)
        {
            _buffer = buffer;
            _structOffset = structOffset;
            ParseProperties();
        }

        /// <summary>
        /// Create an empty PE structure object.
        /// </summary>
        protected PEStructure() { }

        /// <summary>
        /// Serialize the PE structure to a byte array 
        /// with the layout of the PE structure.
        /// </summary>
        /// <returns>PE structure as a byte array.</returns>
        public byte[] SerializeToBytes()
        {
            var propertyObjects = GetSortedProperties();

            var bytes = new List<byte>();
            foreach (var p in propertyObjects)
            {
                bytes.AddRange(p.ToBytes());
            }

            return bytes.ToArray();
        }

        private List<IProperty> GetSortedProperties()
        {
            var properties = GetType().GetProperties().Where(p => typeof(IProperty).IsAssignableFrom(p.PropertyType)).ToList();
            var propertyObjects = new List<IProperty>();

            // Convert the properties to a list of IProperty objects.
            foreach (var p in properties)
            {
                var pObject = (IProperty) p.GetValue(this);
                if(pObject != null)
                    propertyObjects.Add(pObject);
            }

            // Sort properties based on their offset.
            propertyObjects = propertyObjects.OrderBy(p => p.ValueOffset).ToList();
            return propertyObjects;
        }

        /// <summary>
        /// Parse and set all properties which implement the
        /// IProperty interface in the PE structure.
        /// </summary>
        protected void ParseProperties()
        {
            var properties = GetType().GetProperties().Where(p => typeof(IProperty).IsAssignableFrom(p.PropertyType)).ToList();
            var propertyTuples = new List<Tuple<PropertyDescription, PropertyInfo>>();

            foreach (var p in properties)
            {
                var description = p.GetCustomAttribute(typeof(PropertyDescription)) as PropertyDescription;
                if(description == null)
                    continue;
                propertyTuples.Add(new Tuple<PropertyDescription, PropertyInfo>(description, p));
            }

            // Order by the offset to have the correct order of properties in
            // the PE structure.
            propertyTuples = propertyTuples.OrderBy(t => t.Item1.ValueOffset).ToList();

            foreach (var pt in propertyTuples)
            {
                pt.Item2.SetValue(this, CreateProperty(pt.Item1, pt.Item2));
            }
        }

        private IProperty CreateProperty(PropertyDescription description, PropertyInfo info)
        {
            var propertyType = GetPropertyFromType(info.PropertyType.GetGenericArguments()[0]);

            if (propertyType == null)
                throw new Exception($"No compatible property type of type {info.PropertyType.Name} found.");

            return (IProperty)Activator.CreateInstance(propertyType, _buffer, _structOffset, description.ValueOffset, description.ValueSize);
        }

        private Type GetPropertyFromType(Type innerType)
        {
            var peNet = typeof(IProperty).GetTypeInfo().Assembly;
            var propertyTypes = peNet.GetTypes()
                .Where(t => t.GetTypeInfo().GetCustomAttribute(typeof(PropertyType)) != null);

            return propertyTypes.FirstOrDefault(
                p => (p.GetTypeInfo().GetCustomAttribute(typeof(PropertyType)) as PropertyType)?.Type == innerType);
        }

        /// <summary>
        ///     Creates a string representation of all properties.
        /// </summary>
        /// <returns>The header properties as a string.</returns>
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compares all properties of two objects
        /// from the type "PEStructure".
        /// </summary>
        /// <param name="other">PEStructure to compate with.</param>
        /// <returns>True if all properties are equal, false if not.</returns>
        public bool Equals(PEStructure other)
        {
            var ownProperties = GetSortedProperties();
            var otherProperties = other.GetSortedProperties();

            if (ownProperties.Count != otherProperties.Count)
                return false;

            for (var i = 0; i < ownProperties.Count; i++)
            {
                if (!ownProperties[i].Equals(otherProperties[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Compares all properties of two objects
        /// from the type "PEStructure".
        /// </summary>
        /// <param name="obj">PEStructure to compate with.</param>
        /// <returns>True if all properties are equal, false if not.</returns>
        public override bool Equals(object obj)
        {
            return Equals((PEStructure) obj);
        }

        /// <summary>
        /// Gets a hash code of the object.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Equality operator for the PE structure.
        /// </summary>
        /// <param name="s1">A PE structure.</param>
        /// <param name="s2">Another PE structure.</param>
        /// <returns>True if the structures are equal, else false.</returns>
        public static bool operator ==(PEStructure s1, PEStructure s2)
        {
            return s1.Equals(s2);
        }

        /// <summary>
        /// Inequality operator for the PE structure.
        /// </summary>
        /// <param name="s1">A PE structure.</param>
        /// <param name="s2">Another PE structure.</param>
        /// <returns>True if the structures are not equal, else false.</returns>
        public static bool operator !=(PEStructure s1, PEStructure s2)
        {
            return !s1.Equals(s2);
        }
    }
}