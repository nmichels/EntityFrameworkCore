// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="RelationalMethodCallTranslatorProvider" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    ///     <para>
    ///         Do not construct instances of this class directly from either provider or application code as the
    ///         constructor signature may change as new dependencies are added. Instead, use this type in
    ///         your constructor so that an instance will be created and injected automatically by the
    ///         dependency injection container. To create an instance with some dependent services replaced,
    ///         first resolve the object from the dependency injection container, then replace selected
    ///         services using the 'With...' methods. Do not call the constructor at any point in this process.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Singleton" />. This means a single instance
    ///         is used by many <see cref="DbContext" /> instances. The implementation must be thread-safe.
    ///         This service cannot depend on services registered as <see cref="ServiceLifetime.Scoped" />.
    ///     </para>
    /// </summary>
    public sealed class RelationalMethodCallTranslatorProviderDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="RelationalMethodCallTranslatorProvider" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from either provider or application code as it may change
        ///         as new dependencies are added. Instead, use this type in your constructor so that an instance
        ///         will be created and injected automatically by the dependency injection container. To create
        ///         an instance with some dependent services replaced, first resolve the object from the dependency
        ///         injection container, then replace selected services using the 'With...' methods. Do not call
        ///         the constructor at any point in this process.
        ///     </para>
        ///     <para>
        ///         This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///         the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///         any release. You should only use it directly in your code with extreme caution and knowing that
        ///         doing so can result in application failures when updating to a new Entity Framework Core release.
        ///     </para>
        /// </summary>
        [EntityFrameworkInternal]
        public RelationalMethodCallTranslatorProviderDependencies(
            [NotNull] ISqlExpressionFactory sqlExpressionFactory,
            [NotNull] IEnumerable<IMethodCallTranslatorPlugin> plugins,
            [NotNull] IRelationalTypeMappingSource typeMappingSource)
        {
            Check.NotNull(sqlExpressionFactory, nameof(sqlExpressionFactory));
            Check.NotNull(plugins, nameof(plugins));
            Check.NotNull(typeMappingSource, nameof(typeMappingSource));

            SqlExpressionFactory = sqlExpressionFactory;
            RelationalTypeMappingSource = typeMappingSource;
            Plugins = plugins;
        }

        /// <summary>
        ///     The expression factory..
        /// </summary>
        public ISqlExpressionFactory SqlExpressionFactory { get; }

        /// <summary>
        ///     Registered plugins.
        /// </summary>
        public IEnumerable<IMethodCallTranslatorPlugin> Plugins { get; }
        /// <summary>
        ///     Relational Type Mapping Source.
        /// </summary>
        public IRelationalTypeMappingSource RelationalTypeMappingSource { get; }

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="sqlExpressionFactory"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalMethodCallTranslatorProviderDependencies With([NotNull] ISqlExpressionFactory sqlExpressionFactory)
            => new RelationalMethodCallTranslatorProviderDependencies(sqlExpressionFactory, Plugins, RelationalTypeMappingSource);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="plugins"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalMethodCallTranslatorProviderDependencies With([NotNull] IEnumerable<IMethodCallTranslatorPlugin> plugins)
            => new RelationalMethodCallTranslatorProviderDependencies(SqlExpressionFactory, plugins, RelationalTypeMappingSource);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="typeMappingSource"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalMethodCallTranslatorProviderDependencies With([NotNull] IRelationalTypeMappingSource typeMappingSource)
            => new RelationalMethodCallTranslatorProviderDependencies(SqlExpressionFactory, Plugins, typeMappingSource);
    }
}
