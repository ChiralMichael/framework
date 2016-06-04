﻿// Accord Statistics Library
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2016
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Statistics.Distributions.Univariate
{
    using System;
    using Accord.Math;
    using Accord.Statistics.Distributions.Fitting;
    using AForge;

    /// <summary>
    ///    Symmetric Geometric Distribution.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   The Symmetric Geometric Distribution can be seen as a discrete case
    ///   of the <see cref="LaplaceDistribution"/>.</para>
    /// </remarks>
    /// 
    /// <seealso cref="GeometricDistribution"/>
    /// <seealso cref="HypergeometricDistribution"/>
    /// 
    [Serializable]
    public class SymmetricGeometricDistribution : UnivariateDiscreteDistribution,
        IFittableDistribution<double, IFittingOptions>
    {

        // Distribution parameters
        private double p;

        // Derived measures
        private double constant;
        private double lnconstant;


        /// <summary>
        ///   Gets the success probability for the distribution.
        /// </summary>
        /// 
        public double ProbabilityOfSuccess
        {
            get { return p; }
        }

        /// <summary>
        ///   Creates a new symmetric geometric distribution.
        /// </summary>
        /// 
        /// <param name="probabilityOfSuccess">The success probability.</param>
        /// 
        public SymmetricGeometricDistribution([Unit] double probabilityOfSuccess)
        {
            if (probabilityOfSuccess < 0 || probabilityOfSuccess > 1)
                throw new ArgumentOutOfRangeException("probabilityOfSuccess",
                    "A probability must be between 0 and 1.");

            this.p = probabilityOfSuccess;
            this.lnconstant = Math.Log(p) - Math.Log(2 * (1 - p));
            this.constant = p / (2 * (1 - p));
        }


        /// <summary>
        ///   Gets the support interval for this distribution, which
        ///   in the case of the Symmetric Geometric is [-inf, +inf].
        /// </summary>
        /// 
        /// <value>
        ///   A <see cref="IntRange" /> containing
        ///   the support interval for this distribution.
        /// </value>
        /// 
        public override IntRange Support
        {
            get { return new IntRange(Int32.MinValue, Int32.MaxValue); }
        }

        /// <summary>
        ///   Gets the mean for this distribution, which in 
        ///   the case of the Symmetric Geometric is zero.
        /// </summary>
        /// 
        /// <value>
        ///   The distribution's mean value.
        /// </value>
        /// 
        public override double Mean
        {
            get { return 0; } // TODO: Test
        }

        /// <summary>
        ///   Gets the variance for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's variance.</value>
        /// 
        public override double Variance
        {
            get { return ((2 - p) * (1 - p)) / (p * p); }
        }

        /// <summary>
        ///   Not supported.
        /// </summary>
        /// 
        public override double Entropy
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        ///   Not supported.
        /// </summary>
        /// 
        public override double DistributionFunction(int k)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///   Gets the probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The probability of <c>k</c> occurring
        ///   in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        public override double ProbabilityMassFunction(int k)
        {
            return constant * Math.Pow(1 - p, Math.Abs(k));
        }

        /// <summary>
        ///   Gets the log-probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The logarithm of the probability of <c>x</c>
        ///   occurring in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>k</c> will occur.
        /// </remarks>
        /// 
        public override double LogProbabilityMassFunction(int k)
        {
            return lnconstant + Math.Abs(k) * Math.Log(1 - p);
        }


        /// <summary>
        ///   Creates a new object that is a copy of the current instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A new object that is a copy of this instance.
        /// </returns>
        /// 
        public override object Clone()
        {
            return new SymmetricGeometricDistribution(p);
        }


        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format(formatProvider, "SymmetricGeometric(x; p = {0})",
                p.ToString(format, formatProvider));
        }

    }
}