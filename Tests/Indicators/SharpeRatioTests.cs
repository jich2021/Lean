/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using NUnit.Framework;
using QuantConnect.Indicators;
using System;

namespace QuantConnect.Tests.Indicators
{
    [TestFixture]
    public class SharpeRatioTests : CommonIndicatorTests<IndicatorDataPoint>
    {
        protected override IndicatorBase<IndicatorDataPoint> CreateIndicator()
        {
            return new SharpeRatio("SR", 10);
        }

        protected override string TestFileName => "spy_sr.txt";

        protected override string TestColumnName => "SR_10";

        [Test]
        public void TestTradeBarsWithSameValue()
        {
            // With the value not changing, the indicator should return default value 0m.
            var sharpeRatio = new SharpeRatio("SR", 10);
            var reference = new DateTime(2021, 4, 6);

            // push the value 100000 into the indicator 20 times (sharpeRatioPeriod + movingAveragePeriod)
            for (var i = 0; i < 20; i++)
            {
                sharpeRatio.Update(reference.AddDays(i), 100000m);
            }

            Assert.AreEqual(sharpeRatio.Current.Value, 0m);
        }

        [Test]
        public void TestTradeBarsWithDifferingValue()
        {
            // With the value changing, the indicator should return a value that is not the default 0m.
            var sharpeRatio = new SharpeRatio("SR", 10);
            var reference = new DateTime(2021, 4, 6);

            // push the value 100000 into the indicator 20 times (sharpeRatioPeriod + movingAveragePeriod)
            for (var i = 0; i < 20; i++)
            {
                sharpeRatio.Update(reference.AddDays(i), 100000m + i);
            }

            Assert.AreNotEqual(sharpeRatio.Current.Value, 0m);
        }

        [Test]
        public void TestDivByZero()
        {
            // With the value changing, the indicator should return a value that is not the default 0m.
            var sharpeRatio = new SharpeRatio("SR", 10);
            var reference = new DateTime(2021, 4, 6);

            // push the value 100000 into the indicator 20 times (sharpeRatioPeriod + movingAveragePeriod)
            for (var i = 0; i < 20; i++)
            {
                sharpeRatio.Update(reference.AddDays(i), 0);
            }

            Assert.AreEqual(sharpeRatio.Current.Value, 0m);
        }
    }
}