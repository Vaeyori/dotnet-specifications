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

namespace Vaeyori.Specifications.Abstractions
{
    using Vaeyori.Specifications.Abstractions.Internals;

    public static class SpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> specification, Specification<T> andSpecification)
        {
            if (specification.Equals(Specification<T>.All))
                return andSpecification;
            if (andSpecification.Equals(Specification<T>.All))
                return specification;

            return new AndSpecification<T>(specification, andSpecification);
        }

        public static Specification<T> Or<T>(this Specification<T> specification, Specification<T> orSpecification)
        {
            if (specification.Equals(Specification<T>.All) || orSpecification.Equals(Specification<T>.All))
                return Specification<T>.All;


            return new OrSpecification<T>(specification, orSpecification);
        }

        public static Specification<T> Not<T>(this Specification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
    }
}
