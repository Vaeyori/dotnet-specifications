/*
*    Copyright (C) 2021 Joshua "ysovuka" Thompson
*
*    This program is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Affero General Public License as published
*    by the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU Affero General Public License for more details.
*
*    You should have received a copy of the GNU Affero General Public License
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace Vaeyori.Specifications.Abstractions.UnitTests
{
    using System;
    using System.Linq.Expressions;
    using Xunit;

    internal sealed class TestClass
    {
        public TestClass(bool isTested, bool isTested2)
        {
            IsTested = isTested;
            IsTested2 = isTested2;
        }

        public bool IsTested { get; set; }
        public bool IsTested2 { get; set; }
    }


    internal sealed class IsTestClassTestedSpecification : Specification<TestClass>
    {
        public override Expression<Func<TestClass, bool>> ToExpression()
        {
            return r => r.IsTested;
        }
    }

    internal sealed class IsTestClassTested2Specification : Specification<TestClass>
    {
        public override Expression<Func<TestClass, bool>> ToExpression()
        {
            return r => r.IsTested2;
        }
    }

    public class SpecificationUnitTests
    {
        [Fact]
        public void Specification_ToExpression_SuccessfullyCreatesExpression()
        {
            var specification = new IsTestClassTestedSpecification();

            Assert.NotNull(specification.ToExpression());
            Assert.True(specification.ToExpression().Compile()(new TestClass(true, true)));
        }

        [Fact]
        public void AndSpecification_ToExpression_SuccessfullyCreatesExpression()
        {
            var specification = new IsTestClassTestedSpecification().And(new IsTestClassTested2Specification());

            Assert.NotNull(specification.ToExpression());
            Assert.True(specification.ToExpression().Compile()(new TestClass(true, true)));
            Assert.False(specification.ToExpression().Compile()(new TestClass(true, false)));
        }

        [Fact]
        public void AndSpecification_ToExpression_SuccessfullyCreatesExpressionWithAllSpecification()
        {
            var specification = new IsTestClassTestedSpecification().And(Specification<TestClass>.All);

            Assert.NotNull(specification.ToExpression());
            Assert.True(specification.ToExpression().Compile()(new TestClass(true, true)));
        }

        [Fact]
        public void OrSpecification_ToExpression_SuccessfullyCreatesExpression()
        {
            var specification = new IsTestClassTestedSpecification().Or(new IsTestClassTested2Specification());

            Assert.NotNull(specification.ToExpression());
            Assert.True(specification.ToExpression().Compile()(new TestClass(false, true)));
            Assert.True(specification.ToExpression().Compile()(new TestClass(true, false)));
            Assert.False(specification.ToExpression().Compile()(new TestClass(false, false)));
        }

        [Fact]
        public void OrSpecification_ToExpression_SuccessfullyCreatesExpressionWithAllSpecification()
        {
            var specification = new IsTestClassTestedSpecification().Or(Specification<TestClass>.All);

            Assert.NotNull(specification.ToExpression());
            Assert.True(specification.ToExpression().Compile()(new TestClass(true, true)));
        }

        [Fact]
        public void NotSpecification_ToExpression_SuccessfullyCreatesExpression()
        {
            var specification = new IsTestClassTestedSpecification().Not();

            Assert.NotNull(specification.ToExpression());
            Assert.False(specification.ToExpression().Compile()(new TestClass(true, true)));
            Assert.True(specification.ToExpression().Compile()(new TestClass(false, true)));
        }

        [Fact]
        public void AllSpecification_ToExpression_SuccessfullyCreatesExpression()
        {
            Expression<Func<TestClass, bool>> specification = Specification<TestClass>.All;

            Assert.True(specification.Compile()(new TestClass(true, true)));
        }

        [Fact]
        public void AllSpecification_ToExpression_SuccessfullyCreatesExpressionWithAndSpecification()
        {
            Expression<Func<TestClass, bool>> specification = Specification<TestClass>.All.And(new IsTestClassTested2Specification());

            Assert.True(specification.Compile()(new TestClass(true, true)));
        }

        [Fact]
        public void AllSpecification_ToExpression_SuccessfullyCreatesExpressionWithOrSpecification()
        {
            Expression<Func<TestClass, bool>> specification = Specification<TestClass>.All.Or(new IsTestClassTested2Specification());

            Assert.True(specification.Compile()(new TestClass(true, true)));
        }
    }
}
