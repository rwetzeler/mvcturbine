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

namespace Okonau.Mvc.ViewModels {
    using System;

    /// <summary>
    /// Displays the information for a task.
    /// </summary>
    [Serializable]
    public class TaskViewModel {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        public DateTime Created { get; set; }
    }
}