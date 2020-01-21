using System;

namespace SIML.ShieldingCalculator
{
    public class Material
    {
        /// <summary>
        /// The English name of the material
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The mass attenuation coefficient (in cm^2/g) for the material
        /// </summary>
        public double MassAttenuationCoefficient { get; set; }

        /// <summary>
        /// The build-up factor for this material
        /// </summary>
        public double BuildUpFactor { get; set; }

        public double Density { get; set; }

        public static Material Water = new Material { MassAttenuationCoefficient = .0707, BuildUpFactor = 1, Density = 1 };
        public static Material Iron = new Material { MassAttenuationCoefficient = .0599, BuildUpFactor = 1, Density = 1 };

        /// <summary>
        /// MAR at 1332 KeV
        /// </summary>
        public static Material Lead1332 = new Material { MassAttenuationCoefficient = .057, BuildUpFactor = 1, Density = 11.35 };

        /// <summary>
        /// MAR at 662 KeV
        /// </summary>
        public static Material Lead662 = new Material { MassAttenuationCoefficient = .11, BuildUpFactor = 1, Density = 11.35 };
    }

    /// <summary>
    /// Calculates the shielding factors given the Linear Attenuation Shielding Formula
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// Given a particular material, shielding depth and unshielded exposure rate, calculate the effect of the shielding.
        /// </summary>
        /// <param name="material">The material to be used for the calculation</param>
        /// <param name="depth">How deep the shielding is</param>
        /// <param name="unshieldedExposureRate">The rate of exposure in R/hr without shielding.</param>
        /// <returns>A decimal representing the shielded exposure rate</returns>
        public static double GetShieldedExposureRate(Material material, double depth, double unshieldedExposureRate)
        {
            var power = -1 * material.MassAttenuationCoefficient * material.Density * depth;

            return unshieldedExposureRate * Math.Pow(Math.E, power);
        }

        /// <summary>
        /// Given a particular material and a linear value (and depth of the shielding), return the shielding coefficient.
        /// </summary>
        /// <param name="material">The material to be used for the calculation</param>
        /// <param name="depth">How deep the shielding is</param>
        /// <returns>A decimal representing the shielding coefficient</returns>
        public static double ShieldingFactorForDepth(Material material, double depth)
        {
            return GetShieldedExposureRate(material, depth, 1);
        }

        /// <summary>
        /// Given a particular material and a desired shieldingFactor, calculate the depth of the shielding material.
        /// </summary>
        /// <param name="material">The material to be used for the calculation</param>
        /// <param name="shieldingFactor">The desired shielding factor</param>
        /// <returns>A decimal representing the depth of shielding (in cm) necessary to yield the desired shielding factor</returns>
        public static double DepthForShieldingFactor(Material material, double shieldingFactor)
        {
            return -1 * (Math.Log(shieldingFactor, Math.E) / (material.MassAttenuationCoefficient * material.Density));
        }
    }
}
