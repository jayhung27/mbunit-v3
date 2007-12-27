// Copyright 2007 MbUnit Project - http://www.mbunit.com/
// Portions Copyright 2000-2004 Jonathan De Halleux, Jamie Cansdale
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Gallio.Core.ConsoleSupport;
using Gallio.Core.ProgressMonitoring;

namespace Gallio.Core.ProgressMonitoring
{
    /// <summary>
    /// A console progress monitor displays a simple tally of the amount of work
    /// to be done on the main task as a bar chart.  The progress monitor responds
    /// to cancelation events at the console.
    /// </summary>
    public class RichConsoleProgressMonitorProvider : BaseProgressMonitorProvider
    {
        private readonly IRichConsole console;

        /// <summary>
        /// Creates a rich console progress monitor provider.
        /// </summary>
        /// <param name="console">The console to which messages should be written</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="console"/> is null</exception>
        public RichConsoleProgressMonitorProvider(IRichConsole console)
        {
            if (console == null)
                throw new ArgumentNullException(@"console");

            this.console = console;
        }

        /// <inheritdoc />
        protected override IProgressMonitorPresenter GetPresenter()
        {
            return new RichConsoleProgressMonitorPresenter(console);
        }
    }
}