﻿#region License

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
namespace MvcTurbine.Autofac {
    using System;
    using System.Collections.Generic;
    using ComponentModel;
    using global::Autofac;

    /// <summary>
    /// Default implementation of the <seealso cref="IServiceLocator"/> contract with Autofac IoC.
    /// </summary>
    /// <remarks>
    /// To learn more about Autofac, please visit its website: http://code.google.com/p/autofac
    /// </remarks>
    public class AutofacServiceLocator : IServiceLocator {
        private TurbineAutofacModule currentModule;

        /// <summary>
        /// Default constructor. Locator is instantiated with a new <see cref="ContainerBuilder"/> instance.
        /// </summary>
        public AutofacServiceLocator()
            : this(new ContainerBuilder()) {
        }

        public AutofacServiceLocator(ContainerBuilder builder) {
            if (builder == null) {
                throw new ArgumentNullException("builder",
                    "The specified Autofac ContainerBuilder cannot be null.");
            }

            Builder = builder;
        }

        public ContainerBuilder Builder { get; private set; }

        public void Dispose() {
        }

        public T Resolve<T>() where T : class {
            try {
                using (var container = Builder.Build()) {
                    return container.Resolve<T>();
                }
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public T Resolve<T>(string key) where T : class {
            try {
                using (var container = Builder.Build()) {
                    return container.Resolve<T>(key);
                }
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public T Resolve<T>(Type type) where T : class {
            try {
                using (var container = Builder.Build()) {
                    return container.Resolve(type) as T;
                }
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public object Resolve(Type type) {
            try {
                using (var container = Builder.Build()) {
                    return container.Resolve(type);
                }
            } catch (Exception ex) {
                throw new ServiceResolutionException(type, ex);
            }
        }

        public IList<T> ResolveServices<T>() where T : class {
            try {
                using (var container = Builder.Build()) {
                    var enumerable = container.Resolve<IEnumerable<T>>();
                    return new List<T>(enumerable);
                }
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public IServiceRegistrar Batch() {
            currentModule = new TurbineAutofacModule();
            Builder.RegisterModule(currentModule);

            return currentModule;
        }

        public void Register<Interface>(Type implType) where Interface : class {
            currentModule.Register<Interface>(implType);
        }

        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            currentModule.Register<Interface, Implementation>();
        }

        public void Register<Interface, Implementation>(string key) where Implementation :
            class, Interface {
            currentModule.Register<Interface, Implementation>(key);
        }

        public void Register(string key, Type type) {
            currentModule.Register(key, type);
        }

        public void Register(Type serviceType, Type implType) {
            currentModule.Register(serviceType, implType);
        }

        [Obsolete("Not used with this implementation of IServiceLocator.")]
        public void Release(object instance) {
        }

        public void Reset() {
            Dispose();

            Builder = null;
            currentModule = null;
        }

        public TService Inject<TService>(TService instance) where TService : class {
            using (var container = Builder.Build()) {
                return container.InjectProperties(instance);
            }
        }

        [Obsolete("Not used with this implementation of IServiceLocator.")]
        public void TearDown<TService>(TService instance) where TService : class {
        }
    }
}
