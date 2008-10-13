// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
// Portions Copyright 2000-2004 Jonathan de Halleux
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Ambience.Impl;
using MbUnit.Framework;

namespace Gallio.Ambience.Tests
{
    [TestsOn(typeof(AmbienceClient))]
    public class AmbienceClientTest
    {
        private const int PortNumber = 65435;
        private AmbienceServer server;

        [FixtureSetUp]
        public void SetUp()
        {
            server = new AmbienceServer(new AmbienceServerConfiguration()
            {
                Port = PortNumber,
                DatabasePath = "AmbienceClientTest.db"
            });
            server.Start();
        }

        [FixtureTearDown]
        public void TearDown()
        {
            if (server != null)
            {
                server.Dispose();
                server = null;
            }
        }

        [Test]
        public void ConnectThrowsIfConfigIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => AmbienceClient.Connect(null));
        }

        [Test]
        public void ContainerThrowsIfDisposed()
        {
            using (AmbienceClient client = AmbienceClient.Connect(new AmbienceClientConfiguration() { Port = PortNumber }))
            {
                client.Dispose();

                Assert.Throws<ObjectDisposedException>(() => GC.KeepAlive(client.Container));
            }
        }

        [Test]
        public void ContainerIsAWrapperForDb4o()
        {
            using (AmbienceClient client = AmbienceClient.Connect(new AmbienceClientConfiguration() { Port = PortNumber }))
            {
                Assert.IsInstanceOfType(typeof(Db4oAmbientDataContainer), client.Container);
            }
        }
    }
}