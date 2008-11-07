﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Threading;
using Gallio.Runtime;

namespace Gallio.Icarus.Tests
{
    class TaskManagerTest
    {
        private TaskManager taskManager;

        [SetUp]
        public void SetUp()
        {
            taskManager = new TaskManager();
        }

        [Test]
        public void StartTask_Test()
        {
            bool flag = true;
            taskManager.StartTask(delegate
            {
                do
                { }
                while (flag);
            });
            Assert.IsTrue(taskManager.TaskRunning);
            flag = false;
            Thread.Sleep(100);
            Assert.IsFalse(taskManager.TaskRunning);
        }

        [Test]
        public void ExceptionHandling_Test()
        {
            Exception ex = new Exception();
            bool flag = false;
            UnhandledExceptionPolicy.ReportUnhandledException += delegate(object sender, CorrelatedExceptionEventArgs e)
            {
                if (e.Message.StartsWith("An exception occurred in a background task."))
                    flag = true;
            };
            taskManager.StartTask(delegate { throw ex; });
            Thread.Sleep(200);
            Assert.IsTrue(flag);
        }

        [Test]
        public void OperationCanceled_Test()
        {
            bool flag = false;
            UnhandledExceptionPolicy.ReportUnhandledException += delegate(object sender, CorrelatedExceptionEventArgs e)
            {
                if (e.Message.StartsWith("An exception occurred in a background task."))
                    flag = true;
            };
            taskManager.StartTask(delegate { throw new OperationCanceledException(); });
            Thread.Sleep(100);
            Assert.IsFalse(flag);
        }

        [Test]
        public void Queue_Test()
        {
            bool flag1 = false;
            bool flag2 = false;
            taskManager.StartTask(delegate { Thread.Sleep(100); flag1 = true; });
            taskManager.StartTask(delegate { flag2 = true; });
            Thread.Sleep(200);
            Assert.IsTrue(flag1);
            Assert.IsTrue(flag2);
        }
    }
}
