#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace MvcTurbine.ComponentModel {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a simple interface for resolving and registering components within
    /// the application.
    /// </summary>
    public interface IServiceLocator : IDisposable {
        /// <summary>
        /// Resolves the service of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <returns>An instance of the type, null otherwise.</returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Resolves the service of the specified type by the given string key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        T Resolve<T>(string key) where T : class;

        /// <summary>
        /// Resolves the service of the specified type by the given type key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="type">Key type of the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        T Resolve<T>(Type type) where T : class;

        /// <summary>
        /// Resolves the list of services of type <see cref="T"/> that are registered 
        /// within the locator.
        /// </summary>
        /// <typeparam name="T">Type of the service to resolve.</typeparam>
        /// <returns>A list of service of type <see cref="T"/>, null otherwise.</returns>
        IList<T> ResolveServices<T>() where T : class;

        /// <summary>
        /// Releases (disposes) the service instance from within the locator.
        /// </summary>
        /// <param name="instance">Instance of a service to dipose from the locator.</param>
        void Release(object instance);

        /// <summary>
        /// Resets the locator to its initial state clearing all registrations.
        /// </summary>
        void Reset();

        /// <summary>
        /// Injects any types that are registered into the specified <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        TService Inject<TService>(TService instance) where TService : class;

        /// <summary>
        /// Releases any types that have been registered into the specified <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        void TearDown<TService>(TService instance) where TService : class;
    }
}
