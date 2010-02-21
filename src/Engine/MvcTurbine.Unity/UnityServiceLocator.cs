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

namespace MvcTurbine.Unity {
    using System;
    using System.Collections.Generic;
    using ComponentModel;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Default implementation of the <seealso cref="IServiceLocator"/> contract with Unity IoC.
    /// </summary>
    /// <remarks>
    /// To learn more about Unity, please visit its website: http://www.codeplex.com/unity
    /// </remarks>
    [Serializable]
    public class UnityServiceLocator : IServiceLocator {
        
        /// <summary>
        /// Creates an instance of the locator with an empty <seealso cref="IUnityContainer"/> instance.
        /// </summary>
        public UnityServiceLocator()
            : this(new UnityContainer()) {
        }

        /// <summary>
        /// Creates an instance of the locator with the specified <seealso cref="IUnityContainer"/> instance.
        /// </summary>
        /// <param name="container">Instance of <seealso cref="IUnityContainer"/> to use.</param>
        public UnityServiceLocator(IUnityContainer container) {
            if (container == null) {
                throw new ArgumentNullException("container",
                    "The specified Unity container cannot be null.");
            }

            Container = container;
        }

        /// <summary>
        /// Gets the current <seealso cref="IUnityContainer"/> associated with this instance.
        /// </summary>
        public IUnityContainer Container { private set; get; }

        /// <summary>
        /// Resolves the service of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>() where T : class {
            try {
                return Container.Resolve<T>();
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        /// <summary>
        /// Resolves the service of the specified type by the given string key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>(string key) where T : class {
            try {
                return Container.Resolve<T>(key);
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        /// <summary>
        /// Resolves the service of the specified type by the given type key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="type">Key type of the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>(Type type) where T : class {
            try {
                return Container.Resolve(type) as T;
            } catch (Exception ex) {
                throw new ServiceResolutionException(type, ex);
            }
        }

        /// <summary>
        /// Resolves the list of services of type <see cref="T"/> that are registered 
        /// within the locator.
        /// </summary>
        /// <typeparam name="T">Type of the service to resolve.</typeparam>
        /// <returns>A list of service of type <see cref="T"/>, null otherwise.</returns>
        public IList<T> ResolveServices<T>() where T : class {
            IEnumerable<T> services = Container.ResolveAll<T>();
            if(services == null) return null;

            return new List<T>(services);
        }

        /// <summary>
        /// Releases (disposes) the service instance from within the locator.
        /// </summary>
        /// <param name="instance">Instance of a service to dipose from the locator.</param>
        public void Release(object instance) {
            if (instance == null) return;

            Container.Teardown(instance);
        }

        /// <summary>
        /// Resets the locator to its initial state clearing all registrations.
        /// </summary>
        public void Reset() {
            Dispose();
        }

        public TService Inject<TService>(TService instance) where TService : class {
            return Container.BuildUp(instance);
        }

        public void TearDown<TService>(TService instance) where TService : class {
            Container.Teardown(instance);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {
            if (Container == null) return;

            Container.Dispose();
            Container = null;
        }
    }
}
