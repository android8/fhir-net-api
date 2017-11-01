﻿/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Errors = Hl7.Fhir.Utility.Error;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

#if NET_FILESYSTEM

namespace Hl7.Fhir.Specification.Source.Summary
{
    // Note:
    // 1. ArtifactSummaryGenerator creates new (mutable) ArtifactSummaryPropertyBag
    // 2. ArtifactSummaryGenerator calls the available ArtifactSummaryHarvester delegates
    //    to harvest artifact summary information and add it to the property bag
    // 3. ArtifactSummaryGenerator creates a new (read-only) ArtifactSummary instance
    //    from the initialized property bag and returns it to the caller.
    //    This way, harvested property values are protected against modification by callers.

    /// <summary>Represents summary information harvested from a FHIR artifact.</summary>
    /// <remarks>
    /// Created by the <see cref="ArtifactSummaryGenerator"/> class from
    /// a property bag containing the harvested summary information.
    /// <para>
    /// Implements the <see cref="IArtifactSummaryPropertyBag"/> interface
    /// to support extension methods for accessing specific harvested properties.
    /// </para>
    /// </remarks>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ArtifactSummary : IArtifactSummaryPropertyBag
    {
        // Note: omit leading underscore to be CLS compliant
        protected readonly IArtifactSummaryPropertyBag properties;

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance from the specified exception.</summary>
        /// <param name="error">An exception that occured while harvesting artifact summary information.</param>
        public static ArtifactSummary FromException(Exception error) => FromException(error, null);

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance from the specified exception.</summary>
        /// <param name="error">An exception that occured while harvesting artifact summary information.</param>
        /// <param name="origin">The original location of the target artifact.</param>
        public static ArtifactSummary FromException(Exception error, string origin)
        {
            if (error == null) { throw Errors.ArgumentNull(nameof(error)); }
            // Create a new (default) ArtifactSummaryPropertyBag to store the artifact origin
            var properties = new ArtifactSummaryPropertyBag();
            if (!string.IsNullOrEmpty(origin))
            {
                properties[ArtifactSummaryProperties.OriginKey] = origin;
            }
            return new ArtifactSummary(properties, error);
        }

        /// <summary>Create a new <see cref="ArtifactSummary"/> instance from a set of harvested artifact summary properties.</summary>
        /// <param name="properties">A property bag with harvested artifact summary information.</param>
        public ArtifactSummary(IArtifactSummaryPropertyBag properties) : this(properties, null) { }

        /// <summary>
        /// Create a new <see cref="ArtifactSummary"/> instance from a set of harvested artifact summary properties
        /// and a runtime exception that occured during harvesting.
        /// </summary>
        public ArtifactSummary(IArtifactSummaryPropertyBag properties, Exception error)
        {
            this.properties = properties ?? throw Errors.ArgumentNull(nameof(properties));
            this.Error = error;

            // Try to parse the specified type name to a known enum value
            var typeName = ResourceTypeName;
            if (typeName != null)
            {
                ResourceType = ModelInfo.FhirTypeNameToResourceType(ResourceTypeName);
            }
        }

        /// <summary>Returns information about errors that occured while generating the artifact summary.</summary>
        public Exception Error { get; }

        /// <summary>Indicates if any errors occured while generating the artifact summary.</summary>
        /// <remarks>If <c>true</c>, then the <see cref="Error"/> property returns detailed error information.</remarks>
        public bool IsFaulted => Error != null; // cf. Task

        /// <summary>The original location of the associated artifact.</summary>
        public string Origin => properties.GetOrigin();

        /// <summary>
        /// Returns an opaque value that represents the position of the artifact within the container.
        /// Allows the <see cref="DirectorySource"/> to retrieve and deserialize the associated artifact.
        /// </summary>
        public string Position => properties.GetPosition();

        /// <summary>Returns the type name of the resource.</summary>
        public string ResourceTypeName => properties.GetTypeName();

        /// <summary>Returns the type of the resource, parsed from the original <see cref="ResourceTypeName"/> value, or <c>null</c>.</summary>
        public ResourceType? ResourceType { get; }

        /// <summary>Returns the resource uri.</summary>
        /// <remarks>The <see cref="DirectorySource"/> generates virtual uri values for resources that are not bundle entries.</remarks>
        public string ResourceUri => properties.GetResourceUri();

        #region IEnumerable

        /// <summary>Returns an enumerator that iterates through the summary properties.</summary>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => properties.GetEnumerator();

        #endregion

        #region IReadOnlyCollection

        /// <summary>Gets the number of harvested property values.</summary>
        public int Count => properties.Count;

        #endregion

        #region IReadOnlyDictionary

        /// <summary>Gets the property value associated with the specified property key.</summary>
        public object this[string key] => properties[key];

        /// <summary>Gets a collection of property keys.</summary>
        public IEnumerable<string> Keys => properties.Keys;

        /// <summary>Gets a collection of property values.</summary>
        public IEnumerable<object> Values => properties.Values;

        /// <summary>Determines wether the summary contains a property value for the specified property key.</summary>
        public bool ContainsKey(string key) => properties.ContainsKey(key);

        /// <summary>Gets the property value associated with the specified property key.</summary>
        public bool TryGetValue(string key, out object value) => properties.TryGetValue(key, out value);

        #endregion

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected virtual string DebuggerDisplay
            => $"{GetType().Name} for {ResourceTypeName} | Origin: {Origin}"
             + (IsFaulted ? " | Error: " + Error.Message : string.Empty);
    }
}

#endif
