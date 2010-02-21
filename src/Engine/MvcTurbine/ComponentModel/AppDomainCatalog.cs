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
    using System.Linq;
    using System.ComponentModel.Composition.Hosting;

    public class AppDomainCatalog : AggregateCatalog
    {
        public AppDomainCatalog()
            : this(AppDomain.CurrentDomain, new CommonAssemblyFilter())
        {
        }

        public AppDomainCatalog(AppDomain domain, AssemblyFilter filter)
        {
            CurrentDomain = domain;

            InitializeCatalog();
        }

        protected virtual void InitializeCatalog()
        {
            if (CurrentDomain == null) return;
            var assemblies = CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                Catalogs.Add(new AssemblyCatalog(assembly));
            }
        }

        public AppDomain CurrentDomain { get; private set; }
        public AssemblyFilter Filter { get; private set; }
    }
}
