using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIML.ShieldingCalculator;
using System;

namespace SIML.ShieldingCalculator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Slide 37 from https://www.nrc.gov/docs/ML1122/ML11229A721.pdf
        /// </summary>
        [TestMethod]
        public void LeadShieldingShieldedRate1332()
        {
            // arrange
            var material = Material.Lead1332;
            var shieldDepth = 1;
            var unshieldedExposureRate = 100;

            // act
            var observed = Calculator.GetShieldedExposureRate(material, shieldDepth, unshieldedExposureRate);

            // round to make life easier for the test
            observed = Math.Round(observed);

            // assert
            Assert.AreEqual(52, observed);
        }

        /// <summary>
        /// Slide 38 from https://www.nrc.gov/docs/ML1122/ML11229A721.pdf
        /// </summary>
        [TestMethod]
        public void LeadShieldingShieldedRate662()
        {
            // arrange
            var material = Material.Lead662;
            var shieldDepth = 1;
            var unshieldedExposureRate = 100;

            // act
            var observed = Calculator.GetShieldedExposureRate(material, shieldDepth, unshieldedExposureRate);

            // round to make life easier for the test
            observed = Math.Round(observed);

            // assert
            Assert.AreEqual(29, observed);
        }

        /// <summary>
        /// Slide 41 from https://www.nrc.gov/docs/ML1122/ML11229A721.pdf
        /// </summary>
        [TestMethod]
        public void RequiredLeadShielding()
        {
            // arrange
            var material = Material.Lead662;
            double unshieldedExposureRate = 100;
            var shieldedExposureRate = 1;

            // act
            var observed = Calculator.DepthForShieldingFactor(material, shieldedExposureRate / unshieldedExposureRate);

            // round to make life easier for the test
            observed = Math.Round(observed, 1);

            // assert
            Assert.AreEqual(3.7, observed);
        }
    }
}
